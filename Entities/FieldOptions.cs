namespace Keystone4Net.Entities;

public class KeystoneCheckboxFieldOptions : KeystoneFieldOptions
{
    public bool? DefaultValue { get; set; }
    public KeystoneCheckboxDbOptions? Db { get; set; }
}

public class KeystoneIntegerFieldOptions : KeystoneFieldOptions
{
    public int? DefaultValue { get; set; }
    public KeystoneIntegerDbOptions? Db { get; set; }
    public KeystoneIntegerValidationOptions? Validation { get; set; }
}

public class KeystoneBigIntFieldOptions : KeystoneFieldOptions
{
    public long? DefaultValue { get; set; }
    public KeystoneBigIntDbOptions? Db { get; set; }
    public KeystoneBigIntValidationOptions? Validation { get; set; }
}

public class KeystoneFloatFieldOptions : KeystoneFieldOptions
{
    public double? DefaultValue { get; set; }
    public KeystoneFloatDbOptions? Db { get; set; }
    public KeystoneFloatValidationOptions? Validation { get; set; }
}

public class KeystoneDecimalFieldOptions : KeystoneFieldOptions
{
    public decimal? DefaultValue { get; set; }
    public KeystoneDecimalDbOptions? Db { get; set; }
    public KeystoneDecimalValidationOptions? Validation { get; set; }
}

public class KeystonePasswordFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    public KeystonePasswordDbOptions? Db { get; set; }
    public KeystonePasswordValidationOptions? Validation { get; set; }
}

public class KeystoneTimestampFieldOptions : KeystoneFieldOptions
{
    public DateTime? DefaultValue { get; set; }
    public KeystoneTimestampDbOptions? Db { get; set; }
    public KeystoneTimestampValidationOptions? Validation { get; set; }
}

public class KeystoneCalendarDayFieldOptions : KeystoneFieldOptions
{
    public string? DefaultValue { get; set; }
    public KeystoneCalendarDayDbOptions? Db { get; set; }
    public KeystoneCalendarDayValidationOptions? Validation { get; set; }
}

public class KeystoneJsonFieldOptions : KeystoneFieldOptions
{
    public object? DefaultValue { get; set; }
    public KeystoneJsonDbOptions? Db { get; set; }
}

public class KeystoneBytesFieldOptions : KeystoneFieldOptions
{
    public byte[]? DefaultValue { get; set; }
    public KeystoneBytesDbOptions? Db { get; set; }
}

public class KeystoneMultiselectFieldOptions : KeystoneFieldOptions
{
    public string[]? DefaultValue { get; set; }
    public object[]? Options { get; set; }
    public KeystoneMultiselectDbOptions? Db { get; set; }
}

public class KeystoneSelectFieldOptions : KeystoneFieldOptions
{
    public object? DefaultValue { get; set; }
    public object[]? Options { get; set; }
    public KeystoneSelectDbOptions? Db { get; set; }
    public KeystoneSelectValidationOptions? Validation { get; set; }
}

public class KeystoneDocumentFieldOptions : KeystoneFieldOptions
{
    public KeystoneDocumentDbOptions? Db { get; set; }
}

public class KeystoneRelationshipFieldOptions : KeystoneFieldOptions
{
    public bool? Many { get; set; }
    public string Ref { get; set; } = string.Empty;
    public KeystoneRelationshipDbOptions? Db { get; set; }
    public KeystoneRelationshipUiOptions? Ui { get; set; }
}

public class KeystoneVirtualFieldOptions : KeystoneFieldOptions
{
}

public class KeystoneFileFieldOptions : KeystoneFieldOptions
{
    public KeystoneFileDbOptions? Db { get; set; }
    public object? Storage { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
}

public class KeystoneImageFieldOptions : KeystoneFieldOptions
{
    public KeystoneImageDbOptions? Db { get; set; }
    public object? Storage { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
}

public class KeystoneCloudinaryImageFieldOptions : KeystoneFieldOptions
{
    public KeystoneCloudinaryDbOptions? Db { get; set; }
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
}

public class KeystoneCheckboxDbOptions { }
public class KeystoneIntegerDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneIntegerValidationOptions { public bool IsRequired { get; set; } public int? Min { get; set; } public int? Max { get; set; } }
public class KeystoneBigIntDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneBigIntValidationOptions { public bool IsRequired { get; set; } public long? Min { get; set; } public long? Max { get; set; } }
public class KeystoneFloatDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneFloatValidationOptions { public bool IsRequired { get; set; } public double? Min { get; set; } public double? Max { get; set; } }
public class KeystoneDecimalDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneDecimalValidationOptions { public bool IsRequired { get; set; } public decimal? Min { get; set; } public decimal? Max { get; set; } }
public class KeystonePasswordDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystonePasswordValidationOptions { public bool IsRequired { get; set; } public bool RejectCommon { get; set; } public KeystoneTextMatchOptions? Match { get; set; } public KeystoneTextLengthOptions? Length { get; set; } }
public class KeystoneTimestampDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneTimestampValidationOptions { public bool IsRequired { get; set; } }
public class KeystoneCalendarDayDbOptions { public bool? IsNullable { get; set; } }
public class KeystoneCalendarDayValidationOptions { public bool IsRequired { get; set; } }
public class KeystoneJsonDbOptions { public string? Map { get; set; } }
public class KeystoneBytesDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneMultiselectDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneSelectDbOptions { public bool? IsNullable { get; set; } public string? Map { get; set; } }
public class KeystoneSelectValidationOptions { public bool IsRequired { get; set; } }
public class KeystoneDocumentDbOptions { public string? Map { get; set; } }
public class KeystoneRelationshipDbOptions { public string? RelationName { get; set; } }
public class KeystoneRelationshipUiOptions { public bool? HideCreate { get; set; } }
public class KeystoneFileDbOptions { public string? Map { get; set; } }
public class KeystoneImageDbOptions { public string? Map { get; set; } }
public class KeystoneCloudinaryDbOptions { public string? Map { get; set; } }
public class KeystoneCloudinaryCredentials { public string CloudName { get; set; } = string.Empty; public string ApiKey { get; set; } = string.Empty; public string ApiSecret { get; set; } = string.Empty; public string? Folder { get; set; } }
