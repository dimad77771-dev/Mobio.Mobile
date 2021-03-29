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
	public class LoginViewModel : PageViewModel
	{
		public Guid UserProfileRowId { get; set; }
		public State[] DdlStates { get; set; }

		public LoginModel Model { get; set; }

		public Command CommitCommand { get; set; }
		public Command RegisterCommand { get; set; }
		public Command CancelCommand { get; set; }

		public Boolean IsCommit { get; set; }

		public Boolean IsLocaleChooseShow { get; set; } = true;
		public Command LocaleChooseCommand { get; set; }
		public String LocaleChooseText { get; set; } = Globalization.GetOtherLocaleName();

		public Boolean IsNewRow { get; set; }


		public override async Task Init()
		{
			HeaderTitle = Globalization.T("Login");
			IsBackVisible = U.IsBackVisible;
			AllPatientTabs.ForEach(q => PatientHeaderModels.Add(q, new PatientHeaderModel()));

			CommitCommand = CommandFunc.CreateAsync(Commit, () => !HasModelErrors());
			RegisterCommand = CommandFunc.CreateAsync(Register);
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
			UserProfileRowId = UserOptions.GetUserProfileRowId();
			IsNewRow = (UserProfileRowId == default(Guid));

			Model = new LoginModel
			{ 
				IsNewRow = true,
			};
			if (U.IsDebug)
			{
				Model.email = "test1@gmail.com"; Model.password = "123456";
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

		void SetupModel(LoginModel model)
		{
			model.PropertyChanged += (s, e) => OnModelChanged(model, e.PropertyName);
		}

		void OnModelChanged(LoginModel model, string propertyName)
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
			var result = await WebServiceFunc.SubmitLogin(Model);
			UIFunc.HideLoading();

			if (!result.Item1)
			{
				var errtext = (string.IsNullOrEmpty(result.Item3) ? Globalization.T("(!)LoginError") : result.Item3);
				await UIFunc.AlertError(errtext);
				return;
			}

			var userProfileRowId = result.Item2.Value;
			var aspxauth = result.Item4;
			UserOptions.SetUsernamePassword(Model.email, Model.password, userProfileRowId);
			UserOptions.SetAspxauth(aspxauth);

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

			if (IsEmptyFieldValue(Model.email))
			{
				errors.Add(nameof(Model.email));
			}
			if (!IsValidEmail(Model.email))
			{
				errors.Add(nameof(Model.email));
			}

			if (IsEmptyFieldValue(Model.password))
			{
				errors.Add(nameof(Model.password));
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
			var viewModel = new LoginViewModel();
			await NavFunc.NavigateToAsync(viewModel);
		}
	}

	public class LoginViewModel_BorderColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var field = (parameter == null ? "" : parameter.ToString());
			var errors = value as ObservableCollection<string>;
			var hasError = errors.Contains(field);
			return U.GetEntryBorderColor(hasError);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

}


