using SimpleInjector;
using System;

namespace StromPriserWidget.DI
{
    public static class SimpleInjectorContainer
    {
        private static readonly Container _container;
        static SimpleInjectorContainer()
            => _container = new Container();

        public static void Register<TInterface, T>() where TInterface : class where T : class, TInterface
            => RegisterTransient<TInterface, T>();
        public static void RegisterTransient<TInterface, T>() where TInterface : class where T : class, TInterface
            => _container.Register<TInterface, T>(Lifestyle.Transient);
        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
            => _container.Register<TInterface, T>(Lifestyle.Singleton);
        public static void RegisterScoped<TInterface, T>() where TInterface : class where T : class, TInterface
            => _container.Register<TInterface, T>(Lifestyle.Scoped);
        
        public static T Resolve<T>() where T : class
            => _container.GetInstance<T>();
        public static object Resolve(Type t)
            => _container.GetInstance(t);

        /*
         *         public static Container Create()
        {
            if (_container == null)
                Initialize();
            return _container;
        }

        private static void Initialize()
        {
            _container = new Container();
        }*/
    }
}
