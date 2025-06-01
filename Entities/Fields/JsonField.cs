using System.Text.Json;

namespace Keystone4Net.Entities;

public class KeystoneJsonFieldOptions : KeystoneFieldOptions
{
    public JsonElement? DefaultValue { get; set; }
    internal KeystoneJsonDbOptions? Db { get; set; }
}

internal class KeystoneJsonDbOptions
{
    public string? Map { get; set; }
}
