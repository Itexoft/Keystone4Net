namespace Keystone4NET.Models;

public sealed record KeystoneImport(string name, string? importFrom = null)
{
    public static KeystoneImport Core { get; } = new("core", "core");

    public static KeystoneImport Access { get; } = new("access", "core/access");

    public static KeystoneImport Fields { get; } = new("fields", "core/fields");

    public static KeystoneImport Session { get; } = new("session", "core/session");

    public static KeystoneImport Graphql { get; } = new("core.graphql");

    public string? importFrom { get; } = importFrom;

    public string Name { get; } = name;

    public override string ToString()
    {
        return this.Name;
    }
}