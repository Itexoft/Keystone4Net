using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Microsoft.EntityFrameworkCore;

public sealed class KeystoneTextDbOptions : FieldDbOptions
{
    public string? NativeType { get; set; }
}

public sealed class KeystoneTextValidation
{
    public bool? IsRequired { get; set; }

    public KeystoneLengthOptions? Length { get; set; }

    public KeystoneMatchOptions? Match { get; set; }
}

public sealed class KeystoneTextUiOptions : KeystoneFieldUiOptions
{
    public KeystoneTextDisplayMode? DisplayMode { get; set; }
}

public sealed class KeystoneTextField() : KeystoneField<KeystoneTextDbOptions, KeystoneTextUiOptions>("text")
{
    public string? DefaultValue { get; set; }

    public KeystoneTextValidation? Validation { get; set; }
}

public sealed class KeystonePasswordDbOptions : FieldDbOptions
{
}

public sealed class KeystonePasswordValidation
{
    public bool? IsRequired { get; set; }

    public KeystoneLengthOptions? Length { get; set; }

    public KeystoneMatchOptions? Match { get; set; }

    public bool? RejectCommon { get; set; }
}

public sealed class KeystonePasswordField() : KeystoneField<KeystonePasswordDbOptions, KeystoneFieldUiOptions>("password")
{
    public KeystonePasswordValidation? Validation { get; set; }

    public KeystoneJsFunction? Bcrypt { get; set; }
}

public sealed class KeystoneCheckboxDbOptions : FieldDbOptions
{
}

public sealed class KeystoneCheckboxField() : KeystoneField<KeystoneCheckboxDbOptions, KeystoneFieldUiOptions>("checkbox")
{
    public bool? DefaultValue { get; set; }
}

public sealed class KeystoneIntegerDbOptions : FieldDbOptions
{
}

public sealed class KeystoneIntegerValidation
{
    public bool? IsRequired { get; set; }

    public int? Min { get; set; }

    public int? Max { get; set; }
}

public sealed class KeystoneIntegerField() : KeystoneField<KeystoneIntegerDbOptions, KeystoneFieldUiOptions>("integer")
{
    public KeystoneJsValue<int, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneIntegerValidation? Validation { get; set; }
}

public sealed class KeystoneBigIntDbOptions : FieldDbOptions
{
}

public sealed class KeystoneBigIntValidation
{
    public bool? IsRequired { get; set; }

    public BigInteger? Min { get; set; }

    public BigInteger? Max { get; set; }
}

public sealed class KeystoneBigIntField() : KeystoneField<KeystoneBigIntDbOptions, KeystoneFieldUiOptions>("bigInt")
{
    public KeystoneJsValue<BigInteger, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneBigIntValidation? Validation { get; set; }
}

public sealed class KeystoneFloatDbOptions : FieldDbOptions
{
}

public sealed class KeystoneFloatValidation
{
    public bool? IsRequired { get; set; }

    public double? Min { get; set; }

    public double? Max { get; set; }
}

public sealed class KeystoneFloatField() : KeystoneField<KeystoneFloatDbOptions, KeystoneFieldUiOptions>("float")
{
    public double? DefaultValue { get; set; }

    public KeystoneFloatValidation? Validation { get; set; }
}

public sealed class KeystoneDecimalDbOptions : FieldDbOptions
{
}

public sealed class KeystoneDecimalValidation
{
    public bool? IsRequired { get; set; }

    public string? Min { get; set; }

    public string? Max { get; set; }
}

public sealed class KeystoneDecimalField() : KeystoneField<KeystoneDecimalDbOptions, KeystoneFieldUiOptions>("decimal")
{
    public string? DefaultValue { get; set; }

    public int? Precision { get; set; }

    public int? Scale { get; set; }

    public KeystoneDecimalValidation? Validation { get; set; }
}

public sealed class KeystoneJsonDbOptions : FieldDbOptions
{
}

public sealed class KeystoneJsonField() : KeystoneField<KeystoneJsonDbOptions, KeystoneFieldUiOptions>("json")
{
    public JsonElement? DefaultValue { get; set; }
}

public sealed class KeystoneCalendarDayDbOptions : FieldDbOptions
{
}

public sealed class KeystoneCalendarDayValidation
{
    public bool? IsRequired { get; set; }
}

public sealed class KeystoneCalendarDayField() : KeystoneField<KeystoneCalendarDayDbOptions, KeystoneFieldUiOptions>("calendarDay")
{
    public string? DefaultValue { get; set; }

    public KeystoneCalendarDayValidation? Validation { get; set; }
}

public sealed class KeystoneTimestampDbOptions : FieldDbOptions
{
    public bool? UpdatedAt { get; set; }
}

public sealed class KeystoneTimestampValidation
{
    public bool? IsRequired { get; set; }
}

public sealed class KeystoneTimestampField() : KeystoneField<KeystoneTimestampDbOptions, KeystoneFieldUiOptions>("timestamp")
{
    public KeystoneJsValue<string, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneTimestampValidation? Validation { get; set; }
}

public sealed class KeystoneSelectDbOptions : FieldDbOptions
{
}

public sealed class KeystoneSelectValidation
{
    public bool? IsRequired { get; set; }
}

public sealed class KeystoneSelectOption
{
    public required string Label { get; set; }

    public required KeystoneJsValue<string, int> Value { get; set; }
}

public sealed class KeystoneSelectUiOptions : KeystoneFieldUiOptions
{
    public KeystoneSelectUiMode? DisplayMode { get; set; }
}

public sealed class KeystoneSelectField() : KeystoneField<KeystoneSelectDbOptions, KeystoneSelectUiOptions>("select")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];

    public KeystoneSelectValidation? Validation { get; set; }
}

public sealed class KeystoneMultiselectDbOptions : FieldDbOptions
{
}

public sealed class KeystoneMultiselectField() : KeystoneField<KeystoneMultiselectDbOptions, KeystoneFieldUiOptions>("multiselect")
{
    public KeystoneSelectValueType? Type { get; set; }

    public KeystoneJsValue<string, int>[]? DefaultValue { get; set; }

    public KeystoneSelectOption[] Options { get; set; } = [];
}

public sealed class KeystoneForeignKeyOptions
{
    public string? Map { get; set; }
}

public sealed class KeystoneRelationshipDbOptions : FieldDbOptions
{
    public KeystoneJsValue<bool, KeystoneForeignKeyOptions>? ForeignKey { get; set; }
}

public sealed class KeystoneInlineCreateOptions
{
    public string[] Fields { get; set; } = [];
}

public sealed class KeystoneInlineConnectOptions
{
    public string? LabelField { get; set; }

    public string[]? SearchFields { get; set; }
}

public sealed class KeystoneRelationshipUiOptions : KeystoneFieldUiOptions
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

public sealed class KeystoneRelationshipField() : KeystoneField<KeystoneRelationshipDbOptions, KeystoneRelationshipUiOptions>("relationship")
{
    public string Ref { get; set; } = string.Empty;

    public bool? Many { get; set; }
}

public sealed class KeystoneEmptyDbOptions : FieldDbOptions
{
}

public sealed class KeystoneFileField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("file")
{
    public string Storage { get; set; } = string.Empty;
}

public sealed class KeystoneImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("image")
{
    public string Storage { get; set; } = string.Empty;
}

public sealed class KeystoneCloudinaryCredentials
{
    public string CloudName { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string ApiSecret { get; set; } = string.Empty;

    public string? Folder { get; set; }
}

public sealed class KeystoneCloudinaryImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneFieldUiOptions>("cloudinaryImage")
{
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
}

public sealed class KeystoneDocumentDbOptions : FieldDbOptions
{
}

public sealed class KeystoneDocumentRelationship
{
    public KeystoneDocumentRelationshipKind Kind { get; set; }
    public required string ListKey { get; set; }

    public required string Label { get; set; }

    public string? Selection { get; set; }
}

public sealed class KeystoneDocumentFormattingInlineMarks
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

public sealed class KeystoneDocumentFormattingOptions
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

public sealed class KeystoneDocumentLinksOptions
{
    public bool? Anchor { get; set; }
}

public sealed class KeystoneDocumentField() : KeystoneField<KeystoneDocumentDbOptions, KeystoneFieldUiOptions>("document")
{
    public Dictionary<string, KeystoneDocumentRelationship>? Relationships { get; set; }

    public Dictionary<string, object>? ComponentBlocks { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentFormattingOptions>? Formatting { get; set; }

    public KeystoneJsValue<bool, KeystoneDocumentLinksOptions>? Links { get; set; }

    public bool? Dividers { get; set; }

    public int[][]? Layouts { get; set; }
}

public sealed class KeystoneVirtualUiOptions : KeystoneFieldUiOptions
{
    public string? Query { get; set; }
}

public class KeystoneGraphqlObject(string name) : KeystoneJsObject(KeystoneImportObjects.Graphql, name);

public class KeystoneGraphqlField() : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Graphql, "field")
{
    public required KeystoneGraphqlObject Type { get; set; }
    public Dictionary<string, KeystoneGraphqlObject>? Args { get; set; }
    public required KeystoneJsFunction Resolve { get; set; }
}

public sealed class KeystoneVirtualField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneVirtualUiOptions>("virtual")
{
    public required KeystoneGraphqlField Field { get; set; }
}
