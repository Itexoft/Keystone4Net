namespace Keystone4NET.Fields;

public class KeystoneImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneImageUiOptions>("image")
{
    public string Storage { get; set; } = string.Empty;
}

public class KeystoneImageUiOptions : KeystoneFieldUiOptions
{
}
