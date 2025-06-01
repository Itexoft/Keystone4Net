namespace Keystone4Net.Entities;

public class KeystoneDecimalFieldOptions : KeystoneFieldOptions
{
    public decimal? DefaultValue { get; set; }
    public KeystoneDecimalDbOptions? Db { get; set; }
    public KeystoneDecimalValidationOptions? Validation { get; set; }
}

public class KeystoneDecimalDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneDecimalValidationOptions
{
    public bool IsRequired { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}
