using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace OpenDeploymentManager.Common.Unity
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterTypePerRequest<TFrom, TTo>(this IUnityContainer container, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<TFrom, TTo>(new HierarchicalLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterTypePerRequest<T>(this IUnityContainer container, params InjectionMember[] injectionMembers)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<T>(new HierarchicalLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterTypePerRequest<TFrom, TTo>(this IUnityContainer container, Func<IUnityContainer, object> factoryFunc, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (factoryFunc == null)
            {
                throw new ArgumentNullException("factoryFunc");
            }
            
            var extendedInjectionMembers = injectionMembers.ToList();
            extendedInjectionMembers.Insert(0, new InjectionFactory(factoryFunc));

            return container.RegisterType<TFrom, TTo>(new HierarchicalLifetimeManager(), extendedInjectionMembers.ToArray());
        }

        public static IUnityContainer RegisterTypePerRequest<T>(this IUnityContainer container, Func<IUnityContainer, object> factoryFunc, params InjectionMember[] injectionMembers)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (factoryFunc == null)
            {
                throw new ArgumentNullException("factoryFunc");
            }

            var extendedInjectionMembers = injectionMembers.ToList();
            extendedInjectionMembers.Insert(0, new InjectionFactory(factoryFunc));

            return container.RegisterType<T>(new HierarchicalLifetimeManager(), extendedInjectionMembers.ToArray());
        }

        public static IUnityContainer RegisterTypeAsSingleton<TFrom, TTo>(this IUnityContainer container, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterTypeAsSingleton<T>(this IUnityContainer container, params InjectionMember[] injectionMembers)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<T>(new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterTypeAsSingleton<T>(this IUnityContainer container, Func<IUnityContainer, object> factoryFunc, params InjectionMember[] injectionMembers)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (factoryFunc == null)
            {
                throw new ArgumentNullException("factoryFunc");
            }

            var extendedInjectionMembers = injectionMembers.ToList();
            extendedInjectionMembers.Insert(0, new InjectionFactory(factoryFunc));

            return container.RegisterType<T>(new ContainerControlledLifetimeManager(), extendedInjectionMembers.ToArray());
        }
    }
}
