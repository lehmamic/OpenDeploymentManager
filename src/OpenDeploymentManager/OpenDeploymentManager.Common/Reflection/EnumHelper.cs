using System;
using System.Linq;

namespace OpenDeploymentManager.Common.Reflection
{
    public static class EnumHelper
    {
        public static TEnum[] GetValues<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToArray();
        }
    }
}
