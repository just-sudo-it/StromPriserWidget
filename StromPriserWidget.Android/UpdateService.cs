using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using StromPriserWidget.Android;
using Java.Lang;
using System.Net;
using Android.Graphics;
using System.Net.Http;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;

namespace StromPriserWidget.Droid
{
	[Service]
	//	[Service(IsolatedProcess = true)]

	public class UpdateService : Service
	{
		private static readonly HttpClient client = new HttpClient();

		public override IBinder OnBind(Intent intent)
			=> null;

		public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
		{
			Toast.MakeText(this, "Update Service Started", ToastLength.Short)
				 .Show();

			// Build the widget update -Android forces synchronous execution
			RemoteViews updateViews = Task.Run(() => BuildRemoteViews(this)).Result;

			// Push update for this widget to the home screen
			AppWidgetManager.GetInstance(this)
				.UpdateAppWidget(new ComponentName(this, Class.FromType(typeof(AppWidget)).Name), updateViews);

			return StartCommandResult.NotSticky;
		}

		private async Task<RemoteViews> BuildRemoteViews(Context context)
		{
			var prices = await GetData();

			// Build an update that holds the updated widget contents
			var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget);

			updateViews.SetImageViewBitmap(Resource.Id.image_chartview, CreateCharts().ToBitmap());

			//updateViews.SetTextViewText(Resource.Id.blog_title, data.Title);
			//updateViews.SetTextViewText(Resource.Id.creator, data.Creator);

			return updateViews;
		}
		public async Task<string> GetData()
		{
			using var httpRequestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://api.clickatell.com/rest/message"),
				/*Headers =
				{
					{ HttpRequestHeader.Authorization.ToString(), "Bearer xxxxxxxxxxxxxxxxxxx" },
					{ HttpRequestHeader.Accept.ToString(), "application/json" },
					{ "X-Version", "1" }
				}*/
			};

			var response = await client.GetAsync("https://api.clickatell.com/rest/message");
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		public SfChart CreateCharts()
		{
			var chart = new SfChart();
			chart.Title.Text = "Chart";

			//Initializing primary axis
			CategoryAxis primaryAxis = new CategoryAxis();
			primaryAxis.Title.Text = "Name";
			chart.PrimaryAxis = primaryAxis;

			//Initializing secondary Axis
			var secondaryAxis = new NumericalAxis();
			secondaryAxis.Title.Text = "Height (in cm)";
			chart.SecondaryAxis = secondaryAxis;

			//Initializing column series
			var series = new ColumnSeries();
			series.ItemsSource = viewModel.Data;
			series.XBindingPath = "Name";
			series.YBindingPath = "Height";
			series.Label = "Heights";

			series.DataMarker = new ChartDataMarker();
			series.EnableTooltip = true;
			chart.Legend = new ChartLegend();

			chart.Series.Add(series);


			var ft = FragmentManager.BeginTransaction();
			ft.Replace(Resource.Id.fragment_frame_layout, frag, "main");
			ft.Commit();




			return chart;
		}

	}
}