using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneTimestampField() : KeystoneField<KeystoneTimestampDbOptions, KeystoneTimestampUiOptions>("timestamp")
{
    public KeystoneJsValue<string, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneTimestampValidation? Validation { get; set; }
}

public class KeystoneTimestampDbOptions : KeystoneFieldDbOptions
{
    public bool? UpdatedAt { get; set; }
}

public class KeystoneTimestampValidation
{
    public bool? IsRequired { get; set; }
}

public class KeystoneTimestampUiOptions : KeystoneFieldUiOptions
{
}
