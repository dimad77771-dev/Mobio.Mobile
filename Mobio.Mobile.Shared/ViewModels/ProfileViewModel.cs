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
	public class ProfileViewModel : PageViewModel
	{
		public Guid OrderRowId { get; set; }
		public Order Order { get; set; }

		public State[] DdlStates { get; set; }
		public String[] DdlGenders { get; set; } = new[] { "Male", "Female", "Other" };

		public UserProfile Model { get; set; }

		public Command CommitCommand { get; set; }
		public Command CancelCommand { get; set; }

		public Boolean IsCommit { get; set; }

		public override async Task Init()
		{
			OrderRowId = new Guid("4CB06476-B10C-406A-98A2-7A6693A4E590");

			HeaderTitle = "Profile";
			IsBackVisible = false;
			AllPatientTabs.ForEach(q => PatientHeaderModels.Add(q, new PatientHeaderModel()));

			CommitCommand = CommandFunc.CreateAsync(Commit, () => !HasModelErrors());
			CancelCommand = CommandFunc.CreateAsync(Cancel);

			U.RequestMainThread(async () =>
			{
				if (!await LoadData()) return;
				CalcAll();
			});
		}


		async Task<bool> LoadData()
		{
			UIFunc.ShowLoading();

			var task1 = WebServiceFunc.GetOrder(OrderRowId);
			var task3 = WebServiceFunc.GetStates(1);
			await Task.WhenAll(task1, task3);
			if (task1.Result == null || task3.Result == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return false;
			}

			Order = task1.Result;
			DdlStates = task3.Result.OrderBy(q => q.Name).ToArray();

			Model = Order.UserProfile;

			UIFunc.HideLoading();
			return true;
		}


	
		void CalcAll()
		{
			ValidateAll();
			CommandEnabledRefresh();
		}

		void CommandEnabledRefresh()
		{
			this.ChangeAllCanExecute();
		}


		public override async Task OnAppearingEx(ContentPageEx view)
		{
			//view.
			//BusinessCodeFocusTo = true;
		}

		public async Task Commit()
		{
			UIFunc.ShowLoading(U.StandartUpdatingText);
			var result = await WebServiceFunc.SubmitRegister(Order);
			UIFunc.HideLoading();

			if (!result)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
			}
			
			////var homeViewModel = new HomeViewModel();
			//var homeViewModel = new SerialViewModel();
			//await NavFunc.NavigateToAsync(homeViewModel);
		}


		public override async Task<bool> BeforePageClose()
		{
			//UIFunc.ExitApp();
			//return false;
			return true;
		}

		public async Task Cancel()
		{
			
		}

		void ValidateAll()
		{
			ValidateGeneral(totalCalc: false);
			ValidateTotalCalc();
		}

		public ObservableCollection<string> ErrorsGeneral { get; set; } = new ObservableCollection<string>();
		void ValidateGeneral(bool totalCalc = true)
		{
			var errors = new List<string>();

			if (IsEmptyFieldValue(Model.FirstName))
			{
				errors.Add(nameof(Model.FirstName));
			}
			if (IsEmptyFieldValue(Model.LastName))
			{
				errors.Add(nameof(Model.LastName));
			}
			if (IsEmptyFieldValue(Model.AddressLine1))
			{
				errors.Add(nameof(Model.AddressLine1));
			}
			if (IsEmptyFieldValue(Model.City))
			{
				errors.Add(nameof(Model.City));
			}
			if (IsEmptyFieldValue(Model.ProvinceOrStateRowId))
			{
				errors.Add(nameof(Model.ProvinceOrStateRowId));
			}
			if (IsEmptyFieldValue(Model.Postcode))
			{
				errors.Add(nameof(Model.Postcode));
			}
			if (IsEmptyFieldValue(Model.Phone))
			{
				errors.Add(nameof(Model.Phone));
			}
			if (IsEmptyFieldValue(Model.Email))
			{
				errors.Add(nameof(Model.Email));
			}
			if (IsEmptyFieldValue(Model.Password))
			{
				errors.Add(nameof(Model.Password));
			}
			if (IsEmptyFieldValue(Model.PasswordRepeat))
			{
				errors.Add(nameof(Model.PasswordRepeat));
			}

			ErrorsGeneral = errors.ToObservableCollection();

			PatientHeaderModels[General].HasError = errors.Any();
			if (totalCalc) ValidateTotalCalc();
		}


		void ValidateTotalCalc()
		{
			CommandEnabledRefresh();
		}

		bool HasModelErrors()
		{
			var haserror = PatientHeaderModels.Any(q => q.Value.HasError);
			return haserror;
		}

		bool IsEmptyFieldValue(object arg)
		{
			if (arg == null) return true;
			return string.IsNullOrEmpty(arg.ToString().Trim());
		}

		public Dictionary<string, PatientHeaderModel> PatientHeaderModels { get; set; } = new Dictionary<string, PatientHeaderModel>();
		const string General = nameof(General);
		string[] AllPatientTabs = { General };
		public class PatientHeaderModel : ViewModelBase
		{
			public bool HasError { get; set; }
			public string ErrorText { get; set; }
		}
	}

	public class ProfileViewModel_BorderColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var field = (parameter == null ? "" : parameter.ToString());
			var errors = value as ObservableCollection<string>;
			var hasError = errors.Contains(field);
			return hasError ? Color.FromHex("#A94442") : Color.FromHex("#4488F6");
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

}


