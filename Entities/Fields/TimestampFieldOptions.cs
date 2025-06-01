namespace Keystone4Net.Entities;

public class KeystoneTimestampFieldOptions : KeystoneFieldOptions
{
    public DateTime? DefaultValue { get; set; }
    public KeystoneTimestampDbOptions? Db { get; set; }
    public KeystoneTimestampValidationOptions? Validation { get; set; }
}

public class KeystoneTimestampDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneTimestampValidationOptions
{
    public bool IsRequired { get; set; }
}
