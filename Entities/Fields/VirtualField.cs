namespace Keystone4Net.Entities;

public class KeystoneVirtualField : KeystoneField<KeystoneVirtualUiOptions>
{
    public KeystoneVirtualField() : base("virtual")
    {
    }
    public KeystoneJsFunction? Field { get; set; }
}

public class KeystoneVirtualUiOptions : KeystoneFieldUiOptions
{
    public string? Query { get; set; }
}
