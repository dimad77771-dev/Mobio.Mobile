#if __IOS__
	using Foundation;
#elif __ANDROID__
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class SysFunc
	{
		public static void RequestMainThread(Action action)
		{
#if __IOS__
			NSRunLoop.Main.BeginInvokeOnMainThread(new Action(action.Invoke));
#elif __ANDROID__
			U.Context.RunOnUiThread(action);
#endif
		}


		public static void InvokeMainThread(Action action)
		{
#if __IOS__
			NSRunLoop.Main.InvokeOnMainThread(new Action(action.Invoke));
#elif __ANDROID__
			Android.App.Application.SynchronizationContext.Send((o) => action(), null);
#endif
		}

		//public static NSTimer StartTimer(TimeSpan interval, Func<bool> callback)
		//{
		//	NSTimer nSTimer = NSTimer.CreateRepeatingScheduledTimer(interval, delegate (NSTimer t)
		//	{
		//              RequestMainThread(() =>
		//              {
		//                  if (!callback.Invoke())
		//                  {
		//                      t.Invalidate();
		//                  }
		//              });
		//	});
		//	NSRunLoop.Current.AddTimer(nSTimer, NSRunLoopMode.Common);
		//          return nSTimer;
		//      }
	}
}
