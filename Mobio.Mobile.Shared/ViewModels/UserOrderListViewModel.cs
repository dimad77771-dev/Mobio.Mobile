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

		public override async Task Init()
		{
			UserProfileRowId = new Guid("2fd3c1cb-be1a-4444-8131-c44447d3b6bc");

			HeaderTitle = Globalization.T("Orders");
			IsBackVisible = U.IsBackVisible;

			ItemTapCommand = new Command<ItemTapCommandContext>(ItemTap);

			U.RequestMainThread(async () =>
			{
				if (!await LoadData()) return;
				CalcAll();
			});
		}


		async Task<bool> LoadData()
		{
			UIFunc.ShowLoading();

			var task1 = WebServiceFunc.GetUserOrders(UserProfileRowId);
			await Task.WhenAll(task1);
			if (task1.Result == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return false;
			}

			//var gg = task1.Result;
			//Items = new ObservableCollection<Order>();
			//for (int k = 0; k < 100; k++)
			//{
			//	Items.Add(gg[0]);
			//}

			Items = task1.Result.ToObservableCollection();


			UIFunc.HideLoading();
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


