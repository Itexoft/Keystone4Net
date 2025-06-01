namespace Keystone4Net.Entities;

public class KeystoneRelationshipField : KeystoneField<KeystoneRelationshipUiOptions>
{
    public KeystoneRelationshipField() : base("relationship")
    {
    }
    public bool? Many { get; set; }
    public string Ref { get; set; } = string.Empty;
    internal KeystoneRelationshipDbOptions? Db { get; set; }
}

internal class KeystoneRelationshipDbOptions
{
    public string? RelationName { get; set; }
    public object? ForeignKey { get; set; }
}

public class KeystoneRelationshipUiOptions
{
    public bool? HideCreate { get; set; }
    public KeystoneDisplayMode? DisplayMode { get; set; }
    public string? LabelField { get; set; }
    public string[]? SearchFields { get; set; }
    public string[]? CardFields { get; set; }
    public bool? LinkToItem { get; set; }
    public KeystoneRemoveMode? RemoveMode { get; set; }
    public KeystoneInlineCreateOptions? InlineCreate { get; set; }
    public KeystoneInlineEditOptions? InlineEdit { get; set; }
    public KeystoneInlineConnectOptions? InlineConnect { get; set; }
}

public class KeystoneInlineCreateOptions
{
    public string[] Fields { get; set; } = [];
}

public class KeystoneInlineEditOptions
{
    public string[] Fields { get; set; } = [];
}

public class KeystoneInlineConnectOptions
{
    public string? LabelField { get; set; }
    public string[]? SearchFields { get; set; }
}
