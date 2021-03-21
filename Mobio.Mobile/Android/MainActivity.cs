using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using OneBuilder.Sqlite;
//using OneBuilder.Sqlite;

namespace OneBuilder.Mobile.Droid
{
    [Activity(Label = "OneBuilder.Orders", Icon = "@drawable/MainIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		public static MainActivity Instance;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
    
            base.OnCreate(savedInstanceState);
			Instance = this;
			AndroidEnvironment.UnhandledExceptionRaiser += MyApp_UnhandledExceptionHandler;
			DBInit.Init();

			LabelHtml.Forms.Plugin.Droid.HtmlLabelRenderer.Initialize();
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
			FFImageLoading.Forms.Platform.CachedImageRenderer.InitImageViewHandler();
			var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);
			AiForms.Effects.Droid.Effects.Init();
			ZXing.Net.Mobile.Forms.Android.Platform.Init();
			LoadApplication(new App());
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}


		static void MyApp_UnhandledExceptionHandler(object sender, RaiseThrowableEventArgs e)
		{
			UnhandledExceptionProccesing.LogUnhandledException(e.Exception);
		}

	}
}