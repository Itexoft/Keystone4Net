namespace Keystone4Net.Entities;

public class KeystoneDocumentField : KeystoneField
{
    public KeystoneDocumentField() : base("document")
    {
    }
    internal KeystoneDocumentDbOptions? Db { get; set; }
    public object? Relationships { get; set; }
    public object? ComponentBlocks { get; set; }
    public object? Formatting { get; set; }
    public object? Links { get; set; }
    public bool? Dividers { get; set; }
    public int[][]? Layouts { get; set; }
}

internal class KeystoneDocumentDbOptions
{
    public string? Map { get; set; }
}
