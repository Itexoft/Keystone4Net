using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneDecimalField() : KeystoneField<KeystoneDecimalDbOptions, KeystoneDecimalUiOptions>("decimal")
{
    public string? DefaultValue { get; set; }

    public int? Precision { get; set; }

    public int? Scale { get; set; }

    public KeystoneDecimalValidation? Validation { get; set; }
}

public class KeystoneDecimalDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneDecimalValidation
{
    public bool? IsRequired { get; set; }

    public string? Min { get; set; }

    public string? Max { get; set; }
}

public class KeystoneDecimalUiOptions : KeystoneFieldUiOptions
{
}
