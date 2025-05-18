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
        public KeystoneDisplayMode? DisplayMode { get; init; }
        public object? DefaultValue { get; init; }
        public bool DbIsNullable { get; init; }
        public string? DbMap { get; init; }
        public string? DbNativeType { get; init; }
        public bool GraphqlReadIsNonNull { get; init; }
        public bool GraphqlCreateIsNonNull { get; init; }
        public bool GraphqlUpdateIsNonNull { get; init; }

        public KeystoneFieldAttribute(KeystoneFieldType fieldType)
        {
            FieldType = fieldType;
        }
    }
}
