#if __IOS__
	using CoreGraphics;
	using Foundation;
	using UIKit;
#elif __ANDROID__
	using Android.App;
	using OneBuilder.Mobile.Droid;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OneBuilder.Model;


namespace OneBuilder.Mobile
{
	public static class U
	{
#if __IOS__
		public static Object Context
		{
			get
			{
				return new Object();
			}
		}
#elif __ANDROID__
		public static Activity Context
		{
			get
			{
				return MainActivity.Instance as Activity;
			}
		}
#endif

#if DEBUG
		public static bool IsDebug => true;
#else
		public static bool IsDebug => false;
#endif


		public static void Log(object arg)
		{
			Console.WriteLine("" + arg);
		}
		public static void Log2(object arg)
		{
			//Console.WriteLine("" + arg);
		}
		public static void Log3(object arg)
		{
			//Console.WriteLine("" + arg);
			//Console.Out.Flush();
		}
		public static void Log4(object arg)
		{
			Console.WriteLine("" + arg);
			Console.Out.Flush();
		}

		//public static Task Dp(object arg)
		//{
		//	var tcs = new TaskCompletionSource<Boolean>();

		//	UIAlertView alert = new UIAlertView(null, "" + arg, null, "Close");
		//	alert.Show();
		//	alert.Clicked += (s, e) =>
		//	{
		//		tcs.SetResult(true);
		//	};

		//	return tcs.Task;
		//}

		//public static void Dp2(object arg)
		//{
		//	nint buttonClicked = -1;
		//	UIAlertView alert = new UIAlertView(null, "" + arg, null, "Close");
		//	alert.Show();

		//	alert.Clicked += (sender, buttonArgs) => { buttonClicked = buttonArgs.ButtonIndex; };

		//	while (buttonClicked == -1)
		//	{
		//		NSRunLoop.Current.RunUntil(NSDate.FromTimeIntervalSinceNow(0.5));
		//	}
		//}

		public static void TimerLog(object msg, IEnumerable<DateTime> vals)
		{
			TimerLog(msg, vals.ToArray());
		}

		public static void TimerLog(object msg, params DateTime[] vals)
		{
			string str = msg.ToString() + "=";
			for(int i = 0; i < vals.Length - 1; i++)
			{
				str += (int)((vals[i + 1] - vals[i]).TotalMilliseconds) + ";";
			}

			U.Log4(str);
		}

		public static DateTime Now
		{
			get
			{
				return DateTime.Now;
			}
		}
	
		public static DateTime Today
		{
			get
			{
				var b1 = DateTime.Today;
				return new DateTime(b1.Year, b1.Month, b1.Day, 0, 0, 0, DateTimeKind.Utc);
			}
		}

		public static void Logout()
		{
			UserOptions.SetUsernamePassword("", "", default(Guid));
		}


		public static Boolean TabletMode => (Device.Idiom == TargetIdiom.Tablet);

		public static Guid UserRowId => UserOptions.GetCurrent().UserRowId;
		public static Guid BusinessRowId => UserOptions.GetCurrent().BusinessRowId;
		//public static Guid LabourRowId =>  UserOptions.GetCurrent().CurrentLabourRowId;
		//public static String LabourName => UserOptions.GetCurrent().CurrentLabourName;

		public static String StandartErrorUpdateText => Globalization.T("(!)Error during update. Try later");
		public static String StandartErrorRetrieveText => Globalization.T("(!)Error during retrieve data. Try later");
		public static String StandartUpdatingText => Globalization.T("(!)Submitting...");
		public static String StandartLoadingText => Globalization.T("(!)Loading...");
		public static String InternalError => Globalization.T("(!)Internal Error. Try later");

		public static Boolean IsBackVisible => !NavFunc.IsFirstPage;


		public static double DeviceVal(double tabletVal, double phoneVal)
		{
			return (U.TabletMode ? tabletVal : phoneVal);
		}

		public static int GetApkVersion()
		{
#if __IOS__
			var str = (NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"]).ToString();
			str = str.Replace(".", "");
			while (str.Length < 7)
			{
				str += "0";
			}
			var versnum = Convert.ToInt32(str);
			return versnum;
#elif __ANDROID__
			var pm = Context.PackageManager;
			var pname = Context.PackageName;
			var versionCode = pm.GetPackageInfo(pname, 0).VersionCode;

			return versionCode;
#endif
		}

		public static string GetDeviceName()
		{
#if __IOS__
			//var devicename = (UIDevice.CurrentDevice.Model + " " + UIDevice.CurrentDevice.Name).Trim();
			var devicename = (UIDevice.CurrentDevice.Name).Trim();
			return devicename;
#elif __ANDROID__
			var devicename = (Android.OS.Build.Manufacturer + " " + Android.OS.Build.Model).Trim();
			return devicename;
#endif
		}

		public static string GetLocalFilesDir()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		}

		public static void RequestMainThread(Action action)
		{
			SysFunc.RequestMainThread(action);
		}

		//public static void RequestMainThread(Func<Task> action)
		//{
		//	SysFunc.RequestMainThread(async () => await action());
		//}


		//public static void HideKeyboard()
		//{
		//	NavFunc.CurrnetView.EndEditing(true);
		//}


		public static double Pixel2Point(double px)
		{
#if __IOS__
			return px * 0.5;

#elif __ANDROID__
			var resources = U.Context.Resources;
			var metrics = resources.DisplayMetrics;
			var dp = px / metrics.Density;
			return dp;
#endif
		}

		public static T OnPlatform<T>(T ios, T android)
		{
			return (Device.OS == TargetPlatform.iOS ? ios : android);
		}
	}
}
