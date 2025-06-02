using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Microsoft.EntityFrameworkCore;

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
