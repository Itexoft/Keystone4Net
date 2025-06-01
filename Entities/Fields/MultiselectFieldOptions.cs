namespace Keystone4Net.Entities;

public class KeystoneMultiselectFieldOptions : KeystoneFieldOptions
{
    public string[]? DefaultValue { get; set; }
    public KeystoneSelectOption[]? Options { get; set; }
    internal KeystoneMultiselectDbOptions? Db { get; set; }
}

internal class KeystoneMultiselectDbOptions
{
    public bool? IsNullable { get; set; }
    public string? Map { get; set; }
}
