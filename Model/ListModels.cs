using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Microsoft.EntityFrameworkCore;

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
