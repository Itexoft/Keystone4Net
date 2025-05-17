using System.Reflection;
using Keystone4NET.Common;

namespace Keystone4NET.Models;

public abstract class KeystoneJsValue
{
    private protected KeystoneJsValue(object? value)
    {
        this.Value = value;
    }

    internal object? Value { get; }

    public override string? ToString()
    {
        return this.Value?.ToString();
    }
}

public class KeystoneJsValue<T1, T2> : KeystoneJsValue
{
    private protected KeystoneJsValue(object? value) : base(value)
    {
    }

    public static implicit operator KeystoneJsValue<T1, T2>(T1 value)
    {
        return new(value);
    }

    public static implicit operator KeystoneJsValue<T1, T2>(T2 value)
    {
        return new(value);
    }
}

public class KeystoneJsValue<T1, T2, T3> : KeystoneJsValue<T1, T2>
{
    private protected KeystoneJsValue(object? value) : base(value)
    {
    }

    public static implicit operator KeystoneJsValue<T1, T2, T3>(T3 value)
    {
        return new(value);
    }
}

public class KeystoneJsObject(KeystoneImport? import, string name)
{
    public KeystoneJsObject(string name) : this(null, name)
    {
    }

    internal KeystoneImport? Import { get; } = import;

    public string Name { get; } = name;

    public override string ToString()
    {
        return this.Name;
    }
}

public class KeystoneJsFunction(string body, params string[] args)
{
    public string Body { get; } = body;

    public string[] Args { get; } = args;

    public static implicit operator KeystoneJsFunction(bool body)
    {
        return new(body.ToString().ToLower());
    }
}

public abstract class KeystoneJsFunctionCall(KeystoneImport import, string funcName)
{
    internal KeystoneImport Import { get; } = import;

    public string FuncName { get; } = funcName;

    public abstract object?[] Arguments { get; }
}

public abstract class KeystoneJsFunctionObjectCall : KeystoneJsFunctionCall
{
    private readonly PropertyInfo[] props;

    protected KeystoneJsFunctionObjectCall(KeystoneImport import, string funcName) : base(import, funcName)
    {
        this.props = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.Name is not (nameof(this.Arguments) or nameof(this.FuncName) or nameof(this.Import))).ToArray();
    }

    public override object?[] Arguments =>
    [
        this.props.Select(p => (Utils.ToCamelCase(p.Name), p.GetValue(this))).Where(x => x.Item2 != null)
            .ToDictionary(x => x.Item1, x => x.Item2)
    ];
}