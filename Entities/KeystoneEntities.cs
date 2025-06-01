using System.Reflection;
using Keystone4Net.Common;
using Keystone4Net.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Keystone4Net.Entities;

public class KeystoneListInitialSortOptions
{
    public string Field { get; set; } = string.Empty;

    public KeystoneSortDirection Direction { get; set; }
}

public class KeystoneListViewOptions
{
    public KeystoneFieldMode? DefaultFieldMode { get; set; }

    public string[] InitialColumns { get; set; } = [];

    public KeystoneListInitialSortOptions? InitialSort { get; set; }

    public int? PageSize { get; set; }
}

public class KeystoneViewOptions
{
    public KeystoneFieldMode DefaultFieldMode { get; set; }
}

public class KeystoneItemViewOptions : KeystoneViewOptions
{
    public KeystoneFieldPosition FieldPosition { get; set; }
}

public class KeystoneListUiOptions
{
    public string? Label { get; set; }

    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }

    public string? Description { get; set; }

    public KeystoneJsFunction? IsHidden { get; set; }

    public KeystoneJsFunction? HideCreate { get; set; }

    public KeystoneJsFunction? HideDelete { get; set; }

    public KeystoneViewOptions? CreateView { get; set; }

    public KeystoneItemViewOptions? ItemView { get; set; }

    public KeystoneListViewOptions? ListView { get; set; }

    public string? Singular { get; set; }

    public string? Plural { get; set; }

    public string? Path { get; set; }
}

public class KeystoneFieldGraphqlIsNonNull
{
    public bool Read { get; set; }

    public bool Create { get; set; }

    public bool Update { get; set; }
}

public class KeystoneFieldGraphqlOmit
{
    public bool Read { get; set; }

    public bool Create { get; set; }

    public bool Update { get; set; }
}

public class KeystoneFieldGraphqlOptions
{
    public KeystoneGraphqlCacheHint? CacheHint { get; set; }

    public KeystoneFieldGraphqlIsNonNull? IsNonNull { get; set; }

    public KeystoneFieldGraphqlOmit? Omit { get; set; }
}

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
            {
                this.Url = "file:" + Path.GetRelativePath(Path.Combine(entryDir, baseDir), absolute).Replace(Path.DirectorySeparatorChar, '/');
            }
            else if (absolute.StartsWith(entryDir, StringComparison.OrdinalIgnoreCase))
            {
                this.Url = "file:" + Path.GetRelativePath(entryDir, absolute).Replace(Path.DirectorySeparatorChar, '/');
            }
        }
    }

    public KeystoneDbProvider Provider { get; set; }

    public string Url { get; set; }

    public KeystoneJsFunction? OnConnect { get; set; }

    public bool? EnableLogging { get; set; }

    public KeystoneIdFieldOptions? IdField { get; set; }

    public string? ShadowDatabaseUrl { get; set; }

    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public class KeystoneTextMatchOptions
{
    public string Regex { get; set; } = string.Empty;

    public string? Explanation { get; set; }
}

public class KeystoneGraphqlCacheHint
{
    public int MaxAge { get; set; }

    public string? Scope { get; set; }
}

internal sealed class KeystoneListDb
{
    public string? Map { get; set; }

    public KeystoneIdFieldOptions? IdField { get; set; }

    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public class KeystoneIdFieldOptions
{
    public string Kind { get; set; } = string.Empty;

    public string? Type { get; set; }

    public int? Bytes { get; set; }

    public string? Encoding { get; set; }

    public int? Version { get; set; }

    public int? Length { get; set; }
}

public class KeystoneGraphqlOmit
{
    public bool Query { get; set; }

    public bool Create { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }
}

public class KeystoneListGraphqlOptions
{
    public string? Description { get; set; }

    public string? Plural { get; set; }

    public string? ItemQueryName { get; set; }

    public string? ListQueryName { get; set; }

    public int? MaxTake { get; set; }

    public KeystoneGraphqlCacheHint? CacheHint { get; set; }

    public KeystoneGraphqlOmit? Omit { get; set; }
}


public class KeystoneUiSettings
{
    public bool IsDisabled { get; set; }

    public KeystoneJsFunction? IsAccessAllowed { get; set; }

    public string[]? PublicPages { get; set; }

    public string? BasePath { get; set; }

    public KeystoneJsFunction? PageMiddleware { get; set; }

    public KeystoneJsFunction? GetAdditionalFiles { get; set; }
}



public class KeystoneIronOptions
{
    public Dictionary<string, object?>? Values { get; set; }
}

public class KeystoneFieldUiOptions
{
    public string? Views { get; set; }
    public string? Description { get; set; }
    public KeystoneViewOptions? CreateView { get; set; }
    public KeystoneItemViewOptions? ItemView { get; set; }
    public KeystoneViewOptions? ListView { get; set; }
}
public class KeystoneTypesConfig
{
    public string Path { get; set; } = string.Empty;
}

public class KeystoneGraphqlOptions
{
    public bool? Debug { get; set; }
    public string? Path { get; set; }
    public object? Playground { get; set; }
    public object? ApolloConfig { get; set; }
    public string? SchemaPath { get; set; }
}

public class KeystoneServerOptions
{
    public object? Cors { get; set; }
    public int? Port { get; set; }
    public object? Options { get; set; }
    public int? MaxFileSize { get; set; }
    public KeystoneJsFunction? ExtendExpressApp { get; set; }
    public KeystoneJsFunction? ExtendHttpServer { get; set; }
}

public class KeystoneStorageConfig
{
    public string Kind { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool? Preserve { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
    public KeystoneJsFunction? GenerateUrl { get; set; }
    public KeystoneServerRoute? ServerRoute { get; set; }
    public string? BucketName { get; set; }
    public string? Region { get; set; }
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }
    public string? PathPrefix { get; set; }
    public string? Endpoint { get; set; }
    public bool? ForcePathStyle { get; set; }
    public KeystoneSignedOptions? Signed { get; set; }
    public string? Acl { get; set; }
}

public class KeystoneServerRoute
{
    public string Path { get; set; } = string.Empty;
}

public class KeystoneSignedOptions
{
    public int Expiry { get; set; }
}
