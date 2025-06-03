using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneMultiselectField() : KeystoneField<KeystoneMultiselectDbOptions, KeystoneMultiselectUiOptions>("multiselect")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>[]? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];
}

public class KeystoneMultiselectDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneMultiselectUiOptions : KeystoneFieldUiOptions
{
}
