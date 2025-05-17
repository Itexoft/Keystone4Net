using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneCloudinaryCredentials
{
    public string CloudName { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string ApiSecret { get; set; } = string.Empty;

    public string? Folder { get; set; }
}

public class KeystoneCloudinaryImageField(string name)
    : KeystoneField<KeystoneCloudinaryCredentialsDbOptions, KeystoneCloudinaryImageUiOptions>(name, "cloudinaryImage")
{
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
}

public class KeystoneCloudinaryCredentialsDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneCloudinaryImageUiOptions : KeystoneFieldUiOptions
{
}