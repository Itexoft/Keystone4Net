using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Microsoft.EntityFrameworkCore;

public abstract class KeystoneSession(string name) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Session, name);

public abstract class KeystoneCookieSession(string n) : KeystoneSession(n)
{
    public required string Secret { get; set; }

    public int? MaxAge { get; set; }

    public string? CookieName { get; set; }

    public bool? Secure { get; set; }

    public string? Path { get; set; }

    public string? Domain { get; set; }

    public KeystoneJsValue<bool, KeystoneCookieSameSite>? SameSite { get; set; }
}

public sealed class KeystoneStatelessSession() : KeystoneCookieSession("statelessSessions");

public sealed class KeystoneStoredSession() : KeystoneCookieSession("storedSessions")
{
    public KeystoneJsFunction? Store { get; set; }
}

public sealed class KeystoneStorageSigned
{
    public int Expiry { get; set; }
}

public sealed class KeystoneServerRoute
{
    public string Path { get; set; } = string.Empty;
}

public sealed class KeystoneStorageConfig
{
    public KeystoneStorageKind Kind { get; set; }
    public KeystoneStorageType Type { get; set; }
    public bool? Preserve { get; set; }
    public KeystoneJsFunction? TransformName { get; set; }
    public string? StoragePath { get; set; }
    public KeystoneServerRoute? ServerRoute { get; set; }
    public KeystoneJsFunction? GenerateUrl { get; set; }
    public string? BucketName { get; set; }
    public string? Region { get; set; }
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }
    public string? Endpoint { get; set; }
    public bool? ForcePathStyle { get; set; }
    public string? PathPrefix { get; set; }
    public KeystoneStorageSigned? Signed { get; set; }
    public string? Acl { get; set; }
}
