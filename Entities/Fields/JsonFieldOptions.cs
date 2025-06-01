using System.Text.Json;

namespace Keystone4Net.Entities;

public class KeystoneJsonFieldOptions : KeystoneFieldOptions
{
    public JsonElement? DefaultValue { get; set; }
    public KeystoneJsonDbOptions? Db { get; set; }
}

public class KeystoneJsonDbOptions
{
    public string? Map { get; set; }
}
