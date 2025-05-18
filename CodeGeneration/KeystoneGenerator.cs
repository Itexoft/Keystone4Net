using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Keystone4Net.Attributes;
using Keystone4Net.Enums;
using Keystone4Net.Extensions;
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
            var sb = new StringBuilder("""
import { config, list } from '@keystone-6/core';
import { allowAll } from '@keystone-6/core/access';
""");
            var fieldImports = new HashSet<string>();

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
                    var fieldTypeString = fieldType.ToJs();
                    fieldImports.Add(fieldTypeString);

                    var optsStr = BuildFieldOptions(fieldAttr);
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
            sb.AppendLine($$"""
export default config({
  db: {
    provider: '{GetProvider().ToString().ToLower()}',
    url: '{_dbContext.Database.GetConnectionString()}',
  },
""");
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


        private static string FormatJsValue(object value)
        {
            return value switch
            {
                string s => $"'{s}'",
                bool b => b.ToString().ToLowerInvariant(),
                _ => Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture) ?? "null"
            };
        }

        private static string BuildFieldOptions(KeystoneFieldAttribute? fieldAttr)
        {
            if (fieldAttr == null) return string.Empty;

            var opts = new List<string>();

            if (fieldAttr.IsRequired)
                opts.Add("validation: { isRequired: true }");

            if (fieldAttr.Index == KeystoneIndex.Indexed)
                opts.Add("isIndexed: true");
            else if (fieldAttr.Index == KeystoneIndex.Unique)
                opts.Add("isIndexed: 'unique'");

            if (fieldAttr.DisplayMode.HasValue)
                opts.Add($"ui: {{ displayMode: '{fieldAttr.DisplayMode.Value.ToJs()}' }}");

            if (fieldAttr.DefaultValue != null)
                opts.Add($"defaultValue: {FormatJsValue(fieldAttr.DefaultValue)}");

            if (fieldAttr.DbIsNullable || !string.IsNullOrWhiteSpace(fieldAttr.DbMap) || !string.IsNullOrWhiteSpace(fieldAttr.DbNativeType))
            {
                var dbOpts = new List<string>();
                if (fieldAttr.DbIsNullable)
                    dbOpts.Add("isNullable: true");
                if (!string.IsNullOrWhiteSpace(fieldAttr.DbMap))
                    dbOpts.Add($"map: '{fieldAttr.DbMap}'");
                if (!string.IsNullOrWhiteSpace(fieldAttr.DbNativeType))
                    dbOpts.Add($"nativeType: '{fieldAttr.DbNativeType}'");
                opts.Add($"db: {{ {string.Join(", ", dbOpts)} }}");
            }

            if (fieldAttr.GraphqlReadIsNonNull || fieldAttr.GraphqlCreateIsNonNull || fieldAttr.GraphqlUpdateIsNonNull)
            {
                var nn = new List<string>();
                if (fieldAttr.GraphqlReadIsNonNull)
                    nn.Add("read: true");
                if (fieldAttr.GraphqlCreateIsNonNull)
                    nn.Add("create: true");
                if (fieldAttr.GraphqlUpdateIsNonNull)
                    nn.Add("update: true");
                opts.Add($"graphql: {{ isNonNull: {{ {string.Join(", ", nn)} }} }}");
            }

            return opts.Count > 0 ? $"{{ {string.Join(", ", opts)} }}" : string.Empty;
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
