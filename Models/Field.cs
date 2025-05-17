using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Keystone4NET.Models;

public class KeystoneFieldGraphqlIsNonNull
{
    public bool? Read { get; set; }

    public bool? Create { get; set; }

    public bool? Update { get; set; }
}

public class KeystoneGraphqlOmit
{
    public bool? Read { get; set; }

    public bool? Create { get; set; }

    public bool? Update { get; set; }

    public bool? Delete { get; set; }
}

public class KeystoneFieldCreateViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? FieldMode { get; set; }
}

public class KeystoneFieldItemViewOptions
{
    public KeystoneJsValue<KeystoneFieldMode, KeystoneJsFunction>? FieldMode { get; set; }

    public KeystoneFieldPosition? FieldPosition { get; set; }
}

public class KeystoneFieldListViewOptions
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

public abstract class KeystoneFieldDbOptions
{
    [JsonInclude]
    internal string? Map { get; set; }

    [JsonInclude]
    internal bool? IsNullable { get; set; }
}

public abstract class KeystoneField<TDb, TUi>(string name, string funcName) : KeystoneField(name, funcName, new TDb())
    where TDb : KeystoneFieldDbOptions, new()
    where TUi : KeystoneFieldUiOptions
{
    public new TDb Db => (TDb)base.Db;

    public TUi? Ui { get; set; }
}

public class KeystoneLengthOptions
{
    public int? Min { get; set; }

    public int? Max { get; set; }
}

public class KeystoneMatchOptions
{
    public required string Regex { get; set; }

    public string? Explanation { get; set; }
}

public class KeystoneResolveInputHooks
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }
}

public class KeystoneValidateInputHooks
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public class KeystoneOperationHooks
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public class KeystoneFieldHooks
{
    public KeystoneResolveInputHooks? ResolveInput { get; set; }

    public KeystoneValidateInputHooks? ValidateInput { get; set; }

    public KeystoneOperationHooks? BeforeOperation { get; set; }

    public KeystoneOperationHooks? AfterOperation { get; set; }
}

public abstract class KeystoneField(string fieldName, string funcName, KeystoneFieldDbOptions db) : KeystoneJsFunctionObjectCall(KeystoneImport.Fields, funcName)
{
    internal KeystoneFieldDbOptions Db { get; } = db;

    public KeystoneJsValue<KeystoneFieldAccess, KeystoneJsFunction, KeystoneFieldAccessControl>? Access { get; set; }

    public KeystoneFieldHooks? Hooks { get; set; }

    public string? Label { get; set; }

    public KeystoneJsValue<bool, KeystoneJsFunction>? IsFilterable { get; set; }

    public KeystoneJsValue<bool, KeystoneJsFunction>? IsOrderable { get; set; }

    public KeystoneJsValue<bool, KeystoneIndexMode>? IsIndexed { get; set; }

    public KeystoneFieldGraphqlOptions? Graphql { get; set; }
    
    public override string ToString() => fieldName;
}

public class KeystoneFieldGraphqlOptions
{
    public KeystoneGraphqlCacheHint? CacheHint { get; set; }

    public KeystoneFieldGraphqlIsNonNull? IsNonNull { get; set; }

    public KeystoneJsValue<bool, KeystoneFieldGraphqlOmit>? Omit { get; set; }
}