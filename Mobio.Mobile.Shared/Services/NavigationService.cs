using OneBuilder.Mobile.Behaviors;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Services
{
	public class NavigationService : INavigationService
	{
		private readonly INavigation navigation;

		public NavigationService()
		{
			this.navigation = Application.Current.MainPage.Navigation;
		}

		public async Task NavigateToAsync<TViewModel>(TViewModel viewModel)
		{
			var page = await this.CreatePage<TViewModel>(viewModel);
			await this.navigation.PushAsync(page);
		}

		public Task NavigateToRootAsync()
		{
			return this.navigation.PopToRootAsync();
		}

		public Task NavigateBackAsync()
		{
			return this.navigation.PopAsync();
		}


		private async Task<Page> CreatePage<TViewModel>(TViewModel viewModel)
		{
			var viewModelType = typeof(TViewModel);
			var viewModelName = viewModelType.FullName;
			var viewName = viewModelName.Replace("ViewModel", "View");
			var viewType = Type.GetType(viewName);
			var view = (Page)Activator.CreateInstance(viewType);
			IdiomVisibleUtlis.RemoveNotValidVisualElements(view);
			NavigationPage.SetHasNavigationBar(view, false);
			view.Padding = new Thickness(0, Device.OnPlatform(U.TabletMode ? 20 : 30, 0, 0), 0, 0);

			if (viewModel is PageViewModel)
			{
				(viewModel as PageViewModel).Page = view;
				await (viewModel as PageViewModel).Init();
			}
			view.BindingContext = viewModel;
			if (viewModel is PageViewModel)
			{
				(viewModel as PageViewModel).Page = view;
				await (viewModel as PageViewModel).InitAfterBinding();
			}
			

			return view;
		}
	}
}
