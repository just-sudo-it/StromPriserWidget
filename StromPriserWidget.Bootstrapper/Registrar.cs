using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using SimpleInjector;
using StromPriserWidget.DI;
using StromPriserWidget.View;

namespace StromPriserWidget.Bootstrapper
{
    public class Registrar
    {
        public static void BuildContainer(Container container)
        {
            //container.Register(MainPage);
            //container.Register(MainPage);
            //container.Register(MainPage);

            container.Verify();
        }
    }
    public static class ViewModelLocator
    {
        static ViewModelLocator() { }

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached(
                "AutoWireViewModel",
                typeof(bool),
                typeof(ViewModelLocator),
                default(bool),
                propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
            => (bool)bindable.GetValue(AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
            => bindable.SetValue(AutoWireViewModelProperty, value);

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;

            var viewType = view?.GetType();
            if (viewType?.FullName == null)
            {
                return;
            }

            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            var viewModel = SimpleInjectorContainer.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}

