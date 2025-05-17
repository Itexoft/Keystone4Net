using Keystone4Net.CodeGeneration;
using Keystone4Net.Enums;

namespace Keystone4Net.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public abstract class KeystoneSessionAttribute() : KeystoneDbContextAttribute("session")
{
}

public sealed class KeystoneStatelessSessionAttribute(string? secret, TimeSpan maxAge) : KeystoneSessionAttribute
{
    public KeystoneStatelessSessionAttribute(string? secret) : this(secret, TimeSpan.MaxValue)
    {
    }

    public KeystoneStatelessSessionAttribute() : this(null)
    {
    }

    public string? Secret { get; init; } = secret;

    public long MaxAge { get; init; } = (long)maxAge.TotalSeconds;

    internal override object Build() => new JsFunctionCall(KeystoneImportObjects.Session, "statelessSessions", base.Build());
}