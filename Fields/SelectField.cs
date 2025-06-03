using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneSelectField() : KeystoneField<KeystoneSelectDbOptions, KeystoneSelectUiOptions>("select")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];

    public KeystoneSelectValidation? Validation { get; set; }
}

public class KeystoneSelectDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneSelectValidation
{
    public bool? IsRequired { get; set; }
}

public class KeystoneSelectOption
{
    public required string Label { get; set; }

    public required KeystoneJsValue<string, int> Value { get; set; }
}

public class KeystoneSelectUiOptions : KeystoneFieldUiOptions
{
    public KeystoneSelectUiMode? DisplayMode { get; set; }
}
