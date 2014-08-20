using System;
using Microsoft.Practices.Unity;

namespace OpenDeploymentManager.Core.Common.Unity
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterTypePerRequest<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<TFrom, TTo>(new HierarchicalLifetimeManager());
        }

        public static IUnityContainer RegisterTypePerRequest<T>(this IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<T>(new HierarchicalLifetimeManager());
        }

        public static IUnityContainer RegisterTypePerRequest<TFrom, TTo>(this IUnityContainer container, Func<IUnityContainer, object> factoryFunc) where TTo : TFrom
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (factoryFunc == null)
            {
                throw new ArgumentNullException("factoryFunc");
            }

            return container.RegisterType<TFrom, TTo>(new HierarchicalLifetimeManager(), new InjectionFactory(factoryFunc));
        }

        public static IUnityContainer RegisterTypePerRequest<T>(this IUnityContainer container, Func<IUnityContainer, object> factoryFunc)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (factoryFunc == null)
            {
                throw new ArgumentNullException("factoryFunc");
            }

            return container.RegisterType<T>(new HierarchicalLifetimeManager(), new InjectionFactory(factoryFunc));
        }

        public static IUnityContainer RegisterTypeAsSingleton<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterTypeAsSingleton<T>(this IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterTypeAsSingleton<T>(this IUnityContainer container, Func<IUnityContainer, object> factoryFunc)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (factoryFunc == null)
            {
                throw new ArgumentNullException("factoryFunc");
            }

            return container.RegisterType<T>(new ContainerControlledLifetimeManager(), new InjectionFactory(factoryFunc));
        }
    }
}
