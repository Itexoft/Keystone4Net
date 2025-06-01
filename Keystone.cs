using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Itexoft.Common.ExecutionTools.Node;
using Keystone4Net.Common;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Keystone4Net;

public sealed class Keystone<T>(T dbContext, string baseDir) where T : DbContext, IKeystoneDbContext
{
    private const string KeystoneJS = "keystone.js";
    
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        IncludeFields = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            new ValueConverter(),
            new ObjectConverter(),
            new FunctionConverter(),
            new FunctionCallConverter()
        }
    };

    public string BuildDirectory { get; } = Path.Combine(baseDir, "." + Path.GetFileNameWithoutExtension(KeystoneJS));

    public bool IsBuilt => Directory.Exists(this.BuildDirectory);
    
    public async Task InstallAsync(CancellationToken cancellationToken = default)
    {
        await this.WriteKeystoneConfigAsync();
        if (!Directory.Exists(Path.Combine(baseDir, "node_modules")))
        {
            await using var npmRunner = new NpmRunner(baseDir);
            npmRunner.SetConsoleOutError();
            await npmRunner.InstallAsync(["@keystone-6/core"], cancellationToken);
        }
    }

    public IDisposable Start(CancellationToken cancellationToken = default)
    {
        var keystoneProcess = new NpxRunner(baseDir);
        _ = Task.Run(() => keystoneProcess.ExecAsync("keystone", ["start"], cancellationToken), cancellationToken);
        return keystoneProcess;
    }
    
    public async Task RebuildAsync(CancellationToken cancellationToken = default)
    {
        if (this.IsBuilt)
            Directory.Delete(this.BuildDirectory, true);
        await this.BuildAsync();
    }
    
    public async Task BuildAsync(CancellationToken cancellationToken = default)
    {
        if(this.IsBuilt)
            return;
        
        await using var npxRunner = new NpxRunner(baseDir);
        npxRunner.SetConsoleOutError();
        await npxRunner.ExecAsync("keystone", ["build"], cancellationToken);
    }
    
    public async Task WriteKeystoneConfigAsync(CancellationToken cancellationToken = default)
    {
        var keystoneJs = this.GenerateKeystoneConfig();
        await File.WriteAllTextAsync(Path.Combine(baseDir, KeystoneJS), keystoneJs, cancellationToken);
    }
    
    public string GenerateKeystoneConfig()
    {
        var config = new KeystoneConfig(dbContext, baseDir);
        dbContext.ConfigureKeystone(config);
        foreach (var list in config.Lists.Values)
        {
            var entity = dbContext.Model.FindEntityType(list.ClrType)!;
            list.Db.Map = entity.GetTableName()!;

            foreach (var (fieldKey, field) in list.Fields)
            {
                var prop = entity.FindProperty(fieldKey);

                if (prop is null)
                    continue;
                
                field.Db.Map = prop.Name;
                field.Db.IsNullable = prop.IsNullable;
            }
        }

        var json = JsonSerializer.Serialize(config, jsonSerializerOptions);
        var sb = new StringBuilder();
        foreach (var value in Enum.GetValues<KeystoneImportObjects>())
        {
            var name = Utils.ToCamelCase(value.ToString());
            var importFrom = value == KeystoneImportObjects.Core ? "" : "/" + name;
            sb.AppendLine($"import * as {name} from '@keystone-6/core{importFrom}';");
        }

        sb.AppendLine();
        sb.AppendLine($"export default {json}");

        return sb.ToString();
    }

    private sealed class ValueConverter : JsonConverter<KeystoneJsValue>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsAssignableTo(this.Type);
        
        public override KeystoneJsValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, KeystoneJsValue value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(JsonSerializer.Serialize(value.Value, options), true);
        }
    }
    
    private sealed class ObjectConverter : JsonConverter<KeystoneJsObject>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsAssignableTo(this.Type);
        
        public override KeystoneJsObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, KeystoneJsObject value, JsonSerializerOptions options)
        {
            if (value.Imports != null)
            {
                var obj = Utils.ToCamelCase(value.Imports.Value);
                writer.WriteRawValue($"{obj}.{value.Name}", true);
            }
            else
            {
                writer.WriteRawValue(value.Name, true);
            }
        }
    }
    
    private sealed class FunctionConverter : JsonConverter<KeystoneJsFunction>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsAssignableTo(this.Type);
        
        public override KeystoneJsFunction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, KeystoneJsFunction value, JsonSerializerOptions options)
        {
            var args = string.Join(", ", value.Args);
            writer.WriteRawValue($"({args}) => {value.Body}", true);
        }
    }

    private sealed class FunctionCallConverter : JsonConverter<KeystoneJsFunctionCall>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsAssignableTo(this.Type);

        public override KeystoneJsFunctionCall? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, KeystoneJsFunctionCall value, JsonSerializerOptions options)
        {
            var inline = JsonSerializer.Serialize(value.Arguments, new JsonSerializerOptions(options) { WriteIndented = false });
            var multiline = inline.Length > 150;
            var args = multiline ? JsonSerializer.Serialize(value.Arguments, new JsonSerializerOptions(options) { WriteIndented = true }) : inline;

            if (args.Length >= 2 && args[0] == '[' && args[^1] == ']')
                args = args[1..^1];
            
            if (multiline)
            {
                var nextIndent = new string(options.IndentCharacter, (writer.CurrentDepth + 1) * options.IndentSize);
                args = args.Trim().Replace(options.NewLine, $"{options.NewLine}{nextIndent}");
            }
            
            var obj = Utils.ToCamelCase(value.Imports.ToString());
            
            writer.WriteRawValue($"{obj}.{value.Name}({args})", true);
        }
    }
}