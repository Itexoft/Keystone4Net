using System.Reflection;
using Keystone4Net.Common;
using Keystone4Net.Enums;
using Microsoft.EntityFrameworkCore;

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

public class KeystoneListUiOptions
{
    public string? Label { get; set; }

    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }

    public string? Description { get; set; }

    public bool HideNavigation { get; set; }

    public bool HideCreate { get; set; }

    public bool HideDelete { get; set; }

    public KeystoneViewOptions? CreateView { get; set; }

    public KeystoneViewOptions? ItemView { get; set; }

    public KeystoneListViewOptions? ListView { get; set; }

    public string? Singular { get; set; }

    public string? Plural { get; set; }

    public string? Path { get; set; }
}


public class KeystoneTextFieldOptions : IKeystoneFieldOptions
{
    public object? Access { get; set; }

    public KeystoneFieldHooks? Hooks { get; set; }

    public string? Label { get; set; }

    public bool? IsFilterable { get; set; }

    public bool? IsOrderable { get; set; }

    public string? DefaultValue { get; set; }

    public KeystoneTextDbOptions? Db { get; set; }

    public KeystoneTextValidationOptions? Validation { get; set; }

    public KeystoneTextUiOptions? Ui { get; set; }

    public KeystoneIndex? IsIndexed { get; set; }

    public KeystoneFieldGraphqlOptions? Graphql { get; set; }
}

public class KeystoneFieldUiOptions
{
    public string? Views { get; set; }

    public string? Description { get; set; }

    public KeystoneViewOptions? CreateView { get; set; }

    public KeystoneItemKeystoneViewOptions? ItemView { get; set; }

    public KeystoneViewOptions? ListView { get; set; }
}

public class KeystoneItemKeystoneViewOptions : KeystoneViewOptions
{
    public KeystoneFieldPosition FieldPosition { get; set; }
}

public class KeystoneTextValidationOptions
{
    public bool IsRequired { get; set; }

    public KeystoneTextLengthOptions? Length { get; set; }

    public KeystoneTextMatchOptions? Match { get; set; }
}

public class KeystoneTextLengthOptions
{
    public int Min { get; set; }

    public int? Max { get; set; }
}

public class KeystoneTextDbOptions
{
    public string? Map { get; set; }

    public bool IsNullable { get; set; }

    public string? NativeType { get; set; }

    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
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

public class KeystoneTextUiOptions : KeystoneFieldUiOptions
{
    public KeystoneDisplayMode DisplayMode { get; set; }
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

public abstract class KeystoneSession(string name) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Session, name, null)
{
}

public abstract class KeystoneCookieSession(string name) : KeystoneSession(name)
{
    public string Secret { get; set; } = string.Empty;

    public object? IronOptions { get; set; }

    public int MaxAge { get; set; }

    public string? CookieName { get; set; }

    public bool? Secure { get; set; }

    public string? Path { get; set; }

    public string? Domain { get; set; }

    public object? SameSite { get; set; }
}

public class KeystoneStatelessSession() : KeystoneCookieSession("statelessSessions")
{
}

public class KeystoneStoredSession() : KeystoneCookieSession("storedSessions")
{
    public object? Store { get; set; }
}

public class KeystoneField(KeystoneFieldType type, IKeystoneFieldOptions? options)
    : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Fields, Utils.ToCamelCase(type), options)
{
    public KeystoneFieldType Type { get; set; } = type;

    public IKeystoneFieldOptions? Options { get; set; } = options;
}

public class KeystoneListAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneListAccess AllowAll { get; } = new(nameof(AllowAll));

    public static KeystoneListAccess DenyAll { get; } = new(nameof(DenyAll));

    public static KeystoneListAccess AllOperations { get; } = new(nameof(AllOperations));

    public static KeystoneListAccess Unfiltered { get; } = new(nameof(Unfiltered));
}
