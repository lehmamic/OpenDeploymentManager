using System;
using System.Linq;
using System.Reflection;
using OpenDeploymentManager.Common.Annotations;

namespace OpenDeploymentManager.Common.Reflection
{
    public static class MemberInfoExtensions
    {
        public static bool IsDefined<TAttribute>([NotNull] this MemberInfo member, bool inherit) where TAttribute : Attribute
        {
            return member.IsDefined(typeof(TAttribute), inherit);
        }

        public static TAttribute GetCustomAttribute<TAttribute>([NotNull] this MemberInfo member, bool inherit) where TAttribute : Attribute
        {
            TAttribute[] customAttributes = member.GetCustomAttributes<TAttribute>(inherit);
            if (customAttributes.Length != 1)
            {
                throw new InvalidOperationException("The member {0} has not exactly one attribute of type {1}".Invariant(member, typeof(TAttribute)));
            }

            return customAttributes.Single();
        }

        public static TAttribute[] GetCustomAttributes<TAttribute>([NotNull] this MemberInfo member, bool inherit) where TAttribute : Attribute
        {
            return member.GetCustomAttributes(typeof(TAttribute), inherit).OfType<TAttribute>().ToArray();
        }
    }
}
