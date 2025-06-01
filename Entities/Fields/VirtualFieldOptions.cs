namespace Keystone4Net.Entities;

public class KeystoneVirtualFieldOptions : KeystoneFieldOptions
{
    public KeystoneJsFunction? Field { get; set; }

    public string? GraphqlReturnType { get; set; }
}
