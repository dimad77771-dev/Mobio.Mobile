using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Linq.Expressions;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using OneBuilder.Mobile.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class MessageFunc
	{
	}

	public static class MessageCloseViewExtensions
	{
		public static void SubscribeOnClosed<T,R>(this T viewmodel, R closevewmodel, Func<Task> callback) where T: PageViewModel where R : PageViewModel
		{
			MessagingCenter.Subscribe<Application, MessagingType.ClosePageArg>(viewmodel, MessagingType.ClosePage, async (s,arg) =>
			{
				if (arg.ClosedViewModel == closevewmodel)
				{
					await callback();
				}
			});
		}

		public static void SubscribeOnClosed<T, R>(this T viewmodel, R closevewmodel, Action callback) where T : PageViewModel where R : PageViewModel
		{
			MessagingCenter.Subscribe<Application, MessagingType.ClosePageArg>(viewmodel, MessagingType.ClosePage, (s, arg) =>
			{
				if (arg.ClosedViewModel == closevewmodel)
				{
					callback();
				}
			});
		}


		public static void UnsubscribeOnClosed<T, R>(this T viewmodel) where T : PageViewModel
		{
			MessagingCenter.Unsubscribe<Application, MessagingType.ClosePageArg>(viewmodel, MessagingType.ClosePage);
		}
	}



	public class MessagingType
	{
		public const string ClosePage = "ClosePage";

		public class ClosePageArg
		{
			public PageViewModel ClosedViewModel { get; set; }
		}
	}

}