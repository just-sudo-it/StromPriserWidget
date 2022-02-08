using SimpleInjector;
using StromPriserWidget.Bootstrapper;
using StromPriserWidget.View;
using Xamarin.Forms;

namespace StromPriserWidget
{
    public partial class App : Application
    {
        public App(Container container)
        {
            InitializeComponent();

            Registrar.BuildContainer(container);

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
