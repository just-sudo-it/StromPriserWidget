using SimpleInjector;
using System;

namespace StromPriserWidget.DI
{
    public static class SimpleInjectorContainer
    {
        private static Container _container;

        public static Container Create()
        {
            if (_container == null)
                Initialize();
            return _container;
        }

        private static void Initialize()
        {
            _container = new Container();
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
            => _container.RegisterSingleton<TInterface, T>();

        public static T Resolve<T>() where T : class
            => _container.GetInstance<T>();

        public static object Resolve(Type t)
            => _container.GetInstance(t);
    }
}
