namespace Keystone4Net.Entities;

public class KeystoneBytesFieldOptions : KeystoneFieldOptions
{
    public byte[]? DefaultValue { get; set; }
    public KeystoneBytesDbOptions? Db { get; set; }
}

public class KeystoneBytesDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}
