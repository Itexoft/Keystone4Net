using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Keystone4Net.Attributes;
using Keystone4Net.Common;
using Keystone4Net.Enums;
using Microsoft.EntityFrameworkCore;

namespace Keystone4Net.CodeGeneration;

public class KeystoneOptionsSerializer
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        IncludeFields = true,
        PropertyNamingPolicy = new CamelCase(),
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters =
        {
            new EnumFunctionConverter(),
            new JsonStringEnumConverter(new CamelCase()),
            new FunctionConverter(),
            new FunctionCallConverter()
        }
    };

    private static string Serialize(object value) => JsonSerializer.Serialize(value, jsonSerializerOptions);

    public string Serialize(DbContext dbContext)
    {
        var dataSource = dbContext.Database.GetDbConnection().DataSource;

        if (!Uri.TryCreate(dataSource, UriKind.Absolute, out var uri))
            throw new InvalidOperationException(nameof(dataSource));
        
        var result = new Dictionary<string, object>();
        result.Add("db", new { provider = this.GetProvider(dbContext), url = uri.ToString() });
        foreach (var attr in dbContext.GetType().GetCustomAttributes<KeystoneDbContextAttribute>())
            result.Add(attr.Name, attr.Build());
        result.Add("lists", this.BuildLists(dbContext));

        return Serialize(result);
    }

    private Dictionary<string, object> BuildLists(DbContext dbContext)
    {
        var result = new Dictionary<string, object>();
        foreach (var prop in dbContext.GetType().GetProperties())
        {
            if (!prop.PropertyType.IsGenericType || prop.PropertyType.GetGenericTypeDefinition() != typeof(DbSet<>))
                continue;
            
            var type = prop.PropertyType.GetGenericArguments()[0];

            result.Add(type.Name, this.BuildList(type));
        }

        return result;
    }

    private JsFunctionCall BuildList(Type t)
    {
        var attr = t.GetCustomAttribute<KeystoneListAttribute>() ?? new KeystoneListAttribute();
        var result = (Dictionary<string, object?>)attr.Build();
        var fields = t.GetProperties().ToDictionary(p => p.Name, this.BuildField);
        result.Add(nameof(fields), fields);
        return new(KeystoneImportObjects.Core, "list", result);
    }

    private object BuildField(PropertyInfo p)
    {
        var attr = p.GetCustomAttribute<KeystoneFieldAttribute>() ?? new KeystoneFieldAttribute(MapClr(p.PropertyType));
        return attr.Build();
    }

    private static KeystoneFieldType MapClr(Type t)
    {
        if (t == typeof(bool))
            return KeystoneFieldType.Checkbox;
        if (t == typeof(int))
            return KeystoneFieldType.Integer;
        if (t == typeof(long))
            return KeystoneFieldType.BigInt;
        if (t == typeof(float) || t == typeof(double))
            return KeystoneFieldType.Float;
        if (t == typeof(decimal))
            return KeystoneFieldType.Decimal;
        if (t == typeof(DateTime))
            return KeystoneFieldType.Timestamp;
        if (t == typeof(DateOnly))
            return KeystoneFieldType.CalendarDay;

        return KeystoneFieldType.Text;
    }

    private KeystoneDbProvider GetProvider(DbContext dbContext)
    {
        var provider = dbContext.Database.ProviderName?.ToLowerInvariant() ?? string.Empty;

        foreach (var value in Enum.GetValues<KeystoneDbProvider>())
            if (provider.Contains(Utils.ToCamelCase(value)))
                return value;

        throw new NotSupportedException(provider);
    }

    private sealed class FunctionConverter : JsonConverter<JsFunction>
    {
        public override JsFunction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, JsFunction value, JsonSerializerOptions options)
        {
            var args = string.Join(", ", value.Args);
            writer.WriteRawValue($"({args}) => {value.Body}", true);
        }
    }

    private sealed class FunctionCallConverter : JsonConverter<JsFunctionCall>
    {
        public override JsFunctionCall? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, JsFunctionCall value, JsonSerializerOptions options)
        {
            var args = string.Join(", ", value.Args.Select(x => JsonSerializer.Serialize(x, options)));
            var obj = Utils.ToCamelCase(value.Object.ToString());
            writer.WriteRawValue($"{obj}.{value.Name}({args})", true);
        }
    }

    private sealed class CamelCase : JsonNamingPolicy
    {
        public override string ConvertName(string str)
        {
            return Utils.ToCamelCase(str);
        }
    }

    private sealed class EnumFunctionConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum && typeToConvert.GetCustomAttribute<KeystoneEnumAttribute>() != null;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var attr = typeToConvert.GetCustomAttribute<KeystoneEnumAttribute>()!;
            var obj = Utils.ToCamelCase(attr.Object.ToString());
            var converterType = typeof(EnumConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType, obj)!;
        }

        private sealed class EnumConverter<T> : JsonConverter<T> where T : struct, Enum
        {
            private readonly string obj;

            public EnumConverter(string obj)
            {
                this.obj = obj;
            }

            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                var name = Utils.ToCamelCase(value.ToString());
                writer.WriteRawValue($"{obj}.{name}", true);
            }
        }
    }
}
