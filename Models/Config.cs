using System.Collections.ObjectModel;
using System.Reflection;
using Keystone4NET.Common;
using Microsoft.EntityFrameworkCore;

namespace Keystone4NET.Models;

public class KeystoneDb
{
    public KeystoneDb(DbContext dbContext, string? baseDir)
    {
        var provider = dbContext.Database.ProviderName?.ToLowerInvariant() ?? string.Empty;

        foreach (var value in Enum.GetValues<KeystoneDbProvider>())
            if (provider.Contains(Utils.ToCamelCase(value)))
                this.Provider = value;

        var dataSource = dbContext.Database.GetDbConnection().DataSource;

        if (!Uri.TryCreate(dataSource, UriKind.Absolute, out var uri))
            throw new InvalidOperationException(nameof(dataSource));

        this.Url = uri.ToString();

        if (uri.IsFile)
        {
            var entryPath = Assembly.GetEntryAssembly()?.Location;

            if (entryPath is null)
                return;

            var entryDir = Path.GetDirectoryName(entryPath)!;
            var absolute = Path.GetFullPath(uri.LocalPath);

            if (baseDir != null)
                this.Url = "file:"
                           + Path.GetRelativePath(Path.Combine(entryDir, baseDir), absolute).Replace(Path.DirectorySeparatorChar, '/');
            else if (absolute.StartsWith(entryDir, StringComparison.OrdinalIgnoreCase))
                this.Url = "file:" + Path.GetRelativePath(entryDir, absolute).Replace(Path.DirectorySeparatorChar, '/');
        }
    }

    public KeystoneDbProvider Provider { get; }

    public string Url { get; }

    public KeystoneJsFunction? OnConnect { get; set; }

    public bool? EnableLogging { get; set; }

    public KeystoneIdFieldOptions? IdField { get; set; }

    public string? ShadowDatabaseUrl { get; set; }

    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public class KeystoneGraphqlOptions
{
    public bool? Debug { get; set; }

    public string? Path { get; set; }

    public KeystoneJsValue<bool, KeystoneGraphqlPlaygroundType>? Playground { get; set; }

    public Dictionary<string, object>? ApolloConfig { get; set; }

    public string? SchemaPath { get; set; }
}

public class KeystoneUiSettings
{
    public bool? IsDisabled { get; set; }

    public KeystoneJsFunction? IsAccessAllowed { get; set; }

    public string[]? PublicPages { get; set; }

    public KeystoneJsFunction? PageMiddleware { get; set; }

    public KeystoneJsFunction[]? GetAdditionalFiles { get; set; }
}

public class KeystoneCorsOptions : Dictionary<string, object>
{
    public KeystoneCorsOptions(
        KeystoneJsValue<string, KeystoneJsObject, KeystoneJsValue<string, KeystoneJsObject>[]> origin,
        bool? credentials = null)
    {
        base[nameof(origin)] = origin;
        if (credentials != null)
            base[nameof(credentials)] = credentials.Value;
    }
}

public class KeystoneServerOptions
{
    public KeystoneCorsOptions? Cors { get; set; }

    public int? Port { get; set; }

    public Dictionary<string, object>? Options { get; set; }

    public int? MaxFileSize { get; set; }

    public KeystoneJsFunction? ExtendExpressApp { get; set; }

    public KeystoneJsFunction? ExtendHttpServer { get; set; }
}

public class KeystoneConfig(DbContext dbContext, string baseDir) : KeystoneJsFunctionObjectCall(KeystoneImport.Core, "config")
{
    public KeystoneDb Db { get; } = new(dbContext, baseDir);

    public KeystoneGraphqlOptions? Graphql { get; set; }

    public KeystoneServerOptions? Server { get; set; }

    public KeystoneSession? Session { get; set; }

    public KeystoneUiSettings? Ui { get; set; }

    public KeystoneJsFunction? ExtendGraphqlSchema { get; set; }

    public Dictionary<string, KeystoneStorageConfig>? Storage { get; set; }

    public KeystoneCollection<KeystoneList> Lists { get; } = new(x => x.ClrType.Name);
}

public class KeystoneGraphqlCacheHint
{
    public int MaxAge { get; set; }

    public KeystoneGraphqlCacheScope Scope { get; set; }
}

public class KeystoneFieldGraphqlOmit
{
    public bool? Read { get; set; }

    public bool? Create { get; set; }

    public bool? Update { get; set; }
}

public class KeystoneListGraphqlOmit
{
    public bool? Query { get; set; }

    public bool? Create { get; set; }

    public bool? Update { get; set; }

    public bool? Delete { get; set; }
}

public class KeystoneListGraphqlOptions
{
    public string? Description { get; set; }

    public string? Plural { get; set; }

    public string? ItemQueryName { get; set; }

    public string? ListQueryName { get; set; }

    public int? MaxTake { get; set; }

    public KeystoneGraphqlCacheHint? CacheHint { get; set; }

    public KeystoneJsValue<bool, KeystoneListGraphqlOmit>? Omit { get; set; }
}

public class KeystoneFieldDefaultValue
{
    public KeystoneFieldDefaultValueKind Kind { get; set; }
}

public class KeystoneIdFieldOptions
{
    public KeystoneIdFieldKind Kind { get; set; }

    public KeystoneIdFieldType? Type { get; set; }
}

public interface IKeystoneDbContext
{
    Task ConfigureKeystoneAsync(KeystoneConfig config);
}

internal interface IKeystoneRef
{
    string KeystoneName { get; }
}

public sealed class KeystoneRef<T> : IKeystoneRef
{
    private readonly string keystoneName;
    internal KeystoneRef(string keystoneName) => this.keystoneName = keystoneName;
    public override string ToString() => ((IKeystoneRef)this).KeystoneName;
    public static implicit operator KeystoneRef<T>(T value) => new(value?.ToString()!);
    string IKeystoneRef.KeystoneName => this.keystoneName;
}

public sealed class KeystoneCollection<T> : KeyedCollection<string, T>, IReadOnlyDictionary<string, T>
{
    private readonly Func<T, string> getKeyForItem;

    internal KeystoneCollection(Func<T, string> getKeyForItem)
    {
        this.getKeyForItem = getKeyForItem;
    }

    protected override string GetKeyForItem(T item) => this.getKeyForItem(item);

    IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
    {
        return this.Dictionary?.GetEnumerator() ?? Enumerable.Empty<KeyValuePair<string, T>>().GetEnumerator();
    }

    bool IReadOnlyDictionary<string, T>.ContainsKey(string key) => this.Contains(key);

    IEnumerable<string> IReadOnlyDictionary<string, T>.Keys => this.Dictionary?.Keys ?? [];

    IEnumerable<T> IReadOnlyDictionary<string, T>.Values => this.Items;

    public KeystoneRef<T>[] ToRefArray() => this.Items.Select(x => (KeystoneRef<T>)x).ToArray();
}