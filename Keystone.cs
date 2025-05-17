using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Itexoft.Common.ExecutionTools.Node;
using Keystone4NET.Models;
using Microsoft.EntityFrameworkCore;

namespace Keystone4NET;

public sealed class Keystone<T>(T dbContext, string baseDir) where T : DbContext, IKeystoneDbContext
{
    private const string KeystoneJS = "keystone.js";

    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        IncludeFields = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            new RefConverter(),
            new ValueConverter(),
            new ObjectConverter(),
            new FunctionConverter(),
            new FunctionCallConverter()
        }
    };

    public string BuildDirectory { get; } = Path.Combine(baseDir, "." + Path.GetFileNameWithoutExtension(KeystoneJS));

    public string ConfigFile { get; } = Path.Combine(baseDir, KeystoneJS);

    public string NodeModulesDirectory { get; } = Path.Combine(baseDir, "node_modules");

    private bool NodeModulesDirectoryExists => Directory.Exists(this.NodeModulesDirectory);

    private bool BuildDirectoryExists => Directory.Exists(this.BuildDirectory);

    private bool ConfigExists => File.Exists(this.ConfigFile);

    public async Task InstallAsync(CancellationToken cancellationToken = default)
    {
        if (this.NodeModulesDirectoryExists)
            return;

        await using var npmRunner = new NpmRunner(baseDir);
        npmRunner.SetConsoleOutError();
        await npmRunner.InstallAsync(["@keystone-6/core"], cancellationToken);
    }

    public IDisposable Start(CancellationToken cancellationToken = default)
    {
        var keystoneProcess = new NpxRunner(baseDir);
        _ = Task.Run(() => keystoneProcess.ExecAsync("keystone", ["start"], cancellationToken), cancellationToken);

        return keystoneProcess;
    }

    public async Task BuildAsync(CancellationToken cancellationToken = default)
    {
        var keystoneJs = await this.GenerateKeystoneConfigAsync();
        if (!this.ConfigExists || !this.BuildDirectoryExists || !string.Equals(keystoneJs, await this.GetKeystoneJSFileContent()))
        {
            if (this.BuildDirectoryExists)
                Directory.Delete(this.BuildDirectory, true);

            await File.WriteAllTextAsync(this.ConfigFile, keystoneJs, cancellationToken);
            await using var npxRunner = new NpxRunner(baseDir);
            npxRunner.SetConsoleOutError();
            await npxRunner.ExecAsync("keystone", ["build"], cancellationToken);
        }
    }

    public async Task<string> GenerateKeystoneConfigAsync()
    {
        var config = new KeystoneConfig(dbContext, baseDir);
        await dbContext.ConfigureKeystoneAsync(config);
        foreach (var list in config.Lists)
        {
            var entity = dbContext.Model.FindEntityType(list.ClrType)!;
            list.Db.Map = entity.GetTableName()!;

            foreach (var field in list.Fields)
            {
                var prop = entity.FindProperty(field.ToString());

                if (prop is null)
                    continue;

                field.Db.Map = prop.Name;
                field.Db.IsNullable = prop.IsNullable;
            }
        }

        var json = JsonSerializer.Serialize(config, jsonSerializerOptions);
        var sb = new StringBuilder();
        var imports = typeof(KeystoneImport)
            .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(x => x.GetValue(null, null))
            .Cast<KeystoneImport>()
            .Where(x => x.importFrom != null);

        foreach (var import in imports)
            sb.AppendLine($"import * as {import} from '@keystone-6/{import.importFrom}';");

        sb.AppendLine();
        sb.AppendLine($"export default {json}");

        return sb.ToString();
    }

    private async Task<string?> GetKeystoneJSFileContent()
    {
        return this.ConfigExists ? await File.ReadAllTextAsync(this.ConfigFile) : null;
    }

    private abstract class KeystoneJsonConverter<TValue> : JsonConverter<TValue>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableTo(this.Type);
        }

        public override TValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    private sealed class RefConverter : KeystoneJsonConverter<IKeystoneRef>
    {
        public override void Write(Utf8JsonWriter writer, IKeystoneRef value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.KeystoneName);
        }
    }
    
    private sealed class ValueConverter : KeystoneJsonConverter<KeystoneJsValue>
    {
        public override void Write(Utf8JsonWriter writer, KeystoneJsValue value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(value.Value, options), true);
        }
    }

    private sealed class ObjectConverter : KeystoneJsonConverter<KeystoneJsObject>
    {
        public override void Write(Utf8JsonWriter writer, KeystoneJsObject value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.Import != null ? $"{value.Import}.{value.Name}" : value.Name, true);
        }
    }

    private sealed class FunctionConverter : KeystoneJsonConverter<KeystoneJsFunction>
    {
        public override void Write(Utf8JsonWriter writer, KeystoneJsFunction value, JsonSerializerOptions options)
        {
            var args = string.Join(", ", value.Args);
            writer.WriteRawValue($"({args}) => {value.Body}", true);
        }
    }

    private sealed class FunctionCallConverter : KeystoneJsonConverter<KeystoneJsFunctionCall>
    {
        public override void Write(Utf8JsonWriter writer, KeystoneJsFunctionCall value, JsonSerializerOptions options)
        {
            var inline = JsonSerializer.Serialize(value.Arguments, new JsonSerializerOptions(options) { WriteIndented = false });
            var multiline = inline.Length > 130;
            var args = multiline
                ? JsonSerializer.Serialize(value.Arguments, new JsonSerializerOptions(options) { WriteIndented = true })
                : inline;

            if (args.Length >= 2 && args[0] == '[' && args[^1] == ']')
                args = args[1..^1];

            if (multiline)
            {
                var nextIndent = new string(options.IndentCharacter, (writer.CurrentDepth + 1) * options.IndentSize);
                args = args.Trim().Replace(options.NewLine, $"{options.NewLine}{nextIndent}");
            }

            writer.WriteRawValue($"{value.Import}.{value.FuncName}({args})", true);
        }
    }
}