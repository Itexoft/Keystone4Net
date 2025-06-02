using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Microsoft.EntityFrameworkCore;

public sealed class KeystoneFieldAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneFieldAccess AllowAll { get; } = new(nameof(AllowAll));

    public static KeystoneFieldAccess DenyAll { get; } = new(nameof(DenyAll));
}

public sealed class KeystoneListAccess(string name) : KeystoneJsObject(KeystoneImportObjects.Access, Utils.ToCamelCase(name))
{
    public static KeystoneListAccess AllowAll { get; } = new(nameof(AllowAll));

    public static KeystoneListAccess DenyAll { get; } = new(nameof(DenyAll));
}

public sealed class KeystoneOperationAccess
{
    public KeystoneJsFunction? Query { get; set; }

    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneFilterAccess
{
    public KeystoneJsFunction? Query { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneItemAccess
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public sealed class KeystoneListAccessControl
{
    public KeystoneOperationAccess? Operation { get; set; }

    public KeystoneFilterAccess? Filter { get; set; }

    public KeystoneItemAccess? Item { get; set; }
}

public class KeystoneFieldAccessControl
{
    public KeystoneJsFunction? Read { get; set; }

    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }
}
