using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneTextField() : KeystoneField<KeystoneTextDbOptions, KeystoneTextUiOptions>("text")
{
    public string? DefaultValue { get; set; }

    public KeystoneTextValidation? Validation { get; set; }
}

public class KeystoneTextDbOptions : KeystoneFieldDbOptions
{
    public string? NativeType { get; set; }
}

public class KeystoneTextValidation
{
    public bool? IsRequired { get; set; }

    public KeystoneLengthOptions? Length { get; set; }

    public KeystoneMatchOptions? Match { get; set; }
}

public class KeystoneTextUiOptions : KeystoneFieldUiOptions
{
    public KeystoneTextDisplayMode? DisplayMode { get; set; }
}