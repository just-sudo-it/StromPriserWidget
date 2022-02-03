using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using System;

namespace StromPriserWidget.Droid
{
    [BroadcastReceiver(Label = "HellApp Widget")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
    public class AppWidget : AppWidgetProvider
    {
        private const string AnnouncementClick = "AnnouncementClickTag";

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            // Check if the click is from the "Announcement" button
            if (AnnouncementClick.Equals(intent.Action))
            {
                // Open another app
            }
        }
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(AppWidget)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));
        }
        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            var widgetView = new RemoteViews(context.PackageName, Resource.Layout.Widget);

            SetTextViewText(widgetView);
            RegisterClicks(context, appWidgetIds, widgetView);

            return widgetView;
        }
        private void SetTextViewText(RemoteViews widgetView)
        {
            widgetView.SetTextViewText(Resource.Id.widgetMedium, "HelloAppWidget");
            widgetView.SetTextViewText(Resource.Id.widgetSmall,
                string.Format("Last update: {0:H:mm:ss}", DateTime.Now));
        }

        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            widgetView.SetOnClickPendingIntent(Resource.Id.widgetAnnouncementIcon,
                GetPendingSelfIntent(context, AnnouncementClick));
        }

        private PendingIntent GetPendingSelfIntent(Context context, string action)
        {
            var intent = new Intent(context, typeof(AppWidget));
            intent.SetAction(action);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }
    }
}