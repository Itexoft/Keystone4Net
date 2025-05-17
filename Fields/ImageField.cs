using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneImageField(string name) : KeystoneField<KeystoneImageFieldDbOptions, KeystoneImageUiOptions>(name, "image")
{
    public string Storage { get; set; } = string.Empty;
}

public class KeystoneImageFieldDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneImageUiOptions : KeystoneFieldUiOptions
{
}