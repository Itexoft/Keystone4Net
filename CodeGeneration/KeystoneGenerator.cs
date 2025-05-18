using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Keystone4Net.Attributes;
using Keystone4Net.Enums;
using Microsoft.EntityFrameworkCore;

namespace Keystone4Net.CodeGeneration
{
    public class KeystoneGenerator
    {
        private readonly DbContext _dbContext;

        public KeystoneGenerator(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GenerateKeystone()
        {
            var sb = new StringBuilder();
            var fieldImports = new HashSet<string>();

            sb.AppendLine("import { config, list } from '@keystone-6/core';");
            sb.AppendLine("import { allowAll } from '@keystone-6/core/access';");

            var listsBuilder = new StringBuilder();
            listsBuilder.AppendLine("  lists: {");

            var contextType = _dbContext.GetType();
            foreach (var property in contextType.GetProperties())
            {
                if (!IsDbSet(property.PropertyType, out var entityType))
                    continue;

                var listAttr = entityType.GetCustomAttribute<KeystoneListAttribute>();
                listsBuilder.AppendLine($"    {entityType.Name}: list({{");
                if (listAttr != null && !string.IsNullOrWhiteSpace(listAttr.Path))
                    listsBuilder.AppendLine($"      path: '{listAttr.Path}',");
                listsBuilder.AppendLine("      access: allowAll,");
                listsBuilder.AppendLine("      fields: {");

                foreach (var field in entityType.GetProperties())
                {
                    var fieldAttr = field.GetCustomAttribute<KeystoneFieldAttribute>();
                    var fieldType = fieldAttr?.FieldType ?? MapClrType(field.PropertyType);
                    var fieldTypeString = GetFieldTypeString(fieldType);
                    fieldImports.Add(fieldTypeString);

                    var optsStr = fieldAttr != null ? BuildFieldOptions(fieldAttr) : string.Empty;
                    listsBuilder.AppendLine($"        {field.Name}: {fieldTypeString}({optsStr}),");
                }

                listsBuilder.AppendLine("      },");
                listsBuilder.AppendLine("    }),");
            }

            listsBuilder.AppendLine("  }");

            sb.Append("import {");
            sb.Append(string.Join(", ", fieldImports));
            sb.AppendLine(" } from '@keystone-6/core/fields';");

            sb.AppendLine();
            sb.AppendLine("export default config({");
            sb.AppendLine("  db: {");
            sb.AppendLine($"    provider: '{GetProvider().ToString().ToLower()}',");
            sb.AppendLine($"    url: '{_dbContext.Database.GetConnectionString()}',");
            sb.AppendLine("  },");
            sb.Append(listsBuilder.ToString());
            sb.AppendLine("});");

            return sb.ToString();
        }

        private static bool IsDbSet(Type type, out Type entityType)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DbSet<>))
            {
                entityType = type.GetGenericArguments()[0];
                return true;
            }

            entityType = null!;
            return false;
        }

        private static KeystoneFieldType MapClrType(Type type)
        {
            if (type == typeof(string)) return KeystoneFieldType.Text;
            if (type == typeof(bool)) return KeystoneFieldType.Checkbox;
            if (type == typeof(int)) return KeystoneFieldType.Integer;
            if (type == typeof(long)) return KeystoneFieldType.BigInt;
            if (type == typeof(float) || type == typeof(double)) return KeystoneFieldType.Float;
            if (type == typeof(decimal)) return KeystoneFieldType.Decimal;
            if (type == typeof(DateTime)) return KeystoneFieldType.Timestamp;
            if (type.FullName == "System.DateOnly") return KeystoneFieldType.CalendarDay;
            if (type == typeof(Guid)) return KeystoneFieldType.Text;
            return KeystoneFieldType.Text;
        }

        private static string GetFieldTypeString(KeystoneFieldType type)
        {
            return type switch
            {
                KeystoneFieldType.Text => "text",
                KeystoneFieldType.Checkbox => "checkbox",
                KeystoneFieldType.Integer => "integer",
                KeystoneFieldType.BigInt => "bigInt",
                KeystoneFieldType.Float => "float",
                KeystoneFieldType.Decimal => "decimal",
                KeystoneFieldType.Password => "password",
                KeystoneFieldType.Timestamp => "timestamp",
                KeystoneFieldType.CalendarDay => "calendarDay",
                KeystoneFieldType.Json => "json",
                KeystoneFieldType.Multiselect => "multiselect",
                KeystoneFieldType.Select => "select",
                KeystoneFieldType.Document => "document",
                KeystoneFieldType.Relationship => "relationship",
                KeystoneFieldType.Virtual => "virtual",
                KeystoneFieldType.File => "file",
                KeystoneFieldType.Image => "image",
                KeystoneFieldType.CloudinaryImage => "cloudinaryImage",
                _ => "text"
            };
        }

        private static string BuildFieldOptions(KeystoneFieldAttribute attr)
        {
            var opts = new List<string>();
            if (attr.IsRequired)
                opts.Add("validation: { isRequired: true }");
            if (attr.MinLength.HasValue || attr.MaxLength.HasValue || !string.IsNullOrWhiteSpace(attr.MatchRegex))
            {
                var val = new List<string>();
                if (attr.MinLength.HasValue)
                    val.Add($"length: {{ min: {attr.MinLength.Value} }}");
                if (attr.MaxLength.HasValue)
                    val.Add($"length: {{ max: {attr.MaxLength.Value} }}");
                if (!string.IsNullOrWhiteSpace(attr.MatchRegex))
                {
                    var expl = !string.IsNullOrWhiteSpace(attr.MatchExplanation) ? $", explanation: '{attr.MatchExplanation}'" : string.Empty;
                    val.Add($"match: {{ regex: /{attr.MatchRegex}/{expl} }}");
                }
                if (val.Count > 0)
                    opts.Add($"validation: {{ {string.Join(", ", val)} }}");
            }
            if (attr.Min != null || attr.Max != null)
            {
                var val = new List<string>();
                if (attr.Min != null)
                    val.Add($"min: {FormatJsValue(attr.Min)}");
                if (attr.Max != null)
                    val.Add($"max: {FormatJsValue(attr.Max)}");
                opts.Add($"validation: {{ {string.Join(", ", val)} }}");
            }
            if (attr.Index == KeystoneIndex.Indexed)
                opts.Add("isIndexed: true");
            else if (attr.Index == KeystoneIndex.Unique)
                opts.Add("isIndexed: 'unique'");
            if (attr.DisplayMode.HasValue)
                opts.Add($"ui: {{ displayMode: '{FormatDisplayMode(attr.DisplayMode.Value)}' }}");
            if (!attr.IsFilterable)
                opts.Add("isFilterable: false");
            if (!attr.IsOrderable)
                opts.Add("isOrderable: false");
            if (!string.IsNullOrWhiteSpace(attr.Label))
                opts.Add($"label: '{attr.Label}'");
            if (attr.DefaultValue != null)
                opts.Add($"defaultValue: {FormatJsValue(attr.DefaultValue)}");
            if (attr.DbIsNullable || !string.IsNullOrWhiteSpace(attr.DbMap) || !string.IsNullOrWhiteSpace(attr.DbNativeType) || attr.DbUpdatedAt)
            {
                var db = new List<string>();
                if (attr.DbIsNullable)
                    db.Add("isNullable: true");
                if (!string.IsNullOrWhiteSpace(attr.DbMap))
                    db.Add($"map: '{attr.DbMap}'");
                if (!string.IsNullOrWhiteSpace(attr.DbNativeType))
                    db.Add($"nativeType: '{attr.DbNativeType}'");
                if (attr.DbUpdatedAt)
                    db.Add("updatedAt: true");
                opts.Add($"db: {{ {string.Join(", ", db)} }}");
            }
            if (attr.GraphqlReadIsNonNull || attr.GraphqlCreateIsNonNull || attr.GraphqlUpdateIsNonNull || attr.GraphqlOmitRead || attr.GraphqlOmitCreate || attr.GraphqlOmitUpdate)
            {
                var gql = new List<string>();
                var nn = new List<string>();
                if (attr.GraphqlReadIsNonNull)
                    nn.Add("read: true");
                if (attr.GraphqlCreateIsNonNull)
                    nn.Add("create: true");
                if (attr.GraphqlUpdateIsNonNull)
                    nn.Add("update: true");
                if (nn.Count > 0)
                    gql.Add($"isNonNull: {{ {string.Join(", ", nn)} }}");
                var omit = new List<string>();
                if (attr.GraphqlOmitRead)
                    omit.Add("read: true");
                if (attr.GraphqlOmitCreate)
                    omit.Add("create: true");
                if (attr.GraphqlOmitUpdate)
                    omit.Add("update: true");
                if (omit.Count > 0)
                    gql.Add($"omit: {{ {string.Join(", ", omit)} }}");
                if (gql.Count > 0)
                    opts.Add($"graphql: {{ {string.Join(", ", gql)} }}");
            }

            return opts.Count > 0 ? $"{{ {string.Join(", ", opts)} }}" : string.Empty;
        }

        private static string FormatDisplayMode(KeystoneFieldDisplayMode mode) => mode switch
        {
            KeystoneFieldDisplayMode.Input => "input",
            KeystoneFieldDisplayMode.Textarea => "textarea",
            KeystoneFieldDisplayMode.Select => "select",
            KeystoneFieldDisplayMode.SegmentedControl => "segmented-control",
            KeystoneFieldDisplayMode.Radio => "radio",
            KeystoneFieldDisplayMode.Cards => "cards",
            KeystoneFieldDisplayMode.Count => "count",
            _ => "input"
        };

        private static string FormatJsValue(object value)
        {
            return value switch
            {
                string s => $"'{s}'",
                bool b => b.ToString().ToLowerInvariant(),
                _ => Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture) ?? "null"
            };
        }

        private KeystoneDbProvider GetProvider()
        {
            var provider = _dbContext.Database.ProviderName?.ToLowerInvariant() ?? string.Empty;
            if (provider.Contains("sqlite"))
                return KeystoneDbProvider.Sqlite;
            if (provider.Contains("npgsql"))
                return KeystoneDbProvider.Postgresql;
            if (provider.Contains("mysql"))
                return KeystoneDbProvider.Mysql;
            return KeystoneDbProvider.Sqlite;
        }
    }
}
