using System;

namespace Keystone4Net.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class KeystoneListAttribute : Attribute
    {
        public string Label { get; }
        public string? Path { get; init; }
        public string? Singular { get; init; }
        public string? Plural { get; init; }

        public KeystoneListAttribute(string label)
        {
            Label = label;
        }
    }
}
