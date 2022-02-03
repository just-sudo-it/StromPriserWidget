using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
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

		// Build a widget update to show the current Wiktionary
		// "Word of the day." Will block until the online API returns.
		public RemoteViews buildUpdate(Context context)
		{
			var entry = BlogPost.GetBlogPost(); // GET THE DATA 

			// Build an update that holds the updated widget contents
			var updateViews = new RemoteViews(context.PackageName, Resource.Layout.Widget);

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
	}
}