namespace Keystone4Net.Entities;

public class KeystoneRelationshipFieldOptions : KeystoneFieldOptions<KeystoneRelationshipUiOptions>
{
    public bool? Many { get; set; }
    public string Ref { get; set; } = string.Empty;
    public KeystoneRelationshipDbOptions? Db { get; set; }
}

public class KeystoneRelationshipDbOptions
{
    public string? RelationName { get; set; }
}

public class KeystoneRelationshipUiOptions
{
    public bool? HideCreate { get; set; }
}
