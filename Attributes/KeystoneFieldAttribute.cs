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
        public KeystoneFieldDisplayMode? DisplayMode { get; init; }
        public bool IsFilterable { get; init; } = true;
        public bool IsOrderable { get; init; } = true;
        public string? Label { get; init; }
        public object? DefaultValue { get; init; }
        public bool DbIsNullable { get; init; }
        public string? DbMap { get; init; }
        public string? DbNativeType { get; init; }
        public bool DbUpdatedAt { get; init; }
        public int? MinLength { get; init; }
        public int? MaxLength { get; init; }
        public string? MatchRegex { get; init; }
        public string? MatchExplanation { get; init; }
        public object? Min { get; init; }
        public object? Max { get; init; }
        public bool GraphqlReadIsNonNull { get; init; }
        public bool GraphqlCreateIsNonNull { get; init; }
        public bool GraphqlUpdateIsNonNull { get; init; }
        public bool GraphqlOmitRead { get; init; }
        public bool GraphqlOmitCreate { get; init; }
        public bool GraphqlOmitUpdate { get; init; }

        public KeystoneFieldAttribute(KeystoneFieldType fieldType)
        {
            FieldType = fieldType;
        }
    }
}
