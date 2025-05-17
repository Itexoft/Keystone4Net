using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneDocumentField(string name) : KeystoneField<KeystoneDocumentDbOptions, KeystoneDocumentUiOptions>(name, "document")
{
    public Dictionary<string, KeystoneDocumentRelationship>? Relationships { get; set; }

    public Dictionary<string, object>? ComponentBlocks { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentFormattingOptions>? Formatting { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentLinksOptions>? Links { get; set; }

    public bool? Dividers { get; set; }

    public int[][]? Layouts { get; set; }
}

public class KeystoneDocumentDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneDocumentRelationship
{
    public KeystoneDocumentRelationshipKind Kind { get; set; }

    public required string ListKey { get; set; }

    public required string Label { get; set; }

    public string? Selection { get; set; }
}

public class KeystoneDocumentFormattingInlineMarks
{
    public bool? Bold { get; set; }

    public bool? Italic { get; set; }

    public bool? Underline { get; set; }

    public bool? Strikethrough { get; set; }

    public bool? Code { get; set; }

    public bool? Superscript { get; set; }

    public bool? Subscript { get; set; }

    public bool? Keyboard { get; set; }
}

public class KeystoneDocumentFormattingOptions
{
    public KeystoneDocumentFormattingInlineMarks? InlineMarks { get; set; }

    public KeystoneDocumentFormattingListTypes? ListTypes { get; set; }

    public KeystoneDocumentFormattingAlignment? Alignment { get; set; }

    public List<int> HeadingLevels { get; set; } = [];

    public KeystoneDocumentFormattingBlockTypes? BlockTypes { get; set; }

    public bool? SoftBreaks { get; set; }
}

public class KeystoneDocumentFormattingListTypes
{
    public bool? Ordered { get; set; }

    public bool? Unordered { get; set; }
}

public class KeystoneDocumentFormattingAlignment
{
    public bool? Center { get; set; }

    public bool? End { get; set; }
}

public class KeystoneDocumentFormattingBlockTypes
{
    public bool? Blockquote { get; set; }

    public bool? Code { get; set; }
}

public class KeystoneDocumentLinksOptions
{
    public bool? Anchor { get; set; }
}

public class KeystoneDocumentUiOptions : KeystoneFieldUiOptions
{
}