namespace Keystone4Net.Attributes;

public abstract class KeystoneDbContextAttribute(string name) : KeystoneBaseAttribute
{
    internal string Name { get; } = name;
}