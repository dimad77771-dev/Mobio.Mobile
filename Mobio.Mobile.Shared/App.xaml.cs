using OneBuilder.Mobile.Services;
using OneBuilder.Mobile.ViewModels;
using OneBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			//по каким-то причинам это не работает в XAML (причем только в iOS)
			this.Resources.MergedDictionaries.Add(new Telerik.XamarinForms.Chart.TelerikThemeStyles());
			this.Resources.MergedDictionaries.Add(new Telerik.XamarinForms.DataGrid.TelerikThemeStyles());
			this.Resources.MergedDictionaries.Add(new Telerik.XamarinForms.ConversationalUI.TelerikThemeStyles());

			StylesCustom();
			MainPage = new NavigationPage(new MainPage());

			InitializeDependencies();
			InitializeRootPage(animated:true);
		}

		void InitializeDependencies()
		{
			DependencyService.Register<INavigationService, NavigationService>();
		}

		static public async void InitializeRootPage(bool animated)
		{
			//var vmodel = new UserOrderViewModel { OrderRowId = new Guid("b1cbeb1c-a2a8-4bb4-9d32-177d91bf73ec") }; await NavFunc.NavigateToAsync(vmodel); return;
			//if (U.IsDebug) UserOptions.Reset();
			//var vmodel = new ChangePasswordViewModel(); await NavFunc.NavigateToAsync(vmodel); return;
			//var vmodel = new OrderPatientListViewModel { OrderRowId = new Guid("b1cbeb1c-a2a8-4bb4-9d32-177d91bf73ec") }; await NavFunc.NavigateToAsync(vmodel); return;

			U.RequestMainThread(async () =>
			{
				await Task.Yield();
				//await Task.Delay(2000);

				if (await LoginViewModel.Logging())
				{
					var viewModel = new UserOrderListViewModel();
					await NavFunc.NavigateToAsync(viewModel);
				}
				else
				{
					var viewModel = new LoginViewModel();
					await NavFunc.NavigateToAsync(viewModel);
				}
			});
		}

		void StylesCustom()
		{
			var style = new Style(typeof(ButtonEx));
			style.Setters.Add(new Setter { Property = ButtonEx.FontProperty, Value = FontStyle.F22 });
			style.Setters.Add(new Setter { Property = ButtonEx.TextColorProperty, Value = Color.White });
			style.Setters.Add(new Setter { Property = ButtonEx.TitleShadowColorProperty, Value = Color.FromHex("#147485") });
			//style.Setters.Add(new Setter { Property = ButtonEx.Image2Property, Value = ImageSourcesStatic.BTN_4 });
			Application.Current.Resources.Add("buttonStandart1", style);

			style = new Style(typeof(ButtonEx));
			style.Setters.Add(new Setter { Property = ButtonEx.FontProperty, Value = FontStyle.F22 });
			style.Setters.Add(new Setter { Property = ButtonEx.TextColorProperty, Value = Color.FromHex("#16a0b9") });
			style.Setters.Add(new Setter { Property = ButtonEx.TitleShadowColorProperty, Value = Color.FromHex("#ffffff") });
			//style.Setters.Add(new Setter { Property = ButtonEx.Image2Property, Value = ImageSourcesStatic.BTN_6 });
			Application.Current.Resources.Add("buttonStandart2", style);


			style = new Style(typeof(ButtonEx));
			style.Setters.Add(new Setter { Property = ButtonEx.HeightRequestProperty, Value = 40 });
			style.Setters.Add(new Setter { Property = ButtonEx.WidthRequestProperty, Value = 40 });
			Application.Current.Resources.Add("buttonWithPicture", style);

			style = new Style(typeof(ButtonEx));
			style.Setters.Add(new Setter { Property = ButtonEx.HeightRequestProperty, Value = 20 });
			style.Setters.Add(new Setter { Property = ButtonEx.WidthRequestProperty, Value = 40 });
			Application.Current.Resources.Add("buttonNearSearchBar", style);

			style = new Style(typeof(SearchBarEx));
			style.Setters.Add(new Setter { Property = SearchBar.FontSizeProperty, Value = 18 });
			style.Setters.Add(new Setter { Property = SearchBar.TextColorProperty, Value = Color.Black });
			style.Setters.Add(new Setter { Property = SearchBar.PlaceholderColorProperty, Value = Color.FromHex("#8a8a8a") });
			Application.Current.Resources.Add("searchBarStandart1", style);
		}



		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
