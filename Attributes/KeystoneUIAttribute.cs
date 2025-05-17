using Keystone4Net.CodeGeneration;

namespace Keystone4Net.Attributes;

public sealed class KeystoneUIAttribute() : KeystoneDbContextAttribute("ui")
{
    public bool IsDisabled { get; init; }

    public JsFunction IsAccessAllowed { get; init; } = new("true");
}