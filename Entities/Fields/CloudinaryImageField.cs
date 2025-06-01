namespace Keystone4Net.Entities;

public class KeystoneCloudinaryImageFieldOptions : KeystoneFieldOptions
{
    internal KeystoneCloudinaryDbOptions? Db { get; set; }
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
}

internal class KeystoneCloudinaryDbOptions
{
    public string? Map { get; set; }
}

public class KeystoneCloudinaryCredentials
{
    public string CloudName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string? Folder { get; set; }
}
