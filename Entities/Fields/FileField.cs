namespace Keystone4Net.Entities;

public class KeystoneFileField : KeystoneField
{
    public KeystoneFileField() : base("file")
    {
    }
    public string Storage { get; set; } = string.Empty;
}
