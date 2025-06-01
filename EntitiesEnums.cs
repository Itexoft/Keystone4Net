using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Microsoft.EntityFrameworkCore;

public enum KeystoneImportObjects
{
    Core,
    Access,
    Fields,
    Session,
    Graphql
}

public enum KeystoneCookieSameSite
{
    Lax,
    Strict,
    None
}

public enum KeystoneDbProvider
{
    Sqlite,
    Postgresql,
    Mysql
}

public enum KeystoneRelationshipDisplayMode
{
    Select,
    Cards,
    Count
}

public enum KeystoneTextDisplayMode
{
    Input,
    Textarea
}

public enum KeystoneFieldMode
{
    Edit,
    Read,
    Hidden
}

public enum KeystoneFieldPosition
{
    Form,
    Sidebar
}

public enum KeystoneSortDirection
{
    [JsonStringEnumMemberName("ASC")] Asc,
    [JsonStringEnumMemberName("DESC")] Desc
}

public enum KeystoneSelectValueType
{
    String,
    Enum,
    Integer
}

public enum KeystoneRemoveMode
{
    Disconnect,
    None
}

public enum KeystoneStorageKind
{
    Local,
    S3
}

public enum KeystoneStorageType
{
    File,
    Image
}

public abstract class KeystoneJsValue
{
    internal object? Value { get; }
    private protected KeystoneJsValue(object? value) => this.Value = value;
    public override string? ToString() => this.Value?.ToString();
}

public class KeystoneJsValue<T1, T2> : KeystoneJsValue
{
    private protected KeystoneJsValue(object? value) : base(value) { }
    public static implicit operator KeystoneJsValue<T1, T2>(T1 value) => new(value);
    public static implicit operator KeystoneJsValue<T1, T2>(T2 value) => new(value);

}

public class KeystoneJsValue<T1, T2, T3> : KeystoneJsValue<T1, T2>
{
    private protected KeystoneJsValue(object? value) : base(value) { }
    public static implicit operator KeystoneJsValue<T1, T2, T3>(T3 value) => new(value);
}

public class KeystoneJsObject(KeystoneImportObjects? imports, string name)
{
    public KeystoneJsObject(string name) : this(null, name) { }

    public KeystoneImportObjects? Imports { get; } = imports;

    public string Name { get; } = name;

    public override string ToString()
    {
        return this.Name;
    }
}

public sealed class KeystoneJsFunction(string body, params string[] args)
{
    public string Body { get; } = body;

    public string[] Args { get; } = args;
    
    public static implicit operator KeystoneJsFunction(bool body) => new(body.ToString().ToLower());
}

public abstract class KeystoneJsFunctionCall(KeystoneImportObjects imports, string name)
{
    public KeystoneImportObjects Imports { get; } = imports;

    public string Name { get; } = name;

    public abstract object?[] Arguments { get; }
}

public abstract class KeystoneJsFunctionPropArgCall : KeystoneJsFunctionCall
{
    private readonly PropertyInfo[] props;

    protected KeystoneJsFunctionPropArgCall(KeystoneImportObjects imports, string name) : base(imports, name)
    {
        this.props = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.Name is not (nameof(this.Arguments) or nameof(this.Name) or nameof(this.Imports))).ToArray();
    }

    public override object?[] Arguments =>
    [
        this.props.Select(p => (Utils.ToCamelCase(p.Name), p.GetValue(this))).Where(x => x.Item2 != null)
            .ToDictionary(x => x.Item1, x => x.Item2)
    ];
}

public sealed class KeystoneFieldAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneFieldAccess AllowAll { get; } = new(nameof(AllowAll));

    public static KeystoneFieldAccess DenyAll { get; } = new(nameof(DenyAll));
}

public sealed class KeystoneListAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneListAccess AllowAll { get; } = new(nameof(AllowAll));

    public static KeystoneListAccess DenyAll { get; } = new(nameof(DenyAll));
}

public sealed class KeystoneFieldGraphqlIsNonNull
{
    public bool? Read { get; set; }

    public bool? Create { get; set; }

    public bool? Update { get; set; }
}

public sealed class KeystoneGraphqlOmit
{
    public bool? Read { get; set; }

    public bool? Create { get; set; }

    public bool? Update { get; set; }

    public bool? Delete { get; set; }
}

public sealed class KeystoneFieldCreateViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? FieldMode { get; set; }
}

public sealed class KeystoneFieldItemViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? FieldMode { get; set; }

    public KeystoneFieldPosition? FieldPosition { get; set; }
}

public sealed class KeystoneFieldListViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? FieldMode { get; set; }
}

public abstract class KeystoneFieldUiOptions
{
    public string? Views { get; set; }

    public KeystoneFieldCreateViewOptions? CreateView { get; set; }

    public KeystoneFieldItemViewOptions? ItemView { get; set; }

    public KeystoneFieldListViewOptions? ListView { get; set; }
}

public abstract class FieldDbOptions
{
    internal string? Map { get; set; }

    internal bool? IsNullable { get; set; }
}

public abstract class KeystoneField<TDb, TUi>(string n) : KeystoneField(n, new TDb()) 
    where TDb : FieldDbOptions, new() 
    where TUi : KeystoneFieldUiOptions
{
    public new TDb Db => (TDb)base.Db;

    public TUi? Ui { get; set; }
}

public sealed class KeystoneLengthOptions
{
    public int? Min { get; set; }

    public int? Max { get; set; }
}

public sealed class KeystoneMatchOptions
{
    public string Regex { get; set; } = string.Empty;

    public string? Explanation { get; set; }
}

public sealed class KeystoneTextDbOptions : FieldDbOptions
{
    public string? NativeType { get; set; }
}

public sealed class KeystoneTextValidation
{
    public bool? IsRequired { get; set; }

    public KeystoneLengthOptions? Length { get; set; }

    public KeystoneMatchOptions? Match { get; set; }
}

public sealed class KeystoneTextUiOptions : KeystoneFieldUiOptions
{
    public KeystoneTextDisplayMode? DisplayMode { get; set; }
}

public sealed class KeystoneTextField() : KeystoneField<KeystoneTextDbOptions, KeystoneTextUiOptions>("text")
{
    public string? DefaultValue { get; set; }

    public KeystoneTextValidation? Validation { get; set; }
}

public sealed class KeystonePasswordDbOptions : FieldDbOptions
{
}

public sealed class KeystonePasswordValidation
{
    public bool? IsRequired { get; set; }

    public KeystoneLengthOptions? Length { get; set; }

    public KeystoneMatchOptions? Match { get; set; }

    public bool? RejectCommon { get; set; }
}

public sealed class KeystonePasswordField() : KeystoneField<KeystonePasswordDbOptions, KeystoneFieldUiOptions>("password")
{
    public KeystonePasswordValidation? Validation { get; set; }

    public KeystoneJsFunction? Bcrypt { get; set; }
}

public sealed class KeystoneCheckboxDbOptions : FieldDbOptions
{
}

public sealed class KeystoneCheckboxField() : KeystoneField<KeystoneCheckboxDbOptions, KeystoneFieldUiOptions>("checkbox")
{
    public bool? DefaultValue { get; set; }
}

public sealed class KeystoneIntegerDbOptions : FieldDbOptions
{
}

public sealed class KeystoneIntegerValidation
{
    public bool? IsRequired { get; set; }

    public int? Min { get; set; }

    public int? Max { get; set; }
}

public sealed class KeystoneIntegerField() : KeystoneField<KeystoneIntegerDbOptions, KeystoneFieldUiOptions>("integer")
{
    public KeystoneJsValue<int, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneIntegerValidation? Validation { get; set; }
}

public sealed class KeystoneBigIntDbOptions : FieldDbOptions
{
}

public sealed class KeystoneBigIntValidation
{
    public bool? IsRequired { get; set; }

    public BigInteger? Min { get; set; }

    public BigInteger? Max { get; set; }
}

public sealed class KeystoneBigIntField() : KeystoneField<KeystoneBigIntDbOptions, KeystoneFieldUiOptions>("bigInt")
{
    public KeystoneJsValue<BigInteger, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneBigIntValidation? Validation { get; set; }
}

public sealed class KeystoneFloatDbOptions : FieldDbOptions
{
}

public sealed class KeystoneFloatValidation
{
    public bool? IsRequired { get; set; }

    public double? Min { get; set; }

    public double? Max { get; set; }
}

public sealed class KeystoneFloatField() : KeystoneField<KeystoneFloatDbOptions, KeystoneFieldUiOptions>("float")
{
    public double? DefaultValue { get; set; }

    public KeystoneFloatValidation? Validation { get; set; }
}

public sealed class KeystoneDecimalDbOptions : FieldDbOptions
{
}

public sealed class KeystoneDecimalValidation
{
    public bool? IsRequired { get; set; }

    public string? Min { get; set; }

    public string? Max { get; set; }
}

public sealed class KeystoneDecimalField() : KeystoneField<KeystoneDecimalDbOptions, KeystoneFieldUiOptions>("decimal")
{
    public string? DefaultValue { get; set; }

    public int? Precision { get; set; }

    public int? Scale { get; set; }

    public KeystoneDecimalValidation? Validation { get; set; }
}

public sealed class KeystoneJsonDbOptions : FieldDbOptions
{
}

public sealed class KeystoneJsonField() : KeystoneField<KeystoneJsonDbOptions, KeystoneFieldUiOptions>("json")
{
    public JsonElement? DefaultValue { get; set; }
}

public sealed class KeystoneCalendarDayDbOptions : FieldDbOptions
{
}

public sealed class KeystoneCalendarDayValidation
{
    public bool? IsRequired { get; set; }
}

public sealed class KeystoneCalendarDayField() : KeystoneField<KeystoneCalendarDayDbOptions, KeystoneFieldUiOptions>("calendarDay")
{
    public string? DefaultValue { get; set; }

    public KeystoneCalendarDayValidation? Validation { get; set; }
}

public sealed class KeystoneTimestampDbOptions : FieldDbOptions
{
    public bool? UpdatedAt { get; set; }
}

public sealed class KeystoneTimestampValidation
{
    public bool? IsRequired { get; set; }
}


public sealed class KeystoneTimestampField() : KeystoneField<KeystoneTimestampDbOptions, KeystoneFieldUiOptions>("timestamp")
{
    public KeystoneJsValue<string, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneTimestampValidation? Validation { get; set; }
}

public sealed class KeystoneSelectDbOptions : FieldDbOptions
{
}

public sealed class KeystoneSelectValidation
{
    public bool? IsRequired { get; set; }
}

public sealed class KeystoneSelectOption
{
    public required string Label { get; set; }

    public required KeystoneJsValue<string, int> Value { get; set; }
}

public enum KeystoneSelectUiMode
{
    Select,
    [JsonStringEnumMemberName("segmented-control")] SegmentedControl,
    Radio
}

public sealed class KeystoneSelectUiOptions : KeystoneFieldUiOptions
{
    public KeystoneSelectUiMode? DisplayMode { get; set; }
}

public sealed class KeystoneSelectField() : KeystoneField<KeystoneSelectDbOptions, KeystoneSelectUiOptions>("select")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];

    public KeystoneSelectValidation? Validation { get; set; }
}

public sealed class KeystoneMultiselectDbOptions : FieldDbOptions
{
}

public sealed class KeystoneMultiselectField() : KeystoneField<KeystoneMultiselectDbOptions, KeystoneFieldUiOptions>("multiselect")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>[]? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];
}

public sealed class KeystoneForeignKeyOptions
{
    public string? Map { get; set; }
}

public sealed class KeystoneRelationshipDbOptions : FieldDbOptions
{
    public KeystoneJsValue<bool, KeystoneForeignKeyOptions>? ForeignKey { get; set; }
}

public sealed class KeystoneInlineCreateOptions
{
    public string[] Fields { get; set; } = [];
}

public sealed class KeystoneInlineConnectOptions
{
    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }
}

public sealed class KeystoneRelationshipUiOptions : KeystoneFieldUiOptions
{
    public bool? HideCreate { get; set; }

    public KeystoneRelationshipDisplayMode? DisplayMode { get; set; }

    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }

    public string[]? CardFields { get; set; }

    public bool? LinkToItem { get; set; }

    public KeystoneRemoveMode? RemoveMode { get; set; }

    public KeystoneInlineCreateOptions? InlineCreate { get; set; }

    public KeystoneInlineCreateOptions? InlineEdit { get; set; }

    public KeystoneJsValue<bool, KeystoneInlineConnectOptions>? InlineConnect { get; set; }
}

public sealed class KeystoneRelationshipField()
    : KeystoneField<KeystoneRelationshipDbOptions, KeystoneRelationshipUiOptions>("relationship")
{
    public string Ref { get; set; } = string.Empty;

    public bool? Many { get; set; }
}

public sealed class KeystoneEmptyDbOptions : FieldDbOptions
{
}

public sealed class KeystoneFileField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("file")
{
    public string Storage { get; set; } = string.Empty;
}

public sealed class KeystoneImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("image")
{
    public string Storage { get; set; } = string.Empty;
}

public sealed class KeystoneCloudinaryCredentials
{
    public string CloudName { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string ApiSecret { get; set; } = string.Empty;

    public string? Folder { get; set; }
}

public sealed class KeystoneCloudinaryImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("cloudinaryImage")
{
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
}

public sealed class KeystoneDocumentDbOptions : FieldDbOptions
{
}

public enum KeystoneDocumentRelationshipKind
{
    Inline,
    Block,
    Prop
}

public sealed class KeystoneDocumentRelationship
{
    public KeystoneDocumentRelationshipKind Kind { get; set; }
    public required string ListKey { get; set; }

    public required string Label { get; set; }

    public string? Selection { get; set; }
}

public sealed class KeystoneDocumentFormattingInlineMarks
{
    public bool? Bold { get; set; }

    public bool? Italic { get; set; }

    public bool? Underline { get; set; }

    public bool? Strikethrough { get; set; }

    public bool? Code { get; set; }

    public bool? Superscript { get; set; }

    public bool? Subscript { get; set; }

    public bool? Keyboard { get; set; }
}

public sealed class KeystoneDocumentFormattingOptions
{
    public KeystoneDocumentFormattingInlineMarks? InlineMarks { get; set; }
    public KeystoneDocumentFormattingListTypes? ListTypes { get; set; }
    public KeystoneDocumentFormattingAlignment? Alignment { get; set; }
    public List<int> HeadingLevels { get; set; } = [];
    public KeystoneDocumentFormattingBlockTypes? BlockTypes { get; set; }
    public bool? SoftBreaks { get; set; }
}

public class KeystoneDocumentFormattingListTypes
{
    public bool? Ordered { get; set; }
    public bool? Unordered { get; set; }
}

public class KeystoneDocumentFormattingAlignment
{
    public bool? Center { get; set; }
    public bool? End { get; set; }
}

public class KeystoneDocumentFormattingBlockTypes
{
    public bool? Blockquote { get; set; }
    public bool? Code { get; set; }
}

public sealed class KeystoneDocumentLinksOptions
{
    public bool? Anchor { get; set; }
}

public sealed class KeystoneDocumentField() : KeystoneField<KeystoneDocumentDbOptions, KeystoneFieldUiOptions>("document")
{
    public Dictionary<string, KeystoneDocumentRelationship>? Relationships { get; set; }

    public Dictionary<string, object>? ComponentBlocks { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentFormattingOptions>? Formatting { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentLinksOptions>? Links { get; set; }

    public bool? Dividers { get; set; }

    public int[][]? Layouts { get; set; }
}

public sealed class KeystoneVirtualUiOptions : KeystoneFieldUiOptions
{
    public string? Query { get; set; }
}

public class KeystoneGraphqlObject(string name) : KeystoneJsObject(KeystoneImportObjects.Graphql, name);

public class KeystoneGraphqlField() : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Graphql, "field")
{
    public required KeystoneGraphqlObject Type { get; set; }
    public Dictionary<string, KeystoneGraphqlObject>? Args { get; set; }
    public required KeystoneJsFunction Resolve { get; set; }
}

public sealed class KeystoneVirtualField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneVirtualUiOptions>("virtual")
{
    public required KeystoneGraphqlField Field { get; set; }
}

public sealed class KeystoneResolveInputHooks
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }
}

public sealed class KeystoneValidateInputHooks
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneOperationHooks
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneFieldHooks
{
    public KeystoneResolveInputHooks? ResolveInput { get; set; }

    public KeystoneValidateInputHooks? ValidateInput { get; set; }

    public KeystoneOperationHooks? BeforeOperation { get; set; }

    public KeystoneOperationHooks? AfterOperation { get; set; }
}

public sealed class KeystoneListDb
{
    public string? Map { get; set; }

    public KeystoneIdFieldOptions? IdField { get; set; }
    
    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public sealed class KeystoneCreateViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? DefaultFieldMode { get; set; }
}

public sealed class KeystoneItemViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? DefaultFieldMode { get; set; }

    public KeystoneFieldPosition? FieldPosition { get; set; }
}

public sealed class KeystoneListInitialSort
{
    public required string Field { get; set; }

    public KeystoneSortDirection Direction { get; set; }
}

public sealed class KeystoneListViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? DefaultFieldMode { get; set; }

    public string[]? InitialColumns { get; set; }

    public KeystoneListInitialSort? InitialSort { get; set; }

    public int? PageSize { get; set; }
}

public sealed class KeystoneListUiOptions
{
    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }

    public string? Description { get; set; }

    public KeystoneJsValue<bool, KeystoneJsFunction>? IsHidden { get; set; }

    public KeystoneJsValue<bool, KeystoneJsFunction>? HideCreate { get; set; }

    public KeystoneJsValue<bool, KeystoneJsFunction>? HideDelete { get; set; }

    public KeystoneCreateViewOptions? CreateView { get; set; }

    public KeystoneItemViewOptions? ItemView { get; set; }

    public KeystoneListViewOptions? ListView { get; set; }

    public string? Label { get; set; }

    public string? Singular { get; set; }

    public string? Plural { get; set; }

    public string? Path { get; set; }
}

public sealed class KeystoneOperationAccess
{
    public KeystoneJsFunction? Query { get; set; }

    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneFilterAccess
{
    public KeystoneJsFunction? Query { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneItemAccess
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneListAccessControl
{
    public KeystoneOperationAccess? Operation { get; set; }

    public KeystoneFilterAccess? Filter { get; set; }

    public KeystoneItemAccess? Item { get; set; }
}

public class KeystoneFieldAccessControl
{
    public KeystoneJsFunction? Read { get; set; }

    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }
}

public abstract class KeystoneList(Type t) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Core, "list")
{
    [JsonIgnore] internal Type ClrType { get; } = t;

    public KeystoneJsValue<KeystoneListAccess, KeystoneListAccessControl>? Access { get; set; }

    public KeystoneListUiOptions? Ui { get; set; }

    public KeystoneListGraphqlOptions? Graphql { get; set; }

    public KeystoneFieldHooks? Hooks { get; set; }

    public string? Description { get; set; }

    public bool? IsSingleton { get; set; }

    public bool? DefaultIsFilterable { get; set; }

    public bool? DefaultIsOrderable { get; set; }

    public KeystoneListDb Db { get; } = new();

    public Dictionary<string, KeystoneField> Fields { get; } = new();

    public string Add(string key, KeystoneField field)
    {
        key = Utils.ToCamelCase(key);
        this.Fields.Add(key, field);

        return key;
    }
}

public sealed class KeystoneList<T>() : KeystoneList(typeof(T));

public abstract class KeystoneSession(string name) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Session, name);

public abstract class KeystoneCookieSession(string n) : KeystoneSession(n)
{
    public required string Secret { get; set; }

    public int? MaxAge { get; set; }

    public string? CookieName { get; set; }

    public bool? Secure { get; set; }

    public string? Path { get; set; }

    public string? Domain { get; set; }

    public KeystoneJsValue<bool, KeystoneCookieSameSite>? SameSite { get; set; }
}

public sealed class KeystoneStatelessSession() : KeystoneCookieSession("statelessSessions");

public sealed class KeystoneStoredSession() : KeystoneCookieSession("storedSessions")
{
    public KeystoneJsFunction? Store { get; set; }
}

public sealed class KeystoneStorageSigned
{
    public int Expiry { get; set; }
}

public sealed class KeystoneServerRoute
{
    public string Path { get; set; } = string.Empty;
}

public sealed class KeystoneStorageConfig
{
    public KeystoneStorageKind Kind { get; set; }
    public KeystoneStorageType Type { get; set; }
    public bool? Preserve { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
    public string? StoragePath { get; set; }
    public KeystoneServerRoute? ServerRoute { get; set; }
    public KeystoneJsFunction? GenerateUrl { get; set; }
    public string? BucketName { get; set; }
    public string? Region { get; set; }
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }
    public string? Endpoint { get; set; }
    public bool? ForcePathStyle { get; set; }
    public string? PathPrefix { get; set; }
    public KeystoneStorageSigned? Signed { get; set; }
    public string? Acl { get; set; }
}

public sealed class KeystoneDb
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
    
    public KeystoneDbProvider Provider { get; }

    public string Url { get; }

    public KeystoneJsFunction? OnConnect { get; set; }

    public bool? EnableLogging { get; set; }

    public KeystoneIdFieldOptions? IdField { get; set; }

    public string? ShadowDatabaseUrl { get; set; }
    
    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public sealed class KeystoneGraphqlOptions
{
    public bool? Debug { get; set; }

    public string? Path { get; set; }

    public KeystoneJsValue<bool, KeystoneGraphqlPlaygroundType>? Playground { get; set; }

    public Dictionary<string, object>? ApolloConfig { get; set; }

    public string? SchemaPath { get; set; }
}

public enum KeystoneGraphqlPlaygroundType
{
    Apollo
}

public sealed class KeystoneUiSettings
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

public sealed class KeystoneServerOptions
{
    public KeystoneCorsOptions? Cors { get; set; }

    public int? Port { get; set; }

    public Dictionary<string, object>? Options { get; set; }

    public int? MaxFileSize { get; set; }

    public KeystoneJsFunction? ExtendExpressApp { get; set; }

    public KeystoneJsFunction? ExtendHttpServer { get; set; }
}

public sealed class KeystoneConfig(DbContext dbContext, string baseDir) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Core, "config")
{
    public KeystoneDb Db { get; } = new(dbContext, baseDir);

    public KeystoneGraphqlOptions? Graphql { get; set; }

    public KeystoneServerOptions? Server { get; set; }

    public KeystoneSession? Session { get; set; }

    public KeystoneUiSettings? Ui { get; set; }

    public KeystoneJsFunction? ExtendGraphqlSchema { get; set; }

    public Dictionary<string, KeystoneStorageConfig>? Storage { get; set; }

    public Dictionary<string, KeystoneList> Lists { get; } = new();

    public void Add<T>(KeystoneList<T> list)
    {
        this.Lists[typeof(T).Name] = list;
    }
}

public enum KeystoneGraphqlCacheScope
{
    [JsonStringEnumMemberName("PUBLIC")] Public,
    [JsonStringEnumMemberName("PRIVATE")] Private
}

public enum KeystoneFieldDefaultValueKind
{
    Autoincrement,
    Now
}

public class KeystoneFieldDefaultValue
{
    public KeystoneFieldDefaultValueKind Kind { get; set; }
}

public enum KeystoneIdFieldKind
{
    Cuid,
    Uuid,
    Autoincrement
}

public enum KeystoneIndexMode
{
    Unique
}


public sealed class KeystoneGraphqlCacheHint
{
    public int MaxAge { get; set; }
    public KeystoneGraphqlCacheScope Scope { get; set; }
}

public sealed class KeystoneFieldGraphqlOmit
{
    public bool? Read { get; set; }
    public bool? Create { get; set; }
    public bool? Update { get; set; }
}

public sealed class KeystoneListGraphqlOmit
{
    public bool? Query { get; set; }
    public bool? Create { get; set; }
    public bool? Update { get; set; }
    public bool? Delete { get; set; }
}

public abstract class KeystoneField(string name, FieldDbOptions db) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Fields, name)
{
    internal FieldDbOptions Db { get; } = db;
    public KeystoneJsValue<KeystoneFieldAccess, KeystoneJsFunction, KeystoneFieldAccessControl>? Access { get; set; }
    public KeystoneFieldHooks? Hooks { get; set; }
    public string? Label { get; set; }
    public KeystoneJsValue<bool, KeystoneJsFunction>? IsFilterable { get; set; }
    public KeystoneJsValue<bool, KeystoneJsFunction>? IsOrderable { get; set; }
    public KeystoneJsValue<bool, KeystoneIndexMode>? IsIndexed { get; set; }
    public KeystoneFieldGraphqlOptions? Graphql { get; set; }
}

public sealed class KeystoneFieldGraphqlOptions
{
    public KeystoneGraphqlCacheHint? CacheHint { get; set; }
    public KeystoneFieldGraphqlIsNonNull? IsNonNull { get; set; }
    public KeystoneJsValue<bool, KeystoneFieldGraphqlOmit>? Omit { get; set; }
}

public sealed class KeystoneListGraphqlOptions
{
    public string? Description { get; set; }
    public string? Plural { get; set; }
    public string? ItemQueryName { get; set; }
    public string? ListQueryName { get; set; }
    public int? MaxTake { get; set; }
    public KeystoneGraphqlCacheHint? CacheHint { get; set; }
    public KeystoneJsValue<bool, KeystoneListGraphqlOmit>? Omit { get; set; }
}

public enum KeystoneIdFieldType
{
    BigInt
}

public sealed class KeystoneIdFieldOptions
{
    public KeystoneIdFieldKind Kind { get; set; }
    public KeystoneIdFieldType? Type { get; set; }
}

public interface IKeystoneDbContext
{
    void ConfigureKeystone(KeystoneConfig config);
}