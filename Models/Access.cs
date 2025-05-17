using Keystone4NET.Common;

namespace Keystone4NET.Models;

public class KeystoneFieldAccess(string name) : KeystoneJsObject(KeystoneImport.Access, Utils.ToCamelCase(name))
{
    public static KeystoneFieldAccess AllowAll { get; } = new(nameof(AllowAll));

    public static KeystoneFieldAccess DenyAll { get; } = new(nameof(DenyAll));
}

public class KeystoneListAccess(string name) : KeystoneJsObject(KeystoneImport.Access, Utils.ToCamelCase(name))
{
    public static KeystoneListAccess AllowAll { get; } = new(nameof(AllowAll));

    public static KeystoneListAccess DenyAll { get; } = new(nameof(DenyAll));
}

public class KeystoneOperationAccess
{
    public KeystoneJsFunction? Query { get; set; }

    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public class KeystoneFilterAccess
{
    public KeystoneJsFunction? Query { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public class KeystoneItemAccess
{
    public KeystoneJsFunction? Create { get; set; }

    public KeystoneJsFunction? Update { get; set; }

    public KeystoneJsFunction? Delete { get; set; }
}

public class KeystoneListAccessControl
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