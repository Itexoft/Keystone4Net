namespace Keystone4Net.Entities;

public class KeystoneDocumentFieldOptions : KeystoneFieldOptions
{
    public KeystoneDocumentDbOptions? Db { get; set; }
}

public class KeystoneDocumentDbOptions
{
    public string? Map { get; set; }
}
