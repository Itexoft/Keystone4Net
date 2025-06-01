using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public class KeystoneField(KeystoneFieldType type, KeystoneFieldOptions? options)
    : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Fields, Utils.ToCamelCase(type), options)
{
    public KeystoneFieldType Type { get; set; } = type;

    public KeystoneFieldOptions? Options { get; set; } = options;
}
