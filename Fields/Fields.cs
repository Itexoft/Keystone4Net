using System.Numerics;
using System.Text.Json;
using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystonePasswordDbOptions : KeystoneFieldDbOptions
{
}

public class KeystonePasswordValidation
{
    public bool? IsRequired { get; set; }

    public KeystoneLengthOptions? Length { get; set; }

    public KeystoneMatchOptions? Match { get; set; }

    public bool? RejectCommon { get; set; }
}

public class KeystonePasswordField() : KeystoneField<KeystonePasswordDbOptions, KeystoneFieldUiOptions>("password")
{
    public KeystonePasswordValidation? Validation { get; set; }

    public KeystoneJsFunction? Bcrypt { get; set; }
}

public class KeystoneCheckboxDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneCheckboxField() : KeystoneField<KeystoneCheckboxDbOptions, KeystoneFieldUiOptions>("checkbox")
{
    public bool? DefaultValue { get; set; }
}

public class KeystoneIntegerDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneIntegerValidation
{
    public bool? IsRequired { get; set; }

    public int? Min { get; set; }

    public int? Max { get; set; }
}

public class KeystoneIntegerField() : KeystoneField<KeystoneIntegerDbOptions, KeystoneFieldUiOptions>("integer")
{
    public KeystoneJsValue<int, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneIntegerValidation? Validation { get; set; }
}

public class KeystoneBigIntDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneBigIntValidation
{
    public bool? IsRequired { get; set; }

    public BigInteger? Min { get; set; }

    public BigInteger? Max { get; set; }
}

public class KeystoneBigIntField() : KeystoneField<KeystoneBigIntDbOptions, KeystoneFieldUiOptions>("bigInt")
{
    public KeystoneJsValue<BigInteger, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneBigIntValidation? Validation { get; set; }
}

public class KeystoneFloatDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneFloatValidation
{
    public bool? IsRequired { get; set; }

    public double? Min { get; set; }

    public double? Max { get; set; }
}

public class KeystoneFloatField() : KeystoneField<KeystoneFloatDbOptions, KeystoneFieldUiOptions>("float")
{
    public double? DefaultValue { get; set; }

    public KeystoneFloatValidation? Validation { get; set; }
}

public class KeystoneDecimalDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneDecimalValidation
{
    public bool? IsRequired { get; set; }

    public string? Min { get; set; }

    public string? Max { get; set; }
}

public class KeystoneDecimalField() : KeystoneField<KeystoneDecimalDbOptions, KeystoneFieldUiOptions>("decimal")
{
    public string? DefaultValue { get; set; }

    public int? Precision { get; set; }

    public int? Scale { get; set; }

    public KeystoneDecimalValidation? Validation { get; set; }
}

public class KeystoneJsonDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneJsonField() : KeystoneField<KeystoneJsonDbOptions, KeystoneFieldUiOptions>("json")
{
    public JsonElement? DefaultValue { get; set; }
}

public class KeystoneCalendarDayDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneCalendarDayValidation
{
    public bool? IsRequired { get; set; }
}

public class KeystoneCalendarDayField() : KeystoneField<KeystoneCalendarDayDbOptions, KeystoneFieldUiOptions>("calendarDay")
{
    public string? DefaultValue { get; set; }

    public KeystoneCalendarDayValidation? Validation { get; set; }
}

public class KeystoneTimestampDbOptions : KeystoneFieldDbOptions
{
    public bool? UpdatedAt { get; set; }
}

public class KeystoneTimestampValidation
{
    public bool? IsRequired { get; set; }
}

public class KeystoneTimestampField() : KeystoneField<KeystoneTimestampDbOptions, KeystoneFieldUiOptions>("timestamp")
{
    public KeystoneJsValue<string, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneTimestampValidation? Validation { get; set; }
}

public class KeystoneSelectDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneSelectValidation
{
    public bool? IsRequired { get; set; }
}

public class KeystoneSelectOption
{
    public required string Label { get; set; }

    public required KeystoneJsValue<string, int> Value { get; set; }
}

public class KeystoneSelectUiOptions : KeystoneFieldUiOptions
{
    public KeystoneSelectUiMode? DisplayMode { get; set; }
}

public class KeystoneSelectField() : KeystoneField<KeystoneSelectDbOptions, KeystoneSelectUiOptions>("select")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];

    public KeystoneSelectValidation? Validation { get; set; }
}

public class KeystoneMultiselectDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneMultiselectField() : KeystoneField<KeystoneMultiselectDbOptions, KeystoneFieldUiOptions>("multiselect")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>[]? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];
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

public class KeystoneRelationshipField() : KeystoneField<KeystoneRelationshipDbOptions, KeystoneRelationshipUiOptions>("relationship")
{
    public string Ref { get; set; } = string.Empty;

    public bool? Many { get; set; }
}

public class KeystoneEmptyDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneFileField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("file")
{
    public string Storage { get; set; } = string.Empty;
}

public class KeystoneImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("image")
{
    public string Storage { get; set; } = string.Empty;
}

public class KeystoneCloudinaryCredentials
{
    public string CloudName { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string ApiSecret { get; set; } = string.Empty;

    public string? Folder { get; set; }
}

public class KeystoneCloudinaryImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("cloudinaryImage")
{
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
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

public class KeystoneDocumentField() : KeystoneField<KeystoneDocumentDbOptions, KeystoneFieldUiOptions>("document")
{
    public Dictionary<string, KeystoneDocumentRelationship>? Relationships { get; set; }

    public Dictionary<string, object>? ComponentBlocks { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentFormattingOptions>? Formatting { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentLinksOptions>? Links { get; set; }

    public bool? Dividers { get; set; }

    public int[][]? Layouts { get; set; }
}

public class KeystoneVirtualUiOptions : KeystoneFieldUiOptions
{
    public string? Query { get; set; }
}

public class KeystoneGraphqlObject(string name) : KeystoneJsObject(KeystoneImport.Graphql, name);

public class KeystoneGraphqlField() : KeystoneJsFunctionObjectCall(KeystoneImport.Graphql, "field")
{
    public required KeystoneGraphqlObject Type { get; set; }

    public Dictionary<string, KeystoneGraphqlObject>? Args { get; set; }

    public required KeystoneJsFunction Resolve { get; set; }
}

public class KeystoneVirtualField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneVirtualUiOptions>("virtual")
{
    public required KeystoneGraphqlField Field { get; set; }
}