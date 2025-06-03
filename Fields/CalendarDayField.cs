using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneCalendarDayField() : KeystoneField<KeystoneCalendarDayDbOptions, KeystoneCalendarDayUiOptions>("calendarDay")
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
