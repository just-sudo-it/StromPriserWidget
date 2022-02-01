using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using StromPriserWidget.DI;

namespace StromPriserWidget.Android
{
    [Activity(Label = "StromPriserWidget", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var containerBuilder = SimpleInjectorContainer.Create();
            //.Register(DroidModule) To add platform specific dependencies through .Register()
            LoadApplication(new App(containerBuilder));
        }
    }
}

/*using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using StromPriserWidget.DI;
using StromPriserWidget;

namespace StromPriserWidget.Droid
{
    [Activity(Label = "StromPriserWidget", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var containerBuilder = SimpleInjectorContainer.Create();
            //.Register(DroidModule) To add platform specific dependencies through .Register()
            LoadApplication(new App(containerBuilder));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}*/