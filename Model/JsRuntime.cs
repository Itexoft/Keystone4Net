using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Common;
using Microsoft.EntityFrameworkCore;

public abstract class KeystoneJsValue
{
    internal object? Value { get; }
    private protected KeystoneJsValue(object? value) => this.Value = value;
    public override string? ToString() => this.Value?.ToString();
}

public class KeystoneJsValue<T1, T2> : KeystoneJsValue
{
    private protected KeystoneJsValue(object? value) : base(value) { }
    public static implicit operator KeystoneJsValue<T1, T2>(T1 value) => new(value);
    public static implicit operator KeystoneJsValue<T1, T2>(T2 value) => new(value);
}

public class KeystoneJsValue<T1, T2, T3> : KeystoneJsValue<T1, T2>
{
    private protected KeystoneJsValue(object? value) : base(value) { }
    public static implicit operator KeystoneJsValue<T1, T2, T3>(T3 value) => new(value);
}

public class KeystoneJsObject(KeystoneImportObjects? imports, string name)
{
    public KeystoneJsObject(string name) : this(null, name) { }

    public KeystoneImportObjects? Imports { get; } = imports;

    public string Name { get; } = name;

    public override string ToString()
    {
        return this.Name;
    }
}

public sealed class KeystoneJsFunction(string body, params string[] args)
{
    public string Body { get; } = body;

    public string[] Args { get; } = args;

    public static implicit operator KeystoneJsFunction(bool body) => new(body.ToString().ToLower());
}

public abstract class KeystoneJsFunctionCall(KeystoneImportObjects imports, string name)
{
    public KeystoneImportObjects Imports { get; } = imports;

    public string Name { get; } = name;

    public abstract object?[] Arguments { get; }
}

public abstract class KeystoneJsFunctionPropArgCall : KeystoneJsFunctionCall
{
    private readonly PropertyInfo[] props;

    protected KeystoneJsFunctionPropArgCall(KeystoneImportObjects imports, string name) : base(imports, name)
    {
        this.props = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.Name is not (nameof(this.Arguments) or nameof(this.Name) or nameof(this.Imports))).ToArray();
    }

    public override object?[] Arguments =>
    [
        this.props.Select(p => (Utils.ToCamelCase(p.Name), p.GetValue(this))).Where(x => x.Item2 != null)
            .ToDictionary(x => x.Item1, x => x.Item2)
    ];
}
