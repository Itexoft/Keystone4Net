namespace Keystone4Net.Entities;

public class KeystoneCheckboxFieldOptions : KeystoneFieldOptions
{
    public bool? DefaultValue { get; set; }
    public KeystoneCheckboxDbOptions? Db { get; set; }
}

public class KeystoneCheckboxDbOptions
{
}
