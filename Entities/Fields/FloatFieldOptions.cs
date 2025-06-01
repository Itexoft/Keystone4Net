namespace Keystone4Net.Entities;

public class KeystoneFloatFieldOptions : KeystoneFieldOptions
{
    public double? DefaultValue { get; set; }
    internal KeystoneFloatDbOptions? Db { get; set; }
    public KeystoneFloatValidationOptions? Validation { get; set; }
}

internal class KeystoneFloatDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneFloatValidationOptions
{
    public bool IsRequired { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
}
