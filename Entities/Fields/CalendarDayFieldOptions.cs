namespace Keystone4Net.Entities;

public class KeystoneCalendarDayFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    internal KeystoneCalendarDayDbOptions? Db { get; set; }
    public KeystoneCalendarDayValidationOptions? Validation { get; set; }
}

internal class KeystoneCalendarDayDbOptions
{
    public bool? IsNullable { get; set; }
}

public class KeystoneCalendarDayValidationOptions
{
    public bool IsRequired { get; set; }
}
