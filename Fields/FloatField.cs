using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneFloatField() : KeystoneField<KeystoneFloatDbOptions, KeystoneFloatUiOptions>("float")
{
    public double? DefaultValue { get; set; }

    public KeystoneFloatValidation? Validation { get; set; }
}

public class KeystoneFloatDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneFloatValidation
{
    public bool? IsRequired { get; set; }

    public double? Min { get; set; }

    public double? Max { get; set; }
}

public class KeystoneFloatUiOptions : KeystoneFieldUiOptions
{
}
