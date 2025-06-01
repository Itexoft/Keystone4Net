namespace Keystone4Net.Entities;

public class KeystoneDocumentFieldOptions : KeystoneFieldOptions
{
    internal KeystoneDocumentDbOptions? Db { get; set; }
}

internal class KeystoneDocumentDbOptions
{
    public string? Map { get; set; }
}
