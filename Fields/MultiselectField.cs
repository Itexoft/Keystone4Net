using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneMultiselectField(string name) : KeystoneField<KeystoneMultiselectDbOptions, KeystoneMultiselectUiOptions>(name, "multiselect")
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