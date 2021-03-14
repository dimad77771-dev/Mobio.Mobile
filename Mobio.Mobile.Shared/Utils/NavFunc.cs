using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class NavFunc
	{
		public static bool UseAmimation = U.IsDebug ? false : true;
		//public static bool UseAmimation = true;

		public static async Task NavigateToAsync<TViewModel>(TViewModel viewModel)
		{
			var navigationService = DependencyService.Get<INavigationService>();
			await navigationService.NavigateToAsync(viewModel);

			var afterPageOpen = CurrentPage as IAfterPageOpen;
			if (afterPageOpen != null)
			{
				await afterPageOpen.AfterPageOpen();
			}
		}

		public static async Task<bool> NavigateBackAsync()
		{
			//var navigationService = DependencyService.Get<INavigationService>();
			//await navigationService.NavigateBackAsync();
			return await Pop();
		}


		public static async Task Push(Page page)
		{
			await MainPage.Navigation.PushAsync(page, UseAmimation);

			var afterPageOpen = page as IAfterPageOpen;
			if (afterPageOpen != null)
			{
				await afterPageOpen.AfterPageOpen();
			}
		}

		public static async Task<bool> Pop(bool forceClose = false, bool? animated = null)
		{
			if (animated == null)
			{
				animated = UseAmimation;
			}

			var beforePageClose = CurrentPage as IBeforePageClose;
			if (beforePageClose != null && !forceClose)
			{
				if (!await beforePageClose.BeforePageClose())
				{
					return false;
				}
			}

			var viewmodel = CurrentPage.BindingContext as PageViewModel;
			if (viewmodel != null && !forceClose)
			{
				if (!await viewmodel.BeforePageClose())
				{
					return false;
				}
			}

			//await MessagingCenters.CloseView.Send(new MessageCloseView(viewmodel));
			MessagingCenter.Send(App.Current, MessagingType.ClosePage, new MessagingType.ClosePageArg { ClosedViewModel = viewmodel });
			await MainPage.Navigation.PopAsync((bool)animated);
			return true;
		}

		public static async Task<bool> PopToRootAsync(bool animated = false)
		{
			var allpages = MainPage.Navigation.NavigationStack.Reverse().ToArray();

			foreach(var page in allpages)
			{
				var isclose = await Pop(animated: animated);
				if (!isclose)
				{
					return false;
				}
			}

			return true;
		}

		public static void RemovePages<T>() where T : Page
		{
			var removepages = MainPage.Navigation.NavigationStack.OfType<T>().ToArray();
			foreach (var removepage in removepages)
			{
				MainPage.Navigation.RemovePage(removepage);
			}
		}

		public static Page MainPage
		{
			get
			{
				var mainPage = App.Current.MainPage;
				return mainPage;
			}
		}

		public static Page CurrentPage
		{
			get
			{
				var navigationStack = GetNavigationStack();
				return navigationStack[navigationStack.Count - 1];
			}
		}

		public static List<Page> GetNavigationStack()
		{
			var list = new List<Page>();
			list.Add(MainPage);

			list.AddRange(MainPage.Navigation.NavigationStack);
			return list;
		}
	}


	public interface IAfterPageOpen
	{
		Task AfterPageOpen();
	}

	public interface IBeforePageClose
	{
		Task<Boolean> BeforePageClose();
	}

	public interface IAfterPageClose
	{
		Task<Boolean> AfterPageClose();
	}

}
