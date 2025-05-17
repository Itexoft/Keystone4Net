using System.Reflection;

namespace Keystone4Net.Common;

internal static class Utils
{
    public static string ToCamelCase(string str) => string.IsNullOrEmpty(str) ? str : char.ToLowerInvariant(str[0]) + str[1..];
    public static string ToCamelCase<T>(T e) where T: Enum => ToCamelCase(e.ToString());
}