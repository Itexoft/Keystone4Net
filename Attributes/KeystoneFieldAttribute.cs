using Keystone4Net.CodeGeneration;
using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class KeystoneFieldAttribute(KeystoneFieldType fieldType) : KeystoneBaseAttribute
{
    public KeystoneFieldType FieldType { get; } = fieldType;

    public object? Options { get; init; }
    
    public ListUiOptions Ui { get; init; }

    internal override object Build()
    {
        return new JsFunctionCall(KeystoneImportObjects.Fields, Utils.ToCamelCase(this.FieldType), this.Options);
    }
}