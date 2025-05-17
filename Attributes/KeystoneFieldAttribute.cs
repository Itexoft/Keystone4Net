using System;
using Keystone4Net.Enums;

namespace Keystone4Net.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class KeystoneFieldAttribute : Attribute
    {
        public KeystoneFieldType FieldType { get; }
        public bool IsRequired { get; init; }
        public KeystoneIndex Index { get; init; }
        public string? DisplayMode { get; init; }

        public KeystoneFieldAttribute(KeystoneFieldType fieldType)
        {
            FieldType = fieldType;
        }
    }
}
