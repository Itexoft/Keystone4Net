using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneCheckboxField(string name) : KeystoneField<KeystoneCheckboxDbOptions, KeystoneCheckboxUiOptions>(name, "checkbox")
{
    public bool? DefaultValue { get; set; }
}

public class KeystoneCheckboxDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneCheckboxUiOptions : KeystoneFieldUiOptions
{
}