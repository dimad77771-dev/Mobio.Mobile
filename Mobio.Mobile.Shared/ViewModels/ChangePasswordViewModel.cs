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
	public class ChangePasswordViewModel : PageViewModel
	{
		public ChangePasswordModel Model { get; set; }

		public Command CommitCommand { get; set; }
		public Command CancelCommand { get; set; }

		public Boolean IsCommit { get; set; }

		public Boolean IsLocaleChooseShow { get; set; } = true;
		public Command LocaleChooseCommand { get; set; }
		public String LocaleChooseText { get; set; } = Globalization.GetOtherLocaleName();



		public override async Task Init()
		{
			HeaderTitle = Globalization.T("ChangePassword");
			IsBackVisible = U.IsBackVisible;
			AllPatientTabs.ForEach(q => PatientHeaderModels.Add(q, new PatientHeaderModel()));

			CommitCommand = CommandFunc.CreateAsync(Commit, () => !HasModelErrors());
			CancelCommand = CommandFunc.CreateAsync(Cancel);
			LocaleChooseCommand = CommandFunc.CreateAsync(async () => await Globalization.SwitchLocale());


			U.RequestMainThread(async () =>
			{
				if (!await LoadData()) return;
				CalcAll();
			});
		}


		async Task<bool> LoadData()
		{
			Model = new ChangePasswordModel
			{ 
				IsNewRow = true,
			};
			if (U.IsDebug)
			{
				//Model.email = "test1@gmail.com"; Model.password = "123";
				//Model.email = "george_001@gmail.com"; Model.password = "$Uper.User10";
			}

			SetupModel(Model);

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

		void SetupModel(ChangePasswordModel model)
		{
			model.PropertyChanged += (s, e) => OnModelChanged(model, e.PropertyName);
		}

		void OnModelChanged(ChangePasswordModel model, string propertyName)
		{
			ValidateGeneral();
		}


		public override async Task OnAppearingEx(ContentPageEx view)
		{
			//view.
			//BusinessCodeFocusTo = true;
		}

		public async Task Commit()
		{
			UIFunc.ShowLoading(U.StandartUpdatingText);
			var result = await WebServiceFunc.UpdatePassword(Model);
			UIFunc.HideLoading();

			if (!result.Item1)
			{
				await UIFunc.AlertError(U.GetErrorUpdateText(result.Item2));
				return;
			}

			await NavFunc.RestartApp();
		}

		public async Task Register()
		{
			var viewModel = new ProfileViewModel();
			await NavFunc.NavigateToAsync(viewModel);
		}


		public override async Task<bool> BeforePageClose()
		{
			if (!IsBackVisible) return false;
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

			if (IsEmptyFieldValue(Model.currentPassword))
			{
				errors.Add(nameof(Model.currentPassword));
			}
			if (IsEmptyFieldValue(Model.newPassword))
			{
				errors.Add(nameof(Model.newPassword));
			}
			if (IsEmptyFieldValue(Model.passwordRepeat))
			{
				errors.Add(nameof(Model.passwordRepeat));
			}


			if (Model.newPassword != Model.passwordRepeat)
			{
				errors.Add(nameof(Model.newPassword));
				errors.Add(nameof(Model.passwordRepeat));
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

		bool IsEmptyFieldValue(object arg) => ValidatorFunc.IsEmptyFieldValue(arg);
		bool IsValidEmail(string arg) => ValidatorFunc.IsValidEmail(arg);

		public Dictionary<string, PatientHeaderModel> PatientHeaderModels { get; set; } = new Dictionary<string, PatientHeaderModel>();
		const string General = nameof(General);
		string[] AllPatientTabs = { General };
		public class PatientHeaderModel : ViewModelBase
		{
			public bool HasError { get; set; }
			public string ErrorText { get; set; }
		}


		public async static Task OpenPage()
		{
			var viewModel = new ChangePasswordViewModel();
			await NavFunc.NavigateToAsync(viewModel);
		}
	}

	public class ChangePasswordViewModel_BorderColorConverter : IValueConverter
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


