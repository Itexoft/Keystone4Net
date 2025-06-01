namespace Keystone4Net.Entities;

public class KeystoneRelationshipFieldOptions : KeystoneFieldOptions<KeystoneRelationshipUiOptions>
{
    public bool? Many { get; set; }
    public string Ref { get; set; } = string.Empty;
    internal KeystoneRelationshipDbOptions? Db { get; set; }
}

internal class KeystoneRelationshipDbOptions
{
    public string? RelationName { get; set; }
}

public class KeystoneRelationshipUiOptions
{
    public bool? HideCreate { get; set; }
}
