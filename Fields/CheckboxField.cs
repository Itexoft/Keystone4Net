using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneCheckboxField() : KeystoneField<KeystoneCheckboxDbOptions, KeystoneCheckboxUiOptions>("checkbox")
{
    public bool? DefaultValue { get; set; }
}

public class KeystoneCheckboxDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneCheckboxUiOptions : KeystoneFieldUiOptions
{
}
