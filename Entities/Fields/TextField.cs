using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public class KeystoneTextFieldOptions : KeystoneFieldOptions<KeystoneTextUiOptions>
{
    public string? DefaultValue { get; set; }
    internal KeystoneTextDbOptions? Db { get; set; }
    public KeystoneTextValidationOptions? Validation { get; set; }
}

public class KeystoneTextValidationOptions
{
    public bool IsRequired { get; set; }
    public KeystoneTextLengthOptions? Length { get; set; }
    public KeystoneTextMatchOptions? Match { get; set; }
}

public class KeystoneTextLengthOptions
{
    public int Min { get; set; }
    public int? Max { get; set; }
}

internal class KeystoneTextDbOptions
{
    public string? Map { get; set; }
    public bool IsNullable { get; set; }
    public string? NativeType { get; set; }
    public KeystoneJsFunction? ExtendPrismaSchema { get; set; }
}

public class KeystoneTextUiOptions : KeystoneFieldUiOptions
{
    public KeystoneDisplayMode DisplayMode { get; set; }
}
