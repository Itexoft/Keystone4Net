namespace Keystone4Net.Entities;

public class KeystoneMultiselectFieldOptions : KeystoneFieldOptions
{
    public string[]? DefaultValue { get; set; }
    public KeystoneSelectOption[]? Options { get; set; }
    public KeystoneMultiselectDbOptions? Db { get; set; }
}

public class KeystoneMultiselectDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}
