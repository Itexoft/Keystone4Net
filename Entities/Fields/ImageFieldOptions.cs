namespace Keystone4Net.Entities;

public class KeystoneImageFieldOptions : KeystoneFieldOptions
{
    public KeystoneImageDbOptions? Db { get; set; }
    public KeystoneStorageStrategy? Storage { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
}

public class KeystoneImageDbOptions
{
    public string? Map { get; set; }
}
