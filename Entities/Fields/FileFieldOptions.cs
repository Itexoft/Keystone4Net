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
}
