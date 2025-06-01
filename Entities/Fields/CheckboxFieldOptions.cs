namespace Keystone4Net.Entities;

public class KeystoneCheckboxFieldOptions : KeystoneFieldOptions
{
    public bool? DefaultValue { get; set; }
    internal KeystoneCheckboxDbOptions? Db { get; set; }
}

internal class KeystoneCheckboxDbOptions
{
}
