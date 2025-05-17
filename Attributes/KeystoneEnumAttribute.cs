using Keystone4Net.Enums;

namespace Keystone4Net.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
internal sealed class KeystoneEnumAttribute(KeystoneImportObjects obj) : Attribute
{
    public KeystoneImportObjects Object { get; } = obj;
}