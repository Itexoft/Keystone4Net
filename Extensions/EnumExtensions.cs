using Keystone4Net.Enums;

namespace Keystone4Net.Extensions
{
    public static class EnumExtensions
    {
        public static string ToJs(this KeystoneFieldType type)
        {
            var name = type.ToString();
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        public static string ToJs(this KeystoneDisplayMode mode)
        {
            var name = mode.ToString();
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}
