using OneBuilder.Model;
using OneBuilder.WebServices;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.Views;
using System.Windows.Input;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using T = Telerik.XamarinForms.Input;
using OneBuilder.Mobile.Behaviors;
using System.Diagnostics;

namespace OneBuilder.Mobile.ViewModels
{
	public class UserOrderListViewModel : PageViewModel
	{
		public Guid UserProfileRowId { get; set; }

		public ObservableCollection<Order> Items { get; set; }
		public Order SelectedItem { get; set; }
		public Order SelectedItemScrollToRow { get; set; }

		public Command ItemTapCommand { get; set; }

		public bool NavigationBarButton1IsVisible { get; set; } = true;
		public String NavigationBarButton1Text { get; set; } = Globalization.T("Profile");
		public Command NavigationBarButton1Command { get; set; }

		public override async Task Init()
		{
			//UserProfileRowId = new Guid("881C381C-2EEF-420B-9398-39B5190E9CEC");
			UserProfileRowId = UserOptions.GetUserProfileRowId();

			HeaderTitle = Globalization.T("Orders");
			IsBackVisible = U.IsBackVisible;

			ItemTapCommand = new Command<ItemTapCommandContext>(ItemTap);
			NavigationBarButton1Command = CommandFunc.CreateAsync(ProfileViewModel.OpenPage);

			U.RequestMainThread(async () =>
			{
				if (!await LoadData()) return;
				CalcAll();
			});
		}


		async Task<bool> LoadData()
		{
			UIFunc.ShowLoading();

			var items = await LoadOrders();
			if (items == null) return false;

			if (!items.Any())
			{
				if (!await InsertEmptyOrder()) return false;

				items = await LoadOrders();
				if (items == null) return false;
			}

			Items = items.ToObservableCollection();

			UIFunc.HideLoading();
			return true;
		}

		async Task<Order[]> LoadOrders()
		{
			var task1 = WebServiceFunc.GetUserOrders(UserProfileRowId);
			await Task.WhenAll(task1);
			if (task1.Result == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return null;
			}

			var items = task1.Result;
			return items;
		}

		async Task<bool> InsertEmptyOrder()
		{
			var userProfile = await WebServiceFunc.GetProfile(UserProfileRowId);
			if (userProfile == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return false;
			}
			userProfile.Password = UserOptions.GetPassword();

			var order = new Order
			{
				UserProfileRowId = UserProfileRowId,
				IsNew = true,
				UserProfile = userProfile,
				Pois = new List<PatientOrderItem>(),
			};
			var result = await WebServiceFunc.SubmitRegister(order);
			if (!result)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return false;
			}

			return true;
		}


		async void ItemTap(ItemTapCommandContext context)
		{
			var item = (Order)context.Item;
			SetSelectedPatientOrderItem(item);

			var viewModel = new UserOrderViewModel
			{
				OrderRowId = item.RowId,
			};
			await NavFunc.NavigateToAsync(viewModel);
		}


		void SetSelectedPatientOrderItem(Order item)
		{
			SelectedItem = item;
			CalcAll();
		}


		void CalcAll()
		{
			CommandEnabledRefresh();
		}

		void CommandEnabledRefresh()
		{
			this.ChangeAllCanExecute();
		}



		public override async Task OnAppearingEx(ContentPageEx view)
		{
		}

		public override async Task<bool> BeforePageClose()
		{
			if (!IsBackVisible) return false;
			return true;
		}

	}
}


