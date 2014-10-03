using System;

namespace OpenDeploymentManager.Common.Diagnostics
{
    public static class Guard
    {
        public static T ArgumentNotNull<T>(this T value, string parameterName)
            where T : class
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException("parameterName");
            }

            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string ArgumentNotNullOrEmpty(this string value, string parameterName)
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException("parameterName");
            }

            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("The parameter string {0} is empty".Invariant(parameterName), parameterName);
            }

            return value;
        }
    }
}
