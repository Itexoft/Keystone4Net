using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystonePasswordField() : KeystoneField<KeystonePasswordDbOptions, KeystonePasswordUiOptions>("password")
{
    public KeystonePasswordValidation? Validation { get; set; }

    public KeystoneJsFunction? Bcrypt { get; set; }
}

public class KeystonePasswordDbOptions : KeystoneFieldDbOptions
{
}

public class KeystonePasswordValidation
{
    public bool? IsRequired { get; set; }

    public KeystoneLengthOptions? Length { get; set; }

    public KeystoneMatchOptions? Match { get; set; }

    public bool? RejectCommon { get; set; }
}

public class KeystonePasswordUiOptions : KeystoneFieldUiOptions
{
}
