namespace Keystone4Net.Entities;

public class KeystoneIntegerFieldOptions : KeystoneFieldOptions
{
    public int? DefaultValue { get; set; }
    internal KeystoneIntegerDbOptions? Db { get; set; }
    public KeystoneIntegerValidationOptions? Validation { get; set; }
}

internal class KeystoneIntegerDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneIntegerValidationOptions
{
    public bool IsRequired { get; set; }
    public int? Min { get; set; }
    public int? Max { get; set; }
}
