namespace Keystone4NET.Models;

public class KeystoneStorageSigned
{
    public int Expiry { get; set; }
}

public class KeystoneServerRoute
{
    public string Path { get; set; } = string.Empty;
}

public class KeystoneStorageConfig
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