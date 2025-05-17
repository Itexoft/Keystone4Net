using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneRelationshipField(string name) : KeystoneField<KeystoneRelationshipDbOptions, KeystoneRelationshipUiOptions>(name, "relationship")
{
    public string Ref { get; set; } = string.Empty;

    public bool? Many { get; set; }
}

public class KeystoneForeignKeyOptions
{
    public string? Map { get; set; }
}

public class KeystoneRelationshipDbOptions : KeystoneFieldDbOptions
{
    public KeystoneJsValue<bool, KeystoneForeignKeyOptions>? ForeignKey { get; set; }
}

public class KeystoneInlineCreateOptions
{
    public string[] Fields { get; set; } = [];
}

public class KeystoneInlineConnectOptions
{
    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }
}

public class KeystoneRelationshipUiOptions : KeystoneFieldUiOptions
{
    public bool? HideCreate { get; set; }

    public KeystoneRelationshipDisplayMode? DisplayMode { get; set; }

    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }

    public string[]? CardFields { get; set; }

    public bool? LinkToItem { get; set; }

    public KeystoneRemoveMode? RemoveMode { get; set; }

    public KeystoneInlineCreateOptions? InlineCreate { get; set; }

    public KeystoneInlineCreateOptions? InlineEdit { get; set; }

    public KeystoneJsValue<bool, KeystoneInlineConnectOptions>? InlineConnect { get; set; }
}