using System.Globalization;

namespace OpenDeploymentManager.Common
{
    public static class StringExtensions
    {
        public static bool ToBool(this string value, bool defaultValue)
        {
            bool result = false;
            if (!bool.TryParse(value, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static int ToInt(this string value, int defaultValue)
        {
            int result = 0;
            if (!int.TryParse(value, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static string Invariant(this string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }
    }
}
