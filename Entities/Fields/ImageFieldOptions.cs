namespace Keystone4Net.Entities;

public class KeystoneImageFieldOptions : KeystoneFieldOptions
{
    internal KeystoneImageDbOptions? Db { get; set; }
    public KeystoneStorageStrategy? Storage { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
}

internal class KeystoneImageDbOptions
{
    public string? Map { get; set; }
}
