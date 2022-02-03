using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Webkit;
using Android.Widget;
 
namespace StromPriserWidget.Droid
{
	[Service(IsolatedProcess = true)]
	public class UpdateService : Service
	{
		public override IBinder OnBind(Intent intent)
			=> null;

		public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
		{
			Toast.MakeText(this, "Service  Started", ToastLength.Short)
				.Show();

			// Build the widget update for today
			RemoteViews updateViews = buildUpdate(this);
			// Push update for this widget to the home screen
			ComponentName thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(AppWidget)).Name);
			AppWidgetManager manager = AppWidgetManager.GetInstance(this);
			manager.UpdateAppWidget(thisWidget, updateViews); 
			
			return base.OnStartCommand(intent, flags, startId);
		}

		// Build a widget update - Will block until the online API returns.
		private RemoteViews buildUpdate(Context context)
		{
			var entry = BlogPost.GetBlogPost(); // GET THE DATA 

			// Build an update that holds the updated widget contents
			var updateViews = new RemoteViews(context.PackageName, Resource.Layout.Widget);


			var winManager = GetSystemService(Context.WindowService);
			var webView = new WebView(this);
			webView.VerticalScrollBarEnabled(false);
			webView.SetWebViewClient(client);

			var a =
				new WindowManager.LayoutParams(WindowManager.LayoutParams.WRAP_CONTENT,
											   WindowManager.LayoutParams.WRAP_CONTENT,
											   WindowManager.LayoutParams.TYPE_SYSTEM_OVERLAY,
											   WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE,
											   PixelFormat.TRANSLUCENT);
        params.x = 0;
        params.y = 0;
        params.width = 0;
        params.height = 0;

			final FrameLayout frame = new FrameLayout(this);
			frame.addView(webView);
			winManager.addView(frame, params);

			webView.loadUrl("http://stackoverflow.com");

			return START_STICKY;





			updateViews.SetTextViewText(Resource.Id.blog_title, entry.Title);
			updateViews.SetTextViewText(Resource.Id.creator, entry.Creator);

			// When user clicks on widget, launch to Wiktionary definition page
			if (!string.IsNullOrEmpty(entry.Link))
			{
				Intent defineIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(entry.Link));

				PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, defineIntent, 0);
				updateViews.SetOnClickPendingIntent(Resource.Id.Widget, pendingIntent);
			}

			return updateViews;
		}

		////////
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