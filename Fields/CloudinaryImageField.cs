namespace Keystone4NET.Fields;

public class KeystoneCloudinaryCredentials
{
    public string CloudName { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string ApiSecret { get; set; } = string.Empty;

    public string? Folder { get; set; }
}

public class KeystoneCloudinaryImageField() : KeystoneField<KeystoneEmptyDbOptions, KeystoneCloudinaryImageUiOptions>("cloudinaryImage")
{
    public KeystoneCloudinaryCredentials Cloudinary { get; set; } = new();
}

public class KeystoneCloudinaryImageUiOptions : KeystoneFieldUiOptions
{
}
