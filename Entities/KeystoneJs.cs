using System.Reflection;
using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public class KeystoneJsFunction(string body, params string[] args)
{
    public KeystoneJsFunction(bool body, params string[] args) : this(body.ToString().ToLower(), args)
    {
    }

    public string Body { get; } = body;

    public string[] Args { get; } = args;
}

public class KeystoneJsObject(KeystoneImportObjects imports, string name)
{
    public string Name { get; } = name;
    public KeystoneImportObjects Imports { get; } = imports;

    public override string ToString() => this.Name;
}

public abstract class KeystoneJsFunctionCall(KeystoneImportObjects imports, string name)
{
    public KeystoneImportObjects Imports { get; } = imports;

    public string Name { get; } = name;

    public abstract object?[] Args { get; }
}

public abstract class KeystoneJsFunctionPropArgCall : KeystoneJsFunctionCall
{
    private readonly PropertyInfo[] properties;
    private readonly object? propObj;

    protected KeystoneJsFunctionPropArgCall(KeystoneImportObjects imports, string name, object? propObj) : base(imports, name)
    {
        this.propObj = propObj ?? this;
        this.properties = this.propObj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(x => x.Name != nameof(this.Args) && x.Name != nameof(this.Name) && x.Name != nameof(this.Imports)).ToArray();
    }

    public override object?[] Args =>
    [
        this.properties
            .Select(p => (name: Utils.ToCamelCase(p.Name), value: p.GetValue(this.propObj, null)))
            .Where(x => x.value != null)
            .ToDictionary(x => x.name, x => x.value)
    ];
}