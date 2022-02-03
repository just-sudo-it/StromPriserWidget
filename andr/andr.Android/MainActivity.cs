using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using StromPriserWidget;
using StromPriserWidget.DI;
using Android.Webkit;
using Android.Views;

namespace StromPriserWidget.Droid
{
    [Activity(Label = "andr", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            WebView webView = FindViewById<WebView>(Resource.Id.LocalWebView);
            webView.SetWebViewClient(new WebViewClient());
            webView.LoadUrl("http://www.xamarin.com");
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.BuiltInZoomControls = true;
            webView.Settings.SetSupportZoom(true);
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            webView.ScrollbarFadingEnabled = false;

            string HTMLText = "<html>" + "<body>Sample <b>HTML</b> Text<br/><br/>" +
                              "<span style='color:red;'>Application created by: Anoop Kumar Sharma<span></body>" +
                              "</html>";
            //Load HTML Data in WebView
            webView.LoadData(HTMLText, "text/html", null);
            webView.EvaluateJavascript();

            var container = SimpleInjectorContainer.Create(); //Register platform specifics
            LoadApplication(new App(container));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        /*        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }
        */
    }
}