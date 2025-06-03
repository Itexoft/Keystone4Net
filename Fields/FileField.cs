namespace Keystone4NET.Fields;

public class KeystoneFileField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFileUiOptions>("file")
{
    public string Storage { get; set; } = string.Empty;
}

public class KeystoneFileUiOptions : KeystoneFieldUiOptions
{
}
