using OneBuilder.Model;
using OneBuilder.WebServices;
using PropertyChanged;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneBuilder.Mobile.ViewModels
{
	public class HomeViewModel : PageViewModel
	{
		public ICommand LogoutCommand => CommandFunc.CreateAsync(Logout);
		//public ObservableCollection<LabourDTO> EmployeeItems { get; set; } = new ObservableCollection<LabourDTO>();
		//public LabourDTO EmployeeSelectedItem { get; set; }

		public Boolean StartAnimation { get; set; }
		public Boolean StopAnimation { get; set; }
		public Boolean StartAnimation2 { get; set; }
		public Boolean StopAnimation2 { get; set; }


		public HomeViewModel()
		{
			HeaderTitle = Globalization.T("Menu");
			IsBackVisible = false;
			//this.PropertyChanged += DoPropertyChanged;
		}


		public override async Task Init()
		{
			NavFunc.RemovePages<Views.LoginView>();
			//await LoadLabours();
		}


		bool CanEnterSerialNumberCommand()
		{
			//return (EmployeeSelectedItem != null);
			return true;
		}


		bool isLogoutClose = false;
		async Task Logout()
		{
			if (!await UIFunc.ConfirmAsync("Logout?"))
			{
				return;
			}

			isLogoutClose = true;
			var ret = await NavFunc.PopToRootAsync();
			isLogoutClose = false;

			if (!ret)
			{
				return;
			}

			UserOptions.Clear();
			var model = new LoginViewModel();
			await NavFunc.NavigateToAsync(model);
		}

		public override async Task<bool> BeforePageClose()
		{
			if (isLogoutClose)
			{
				return true;
			}

			if (!await UIFunc.ConfirmAsync("Exit from application?"))
			{
				return false;
			}

			UIFunc.ExitApp();
			return false;
		}
	}
}
