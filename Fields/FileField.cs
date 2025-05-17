using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneFileField(string name) : KeystoneField<KeystoneFileFieldDbOptions, KeystoneFileUiOptions>(name, "file")
{
    public string Storage { get; set; } = string.Empty;
}

public class KeystoneFileFieldDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneFileUiOptions : KeystoneFieldUiOptions
{
}