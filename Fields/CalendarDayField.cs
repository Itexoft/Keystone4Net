using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneCalendarDayField(string name) : KeystoneField<KeystoneCalendarDayDbOptions, KeystoneCalendarDayUiOptions>(name, "calendarDay")
{
    public string? DefaultValue { get; set; }

    public KeystoneCalendarDayValidation? Validation { get; set; }
}

public class KeystoneCalendarDayDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneCalendarDayValidation
{
    public bool? IsRequired { get; set; }
}

public class KeystoneCalendarDayUiOptions : KeystoneFieldUiOptions
{
}