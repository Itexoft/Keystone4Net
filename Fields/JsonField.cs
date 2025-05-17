using System.Text.Json;
using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneJsonField(string name) : KeystoneField<KeystoneJsonDbOptions, KeystoneJsonUiOptions>(name, "json")
{
    public JsonElement? DefaultValue { get; set; }
}

public class KeystoneJsonDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneJsonUiOptions : KeystoneFieldUiOptions
{
}