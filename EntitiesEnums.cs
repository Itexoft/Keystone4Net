namespace Keystone4Net.Entities;

public class KeystoneBigIntFieldOptions : KeystoneFieldOptions
{
    public long? DefaultValue { get; set; }
    internal KeystoneBigIntDbOptions? Db { get; set; }
    public KeystoneBigIntValidationOptions? Validation { get; set; }
}

internal class KeystoneBigIntDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneBigIntValidationOptions
{
    public bool IsRequired { get; set; }
    public long? Min { get; set; }
    public long? Max { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneBytesFieldOptions : KeystoneFieldOptions
{
    public byte[]? DefaultValue { get; set; }
    internal KeystoneBytesDbOptions? Db { get; set; }
}

internal class KeystoneBytesDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneCalendarDayFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    internal KeystoneCalendarDayDbOptions? Db { get; set; }
    public KeystoneCalendarDayValidationOptions? Validation { get; set; }
}

internal class KeystoneCalendarDayDbOptions
{
    public bool? IsNullable { get; set; }
}

public class KeystoneCalendarDayValidationOptions
{
    public bool IsRequired { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneCheckboxFieldOptions : KeystoneFieldOptions
{
    public bool? DefaultValue { get; set; }
    internal KeystoneCheckboxDbOptions? Db { get; set; }
}

internal class KeystoneCheckboxDbOptions
{
}

namespace Keystone4Net.Entities;

public class KeystoneCloudinaryImageFieldOptions : KeystoneFieldOptions
{
    internal KeystoneCloudinaryDbOptions? Db { get; set; }
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
}

internal class KeystoneCloudinaryDbOptions
{
    public string? Map { get; set; }
}

public class KeystoneCloudinaryCredentials
{
    public string CloudName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string? Folder { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneDecimalFieldOptions : KeystoneFieldOptions
{
    public decimal? DefaultValue { get; set; }
    internal KeystoneDecimalDbOptions? Db { get; set; }
    public KeystoneDecimalValidationOptions? Validation { get; set; }
}

internal class KeystoneDecimalDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneDecimalValidationOptions
{
    public bool IsRequired { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneDocumentField : KeystoneField
{
    public KeystoneDocumentField() : base("document")
    {
    }
    internal KeystoneDocumentDbOptions? Db { get; set; }
    public object? Relationships { get; set; }
    public object? ComponentBlocks { get; set; }
    public object? Formatting { get; set; }
    public object? Links { get; set; }
    public bool? Dividers { get; set; }
    public int[][]? Layouts { get; set; }
}

internal class KeystoneDocumentDbOptions
{
    public string? Map { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneFileField : KeystoneField
{
    public KeystoneFileField() : base("file")
    {
    }
    public string Storage { get; set; } = string.Empty;
}

namespace Keystone4Net.Entities;

public class KeystoneFloatFieldOptions : KeystoneFieldOptions
{
    public double? DefaultValue { get; set; }
    internal KeystoneFloatDbOptions? Db { get; set; }
    public KeystoneFloatValidationOptions? Validation { get; set; }
}

internal class KeystoneFloatDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneFloatValidationOptions
{
    public bool IsRequired { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneImageField : KeystoneField
{
    public KeystoneImageField() : base("image")
    {
    }
    public string Storage { get; set; } = string.Empty;
}

namespace Keystone4Net.Entities;

public class KeystoneIntegerFieldOptions : KeystoneFieldOptions
{
    public int? DefaultValue { get; set; }
    internal KeystoneIntegerDbOptions? Db { get; set; }
    public KeystoneIntegerValidationOptions? Validation { get; set; }
}

internal class KeystoneIntegerDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneIntegerValidationOptions
{
    public bool IsRequired { get; set; }
    public int? Min { get; set; }
    public int? Max { get; set; }
}

using System.Text.Json;

namespace Keystone4Net.Entities;

public class KeystoneJsonFieldOptions : KeystoneFieldOptions
{
    public JsonElement? DefaultValue { get; set; }
    internal KeystoneJsonDbOptions? Db { get; set; }
}

internal class KeystoneJsonDbOptions
{
    public string? Map { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneMultiselectFieldOptions : KeystoneFieldOptions
{
    public string[]? DefaultValue { get; set; }
    public KeystoneSelectOption[]? Options { get; set; }
    internal KeystoneMultiselectDbOptions? Db { get; set; }
}

internal class KeystoneMultiselectDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystonePasswordFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    internal KeystonePasswordDbOptions? Db { get; set; }
    public KeystonePasswordValidationOptions? Validation { get; set; }
}

internal class KeystonePasswordDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystonePasswordValidationOptions
{
    public bool IsRequired { get; set; }
    public bool RejectCommon { get; set; }
    public KeystoneTextMatchOptions? Match { get; set; }
    public KeystoneTextLengthOptions? Length { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneRelationshipField : KeystoneField<KeystoneRelationshipUiOptions>
{
    public KeystoneRelationshipField() : base("relationship")
    {
    }
    public bool? Many { get; set; }
    public string Ref { get; set; } = string.Empty;
    internal KeystoneRelationshipDbOptions? Db { get; set; }
}

internal class KeystoneRelationshipDbOptions
{
    public string? RelationName { get; set; }
    public object? ForeignKey { get; set; }
}

public class KeystoneRelationshipUiOptions
{
    public bool? HideCreate { get; set; }
    public KeystoneDisplayMode? DisplayMode { get; set; }
    public string? LabelField { get; set; }
    public string[]? SearchFields { get; set; }
    public string[]? CardFields { get; set; }
    public bool? LinkToItem { get; set; }
    public KeystoneRemoveMode? RemoveMode { get; set; }
    public KeystoneInlineCreateOptions? InlineCreate { get; set; }
    public KeystoneInlineEditOptions? InlineEdit { get; set; }
    public KeystoneInlineConnectOptions? InlineConnect { get; set; }
}

public class KeystoneInlineCreateOptions
{
    public string[] Fields { get; set; } = [];
}

public class KeystoneInlineEditOptions
{
    public string[] Fields { get; set; } = [];
}

public class KeystoneInlineConnectOptions
{
    public string? LabelField { get; set; }
    public string[]? SearchFields { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneSelectFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    public KeystoneSelectOption[]? Options { get; set; }
    internal KeystoneSelectDbOptions? Db { get; set; }
    public KeystoneSelectValidationOptions? Validation { get; set; }
}

public class KeystoneSelectOption
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

internal class KeystoneSelectDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneSelectValidationOptions
{
    public bool IsRequired { get; set; }
}

using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public class KeystoneTextFieldOptions : KeystoneFieldOptions<KeystoneTextUiOptions>
{
    public string? DefaultValue { get; set; }
    internal KeystoneTextDbOptions? Db { get; set; }
    public KeystoneTextValidationOptions? Validation { get; set; }
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

internal class KeystoneTextDbOptions
{
    public string? Map { get; set; }
    public bool IsNullable { get; set; }
    public string? NativeType { get; set; }
    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public class KeystoneTextUiOptions : KeystoneFieldUiOptions
{
    public KeystoneDisplayMode DisplayMode { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneTimestampFieldOptions : KeystoneFieldOptions
{
    public DateTime? DefaultValue { get; set; }
    internal KeystoneTimestampDbOptions? Db { get; set; }
    public KeystoneTimestampValidationOptions? Validation { get; set; }
}

internal class KeystoneTimestampDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneTimestampValidationOptions
{
    public bool IsRequired { get; set; }
}

namespace Keystone4Net.Entities;

public class KeystoneVirtualField : KeystoneField<KeystoneVirtualUiOptions>
{
    public KeystoneVirtualField() : base("virtual")
    {
    }
    public KeystoneJsFunction? Field { get; set; }
}

public class KeystoneVirtualUiOptions : KeystoneFieldUiOptions
{
    public string? Query { get; set; }
}

namespace Keystone4Net.Entities;

public interface IKeystone
{
    void ConfigureKeystone(KeystoneConfig config);
}

using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public class KeystoneFieldAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneFieldAccess AllowAll { get; } = new(nameof(AllowAll));
    public static KeystoneFieldAccess DenyAll { get; } = new(nameof(DenyAll));
    public static KeystoneFieldAccess AllOperations { get; } = new(nameof(AllOperations));
    public static KeystoneFieldAccess Unfiltered { get; } = new(nameof(Unfiltered));
}

public class KeystoneListAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneListAccess AllowAll { get; } = new(nameof(AllowAll));
    public static KeystoneListAccess DenyAll { get; } = new(nameof(DenyAll));
    public static KeystoneListAccess AllOperations { get; } = new(nameof(AllOperations));
    public static KeystoneListAccess Unfiltered { get; } = new(nameof(Unfiltered));
}

using Keystone4Net.Enums;
using Microsoft.EntityFrameworkCore;

namespace Keystone4Net.Entities;

public class KeystoneConfig : KeystoneJsFunctionPropArgCall
{
    internal KeystoneConfig(DbContext dbContext, string? baseDir) : base(KeystoneImportObjects.Core, "config", null)
    {
        this.Db = new(dbContext, baseDir);
    }

    internal KeystoneDb Db { get; }

    public KeystoneTypesConfig? Types { get; set; }

    public KeystoneGraphqlOptions? Graphql { get; set; }

    public KeystoneServerOptions? Server { get; set; }

    public KeystoneSession? Session { get; set; }

    public KeystoneUiSettings? Ui { get; set; }

    public KeystoneJsFunction? ExtendGraphqlSchema { get; set; }

    public Dictionary<string, KeystoneStorageConfig>? Storage { get; set; }

    public bool Telemetry { get; set; }

    public Dictionary<string, KeystoneList> Lists { get; } = [];

    public void Add<T>(KeystoneList<T> value)
    {
        var typeName = typeof(T).Name;
        this.Lists.Add(typeName, value);
    }
}

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

internal class KeystoneDb
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

using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public abstract class KeystoneField<TUiOptions> : KeystoneJsFunctionPropArgCall where TUiOptions : KeystoneFieldUiOptions
{
    protected KeystoneField(string name) : base(KeystoneImportObjects.Fields, name, null)
    {
    }

    public KeystoneFieldAccess? Access { get; set; }
    public KeystoneFieldHooks? Hooks { get; set; }
    public string? Label { get; set; }
    public bool? IsFilterable { get; set; }
    public bool? IsOrderable { get; set; }
    public TUiOptions? Ui { get; set; }
    public KeystoneIndex? IsIndexed { get; set; }
    public KeystoneFieldGraphqlOptions? Graphql { get; set; }
}

public abstract class KeystoneField : KeystoneField<KeystoneFieldUiOptions>
{
    protected KeystoneField(string name) : base(name)
    {
    }
}

namespace Keystone4Net.Entities;

public abstract class KeystoneHooks
{
    public KeystoneJsFunction? ResolveInput { get; set; }

    public KeystoneJsFunction? ResolveInputCreate { get; set; }

    public KeystoneJsFunction? ResolveInputUpdate { get; set; }

    public KeystoneJsFunction? Validate { get; set; }

    public KeystoneJsFunction? ValidateCreate { get; set; }

    public KeystoneJsFunction? ValidateUpdate { get; set; }

    public KeystoneJsFunction? ValidateDelete { get; set; }

    public KeystoneJsFunction? BeforeOperation { get; set; }

    public KeystoneJsFunction? BeforeOperationCreate { get; set; }

    public KeystoneJsFunction? BeforeOperationUpdate { get; set; }

    public KeystoneJsFunction? BeforeOperationDelete { get; set; }

    public KeystoneJsFunction? AfterOperation { get; set; }

    public KeystoneJsFunction? AfterOperationCreate { get; set; }

    public KeystoneJsFunction? AfterOperationUpdate { get; set; }

    public KeystoneJsFunction? AfterOperationDelete { get; set; }
}

public class KeystoneListHooks : KeystoneHooks
{
}

public class KeystoneFieldHooks : KeystoneHooks
{
}

using System.Reflection;
using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public class KeystoneJsFunction(string body, params string[] args)
{
    public KeystoneJsFunction(bool body, params string[] args) : this(body.ToString().ToLower(), args)
    {
    }

    public string Body { get; } = body;

    public string[] Args { get; } = args;
}

public class KeystoneJsObject(KeystoneImportObjects imports, string name)
{
    public string Name { get; } = name;
    public KeystoneImportObjects Imports { get; } = imports;

    public override string ToString() => this.Name;
}

public abstract class KeystoneJsFunctionCall(KeystoneImportObjects imports, string name)
{
    public KeystoneImportObjects Imports { get; } = imports;

    public string Name { get; } = name;

    public abstract object?[] Args { get; }
}

public abstract class KeystoneJsFunctionPropArgCall : KeystoneJsFunctionCall
{
    private readonly PropertyInfo[] properties;
    private readonly object? propObj;

    protected KeystoneJsFunctionPropArgCall(KeystoneImportObjects imports, string name, object? propObj) : base(imports, name)
    {
        this.propObj = propObj ?? this;
        this.properties = this.propObj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(x => x.Name != nameof(this.Args) && x.Name != nameof(this.Name) && x.Name != nameof(this.Imports)).ToArray();
    }

    public override object?[] Args =>
    [
        this.properties
            .Select(p => (name: Utils.ToCamelCase(p.Name), value: p.GetValue(this.propObj, null)))
            .Where(x => x.value != null)
            .ToDictionary(x => x.name, x => x.value)
    ];
}

using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public abstract class KeystoneList(Type type) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Core, "list", null)
{
    [JsonIgnore]
    internal Type Type { get; } = type;

    internal KeystoneListDb Db { get; } = new();

    public KeystoneListAccess? Access { get; set; } = KeystoneListAccess.AllowAll;

    public KeystoneListUiOptions? Ui { get; set; }

    public KeystoneListGraphqlOptions? Graphql { get; set; }

    public KeystoneListHooks? Hooks { get; set; }

    public string? Description { get; set; }

    public bool IsSingleton { get; set; }

    public bool? DefaultIsFilterable { get; set; }

    public bool? DefaultIsOrderable { get; set; }

    public Dictionary<string, KeystoneField> Fields { get; } = [];

    public string Add(string key, KeystoneField value)
    {
        key = Utils.ToCamelCase(key);
        this.Fields.Add(key, value);

        return key;
    }

}

public sealed class KeystoneList<T>() : KeystoneList(typeof(T))
{
}

using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public abstract class KeystoneSession(string name) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Session, name, null)
{
}

public abstract class KeystoneCookieSession(string name) : KeystoneSession(name)
{
    public string Secret { get; set; } = string.Empty;

    public KeystoneIronOptions? IronOptions { get; set; }

    public int MaxAge { get; set; }

    public string? CookieName { get; set; }

    public bool? Secure { get; set; }

    public string? Path { get; set; }

    public string? Domain { get; set; }

    public KeystoneCookieSameSite? SameSite { get; set; }
}

public class KeystoneStatelessSession() : KeystoneCookieSession("statelessSessions")
{
}

public class KeystoneStoredSession() : KeystoneCookieSession("storedSessions")
{
    public KeystoneJsFunction? Store { get; set; }
}



namespace Keystone4Net.Enums;

public enum KeystoneCookieSameSite
{
    True,
    False,
    Lax,
    Strict,
    None
}
namespace Keystone4Net.Enums;

public enum KeystoneDbProvider
{
    Sqlite,
    Postgresql,
    Mysql
}
namespace Keystone4Net.Enums;

public enum KeystoneDisplayMode
{
    Input,
    Textarea,
    Select,
    Cards,
    Count
}
namespace Keystone4Net.Enums;

public enum KeystoneFieldMode
{
    Edit,
    Read,
    Hidden
}


namespace Keystone4Net.Enums;

public enum KeystoneFieldPosition
{
    Form,
    Sidebar
}


namespace Keystone4Net.Enums;

public enum KeystoneImportObjects
{
    Core,
    Access,
    Fields,
    Session
}
namespace Keystone4Net.Enums;

public enum KeystoneIndex
{
    None,
    Indexed,
    Unique
}

namespace Keystone4Net.Enums;

public enum KeystoneRemoveMode
{
    Disconnect,
    None
}

namespace Keystone4Net.Enums;

public enum KeystoneSortDirection
{
    Asc,
    Desc
}


