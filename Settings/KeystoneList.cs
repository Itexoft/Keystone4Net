using Keystone4Net.CodeGeneration;
using Keystone4Net.Enums;

namespace Keystone4Net.Settings;

public abstract class KeystoneList
{
    private readonly Dictionary<string, KeystoneField> fields = new();

    protected KeystoneList(string name, Type entityType)
    {
        Name = name;
        EntityType = entityType;
    }

    public string Name { get; }
    public Type EntityType { get; }
    public KeystoneListAccess Access { get; set; }
    public ListUiOptions? Ui { get; set; }
    public IReadOnlyDictionary<string, KeystoneField> Fields => fields;

    public void AddField(string name, KeystoneFieldType type, object? options = null)
    {
        fields[name] = new KeystoneField(name, type, options);
    }
}

public sealed class KeystoneList<T> : KeystoneList
{
    public KeystoneList(string name) : base(name, typeof(T))
    {
    }
}

public sealed record KeystoneField(string Name, KeystoneFieldType Type, object? Options);
