namespace Keystone4Net.Entities;

public class KeystoneBigIntFieldOptions : KeystoneFieldOptions
{
    public long? DefaultValue { get; set; }
    internal KeystoneBigIntDbOptions? Db { get; set; }
    public KeystoneBigIntValidationOptions? Validation { get; set; }
}

internal class KeystoneBigIntDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneBigIntValidationOptions
{
    public bool IsRequired { get; set; }
    public long? Min { get; set; }
    public long? Max { get; set; }
}
