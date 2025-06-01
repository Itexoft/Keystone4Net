namespace Keystone4Net.Entities;

public class KeystoneBytesFieldOptions : KeystoneFieldOptions
{
    public byte[]? DefaultValue { get; set; }
    internal KeystoneBytesDbOptions? Db { get; set; }
}

internal class KeystoneBytesDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}
