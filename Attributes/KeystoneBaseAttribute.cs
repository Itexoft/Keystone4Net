using System.Reflection;

namespace Keystone4Net.Attributes;

public abstract class KeystoneBaseAttribute : Attribute
{
    internal virtual object Build()
    {
        return this.GetType().GetProperties(BindingFlags.Instance| BindingFlags.Public | BindingFlags.DeclaredOnly).ToDictionary(p => p.Name, x => x.GetValue(this, null));
    }
}