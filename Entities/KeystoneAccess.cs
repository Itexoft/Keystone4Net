using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public class KeystoneFieldAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneFieldAccess AllowAll { get; } = new(nameof(AllowAll));
    public static KeystoneFieldAccess DenyAll { get; } = new(nameof(DenyAll));
    public static KeystoneFieldAccess AllOperations { get; } = new(nameof(AllOperations));
    public static KeystoneFieldAccess Unfiltered { get; } = new(nameof(Unfiltered));
}

public class KeystoneListAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneListAccess AllowAll { get; } = new(nameof(AllowAll));
    public static KeystoneListAccess DenyAll { get; } = new(nameof(DenyAll));
    public static KeystoneListAccess AllOperations { get; } = new(nameof(AllOperations));
    public static KeystoneListAccess Unfiltered { get; } = new(nameof(Unfiltered));
}