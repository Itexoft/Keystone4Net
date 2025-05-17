namespace Keystone4NET.Common;

internal static class Utils
{
    public static string ToCamelCase(string str)
    {
        return string.IsNullOrEmpty(str) ? str : char.ToLowerInvariant(str[0]) + str[1..];
    }

    public static string ToCamelCase<T>(T e) where T : Enum
    {
        return ToCamelCase(e.ToString());
    }
}