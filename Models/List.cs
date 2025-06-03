using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Keystone4NET.Common;

namespace Keystone4NET.Models;

public class KeystoneListDb
{
    [JsonInclude]
    internal string? Map { get; set; }

    public KeystoneIdFieldOptions? IdField { get; set; }

    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public class KeystoneCreateViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? DefaultFieldMode { get; set; }
}

public class KeystoneItemViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? DefaultFieldMode { get; set; }

    public KeystoneFieldPosition? FieldPosition { get; set; }
}

public class KeystoneListInitialSort
{
    public required KeystoneRef<KeystoneField> Field { get; set; }

    public KeystoneSortDirection Direction { get; set; }
}

public class KeystoneListViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? DefaultFieldMode { get; set; }

    public IList<KeystoneRef<KeystoneField>>? InitialColumns { get; set; }

    public KeystoneListInitialSort? InitialSort { get; set; }

    public int? PageSize { get; set; }
}

public class KeystoneListUiOptions
{
    public KeystoneRef<KeystoneField>? LabelField { get; set; }

    public IList<KeystoneRef<KeystoneField>>? SearchFields { get; set; }

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

public abstract class KeystoneList(Type t, KeystoneJsValue<KeystoneListAccess, KeystoneListAccessControl> access)
    : KeystoneJsFunctionObjectCall(KeystoneImport.Core, "list")
{
    [JsonIgnore] internal Type ClrType { get; } = t;

    public KeystoneJsValue<KeystoneListAccess, KeystoneListAccessControl> Access { get; set; } = access;

    public KeystoneListUiOptions? Ui { get; set; }

    public KeystoneListGraphqlOptions? Graphql { get; set; }

    public KeystoneFieldHooks? Hooks { get; set; }

    public string? Description { get; set; }

    public bool? IsSingleton { get; set; }

    public bool? DefaultIsFilterable { get; set; }

    public bool? DefaultIsOrderable { get; set; }

    public KeystoneListDb Db { get; } = new();

    public KeystoneCollection<KeystoneField> Fields { get; } = new(x => x.ToString());

    public override string ToString() => this.ClrType.Name;
}

public class KeystoneList<T>(KeystoneJsValue<KeystoneListAccess, KeystoneListAccessControl> access) : KeystoneList(typeof(T), access);