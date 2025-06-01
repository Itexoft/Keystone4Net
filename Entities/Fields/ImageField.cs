namespace Keystone4Net.Entities;

public class KeystoneImageField : KeystoneField
{
    public KeystoneImageField() : base("image")
    {
    }
    public string Storage { get; set; } = string.Empty;
}
