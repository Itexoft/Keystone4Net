namespace Keystone4Net.Entities;

public class KeystonePasswordFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    internal KeystonePasswordDbOptions? Db { get; set; }
    public KeystonePasswordValidationOptions? Validation { get; set; }
}

internal class KeystonePasswordDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}

public class KeystonePasswordValidationOptions
{
    public bool IsRequired { get; set; }
    public bool RejectCommon { get; set; }
    public KeystoneTextMatchOptions? Match { get; set; }
    public KeystoneTextLengthOptions? Length { get; set; }
}
