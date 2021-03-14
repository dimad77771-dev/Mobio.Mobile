using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class UIFunc
	{
		public static void ShowLoading(string title = "Loading")
		{
#if __IOS__
			BigTed.BTProgressHUD.Show(title, -1, BigTed.ProgressHUD.MaskType.Black);
#elif __ANDROID__
			AndroidHUD.AndHUD.Shared.Show(U.Context, title, -1, AndroidHUD.MaskType.Black);
#endif
		}

		public static void HideLoading()
		{
#if __IOS__
			BigTed.BTProgressHUD.Dismiss();
#elif __ANDROID__
			AndroidHUD.AndHUD.Shared.Dismiss();
#endif
		}


		async public static Task AlertInfo(string message)
		{
			await NavFunc.CurrentPage.DisplayAlert("Information", message, "Close");
		}

		async public static Task AlertError(string message)
		{
			UIFunc.HideLoading();
			await NavFunc.CurrentPage.DisplayAlert("Error", message, "Close");
		}

		async public static Task AlertWarning(string message)
		{
			await NavFunc.CurrentPage.DisplayAlert("Warning", message, "Close");
		}

		async public static Task<Boolean> ConfirmAsync(string message)
		{
			var ret = await NavFunc.CurrentPage.DisplayAlert("Warning", message, "Ok", "Cancel");
			return ret;
		}

		async public static Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
		{
			var ret = await NavFunc.CurrentPage.DisplayActionSheet(title, cancel, destruction, buttons);
			return ret;
		}

		public static Page GetParentPage(this VisualElement self)
		{
			while (true)
			{
				var page = self as Page;
				if (page != null)
				{
					return page;
				}

				if (self == null) throw new ArgumentException("");
				self = self.ParentView;
			}
		}

		public static ContentPage GetParentContentPage(this VisualElement self)
		{
			while (true)
			{
				var page = self as ContentPage;
				if (page != null)
				{
					return page;
				}

				if (self == null) throw new ArgumentException("");
				self = self.ParentView;
			}
		}


		public static void ExitApp()
		{
#if __IOS__
			System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
#elif __ANDROID__
			Java.Lang.JavaSystem.Exit(0);
#endif
		}

		
	}
}
