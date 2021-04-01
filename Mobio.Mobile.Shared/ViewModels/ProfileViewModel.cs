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
		public Guid UserProfileRowId { get; set; }
		public State[] DdlStates { get; set; }

		public UserProfile Model { get; set; }

		public Command CommitCommand { get; set; }
		public Command LogoutCommand { get; set; }
		public Command ChangePasswordCommand { get; set; }
		public Command CancelCommand { get; set; }

		public Boolean IsCommit { get; set; }

		public Boolean IsLocaleChooseShow { get; set; } = true;
		public Command LocaleChooseCommand { get; set; }
		public String LocaleChooseText { get; set; } = Globalization.GetOtherLocaleName();

		public Boolean IsNewRow { get; set; }


		public override async Task Init()
		{
			//UserProfileRowId = new Guid("2fd3c1cb-be1a-4444-8131-c44447d3b6bc");

			HeaderTitle = Globalization.T("Profile");
			IsBackVisible = U.IsBackVisible;
			AllPatientTabs.ForEach(q => PatientHeaderModels.Add(q, new PatientHeaderModel()));

			CommitCommand = CommandFunc.CreateAsync(Commit, () => !HasModelErrors());
			CancelCommand = CommandFunc.CreateAsync(Cancel);
			LogoutCommand = CommandFunc.CreateAsync(Logout);
			ChangePasswordCommand = CommandFunc.CreateAsync(ChangePassword);
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

			UIFunc.ShowLoading();

			var newModel = new UserProfile 
			{ 
				IsNewRow = true,
				Type = 1,
			};
			if (U.IsDebug)
			{
				newModel.FirstName = "Test1";
				newModel.LastName = "Test1";
				newModel.AddressLine1 = "AddressLine1";
				newModel.City = "City1";
				newModel.ProvinceOrStateRowId = new Guid("75D55A3F-FD2E-4EBA-A597-53E5A5BE532C");
				newModel.Postcode = "Postcode1";
				newModel.Phone = "123-45-67";
				newModel.Email = "test1@gmail.com";
				newModel.Password = "123456";
				newModel.PasswordRepeat = "123456";
			}

			var task1 = !IsNewRow ? WebServiceFunc.GetProfile(UserProfileRowId) : Task.FromResult(newModel);
			var task3 = WebServiceFunc.GetStates(1);
			await Task.WhenAll(task1, task3);
			if (task1.Result == null || task3.Result == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return false;
			}

			DdlStates = task3.Result.OrderBy(q => q.Name).ToArray();

			Model = task1.Result;
			SetupModel(Model);

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

		void SetupModel(UserProfile model)
		{
			model.PropertyChanged += (s, e) => OnModelChanged(model, e.PropertyName);
		}

		void OnModelChanged(UserProfile model, string propertyName)
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
			if (IsNewRow)
			{
				var order = new Order
				{
					IsNew = true,
					UserProfile = Model,
					Pois = new List<PatientOrderItem>(),
				};

				UIFunc.ShowLoading(U.StandartUpdatingText);
				var result = await WebServiceFunc.SubmitRegister(order);
				UIFunc.HideLoading();

				if (!result.Item1)
				{
					await UIFunc.AlertError(U.GetErrorUpdateText(result.Item2));
					return;
				}

				var userProfileRowId = result.Item3.UserProfileRowId;
				UserOptions.SetUsernamePassword(Model.Email, Model.Password, userProfileRowId);

				//NavFunc.RemovePages<Views.ProfileView>();
				//var viewmodel = new UserOrderListViewModel();
				//await NavFunc.NavigateToAsync(viewmodel);
				await NavFunc.RestartApp();
			}
			else
			{
				var userProfileRowId = await WebServiceFunc.CreateOrUpdateProfile(Model);
				UIFunc.HideLoading();

				if (userProfileRowId == default(Guid))
				{
					await UIFunc.AlertError(U.StandartErrorUpdateText);
					return;
				}

				await NavFunc.Pop();
			}
		}


		public override async Task<bool> BeforePageClose()
		{
			if (!IsBackVisible) return false;
			return true;
		}

		public async Task Cancel()
		{
			
		}

		public async Task Logout()
		{
			U.Logout();
			await NavFunc.RestartApp();
		}

		public async Task ChangePassword()
		{
			await ChangePasswordViewModel.OpenPage();
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
			if (!IsValidEmail(Model.Email))
			{
				errors.Add(nameof(Model.Email));
			}

			if (Model.IsNewRow)
			{
				if (IsEmptyFieldValue(Model.Password))
				{
					errors.Add(nameof(Model.Password));
				}
				if (IsEmptyFieldValue(Model.PasswordRepeat))
				{
					errors.Add(nameof(Model.PasswordRepeat));
				}

				if (!IsValidPassword(Model.Password))
				{
					errors.Add(nameof(Model.Password));
				}
				if (!IsValidPassword(Model.PasswordRepeat))
				{
					errors.Add(nameof(Model.PasswordRepeat));
				}
			}

			if (Model.Password != Model.PasswordRepeat)
			{
				errors.Add(nameof(Model.Password));
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

		bool IsEmptyFieldValue(object arg) => ValidatorFunc.IsEmptyFieldValue(arg);
		bool IsValidEmail(string arg) => ValidatorFunc.IsValidEmail(arg);
		bool IsValidPassword(string arg) => ValidatorFunc.IsValidPassword(arg);

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
			var viewModel = new ProfileViewModel();
			await NavFunc.NavigateToAsync(viewModel);
		}
	}

	public class ProfileViewModel_BorderColorConverter : IValueConverter
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


