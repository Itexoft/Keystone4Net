namespace Keystone4Net.Entities;

public class KeystoneFileFieldOptions : KeystoneFieldOptions
{
    public KeystoneFileDbOptions? Db { get; set; }
    public KeystoneStorageStrategy? Storage { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
}

public class KeystoneFileDbOptions
{
    public string? Map { get; set; }
}

public class KeystoneStorageStrategy
{
    public KeystoneJsFunction? Read { get; set; }

    public KeystoneJsFunction? Write { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}
