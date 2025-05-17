namespace Keystone4Net.Common;

public static class Utils
{
    public static string ToCamelCase(string str) => string.IsNullOrEmpty(str) ? str : char.ToLowerInvariant(str[0]) + str[1..];
    public static string ToCamelCase(Enum e) => ToCamelCase(e.ToString());
}