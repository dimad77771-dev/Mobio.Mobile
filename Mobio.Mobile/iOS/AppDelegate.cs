using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using ObjCRuntime;
using OneBuilder.Mobile;
using OneBuilder.Sqlite;
using UIKit;

namespace Mobio.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public static AppDelegate Current;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			Current = this;
			DBInit.Init();
			LabelHtml.Forms.Plugin.iOS.HtmlLabelRenderer.Initialize();
			global::Xamarin.Forms.Forms.Init();
			FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
			FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
			var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);
			ZXing.Net.Mobile.Forms.iOS.Platform.Init();
			FormsControls.Touch.Renderers.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, [Transient] UIWindow forWindow)
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
			{
				return UIInterfaceOrientationMask.Landscape;
			}
			else
			{
				return UIInterfaceOrientationMask.Portrait;
			}
		}
	}
}
