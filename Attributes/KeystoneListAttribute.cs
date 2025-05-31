using Keystone4Net.CodeGeneration;
using Keystone4Net.Enums;

namespace Keystone4Net.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class KeystoneListAttribute : KeystoneBaseAttribute
{
    public KeystoneListAttribute()
    {
    }

    public string? Path { get; init; }

    public KeystoneListAccess Access { get; init; } = KeystoneListAccess.AllowAll;

    public ListUiOptions? Ui { get; init; }
}
