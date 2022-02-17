using Android.App;
using Android.Appwidget;
using Android.Content;

namespace StromPriserWidget.Droid
{
    [BroadcastReceiver(Label = "StromPriserWidget")]
    [IntentFilter(new [] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
    public class AppWidget : AppWidgetProvider
    {
        private const string AnnouncementClick = "AnnouncementClickTag";

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            if (AnnouncementClick.Equals(intent.Action))
            {
                // Open another app
            }
        }
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
            => context.StartService(new Intent(context, typeof(UpdateService)));
    }
}