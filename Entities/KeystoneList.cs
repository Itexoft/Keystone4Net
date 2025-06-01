using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public abstract class KeystoneList(Type type) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Core, "list", null)
{
    [JsonIgnore]
    internal Type Type { get; } = type;

    internal KeystoneListDb Db { get; } = new();

    public KeystoneListAccess? Access { get; set; } = KeystoneListAccess.AllowAll;

    public KeystoneListUiOptions? Ui { get; set; }

    public KeystoneListGraphqlOptions? Graphql { get; set; }

    public object? Hooks { get; set; }

    public string? Description { get; set; }

    public bool IsSingleton { get; set; }

    public bool? DefaultIsFilterable { get; set; }

    public bool? DefaultIsOrderable { get; set; }

    public Dictionary<string, KeystoneField> Fields { get; } = [];

    public string Add(string key, KeystoneField value)
    {
        key = Utils.ToCamelCase(key);
        this.Fields.Add(key, value);

        return key;
    }

    public string Add(string key, KeystoneFieldType value, IKeystoneFieldOptions? options = null)
    {
        return this.Add(key, new(value, options));
    }
}

public sealed class KeystoneList<T>() : KeystoneList(typeof(T))
{
}