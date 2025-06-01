namespace Keystone4Net.Entities;

public class KeystoneFileFieldOptions : KeystoneFieldOptions
{
    internal KeystoneFileDbOptions? Db { get; set; }
    public KeystoneStorageStrategy? Storage { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
}

internal class KeystoneFileDbOptions
{
    public string? Map { get; set; }
}

public class KeystoneStorageStrategy
{
    public KeystoneJsFunction? Read { get; set; }

    public KeystoneJsFunction? Write { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}
