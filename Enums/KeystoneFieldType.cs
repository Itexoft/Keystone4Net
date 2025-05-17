using Keystone4Net.Attributes;

namespace Keystone4Net.Enums;

[KeystoneEnum(KeystoneImportObjects.Fields)]
public enum KeystoneFieldType
{
    Text,
    Checkbox,
    Integer,
    BigInt,
    Float,
    Decimal,
    Password,
    Timestamp,
    CalendarDay,
    Json,
    Multiselect,
    Select,
    Document,
    Relationship,
    Virtual,
    File,
    Image,
    CloudinaryImage
}