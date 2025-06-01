namespace Keystone4Net.Entities;

public class KeystoneSelectFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    public KeystoneSelectOption[]? Options { get; set; }
    internal KeystoneSelectDbOptions? Db { get; set; }
    public KeystoneSelectValidationOptions? Validation { get; set; }
}

public class KeystoneSelectOption
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

internal class KeystoneSelectDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystoneSelectValidationOptions
{
    public bool IsRequired { get; set; }
}
