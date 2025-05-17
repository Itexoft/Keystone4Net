using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneIntegerField(string name) : KeystoneField<KeystoneIntegerDbOptions, KeystoneIntegerUiOptions>(name, "integer")
{
    public KeystoneJsValue<int, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneIntegerValidation? Validation { get; set; }
}

public class KeystoneIntegerDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneIntegerValidation
{
    public bool? IsRequired { get; set; }

    public int? Min { get; set; }

    public int? Max { get; set; }
}

public class KeystoneIntegerUiOptions : KeystoneFieldUiOptions
{
}