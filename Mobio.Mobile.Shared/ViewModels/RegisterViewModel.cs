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
using Xamarin.Forms;
using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.Views;
using System.Windows.Input;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using T = Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input;
using OneBuilder.Mobile.Behaviors;

namespace OneBuilder.Mobile.ViewModels
{
	public class RegisterViewModel : PageViewModel
	{
		public Guid OrderRowId { get; set; }
		public Order Order { get; set; }

		public State[] DdlStates { get; set; }
		public String[] DdlGenders { get; set; } = new[] { "Male", "Female", "Other" };
		public ObservableCollection<UserProfile> DdlInstitutions { get; set; }
		public Dictionary<Guid, ScheduleItemSlot[]> AllBookingSlots { get; set; } = new Dictionary<Guid, ScheduleItemSlot[]>();

		public UserProfile Model { get; set; }
		public ObservableCollection<PatientOrderItem> PatientOrderItems { get; set; }
		public PatientOrderItem SelectedPatientOrderItem { get; set; }

		public RadCalendarOperationBehaviorManager CalendarManager { get; set; } = new RadCalendarOperationBehaviorManager();
		public Boolean IsShowCalendar => SelectedPatientOrderItem?.InstitutionProfileRowId != null;

		public String PatientTabText { get; set; }

		public Command CommitCommand => CommandFunc.CreateAsync(Commit);
		public Command CancelCommand => CommandFunc.CreateAsync(Cancel);
		public ICommand ItemTapCommand { get; set; }

		public Dictionary<string, PatientHeaderModel> PatientHeaderModels { get; set; } = new Dictionary<string, PatientHeaderModel>();

		public Boolean IsCommit { get; set; }

		public override async Task Init()
		{
			//await UnhandledExceptionProccesing.SendErrorServer();

			//OrderRowId = new Guid("c1f3ef4a-cf61-4924-994a-6cc2cee8228e");
			OrderRowId = new Guid("4CB06476-B10C-406A-98A2-7A6693A4E590");

			HeaderTitle = "Register";
			IsBackVisible = true;
			ItemTapCommand = new Command<ItemTapCommandContext>(this.ItemTapped);
			AllPatientTabs.ForEach(q => PatientHeaderModels.Add(q, new PatientHeaderModel()));

			U.RequestMainThread(async () =>
			{
				//if (U.IsDebug) LoadDebug();

				if (!await LoadData()) return;

				if (PatientOrderItems.Any())
				{
					SelectedPatientOrderItem = PatientOrderItems.First();
				}
				CalcPatients();
			});


		}


		async Task<bool> LoadData()
		{
			UIFunc.ShowLoading();

			var task1 = WebServiceFunc.GetOrder(OrderRowId);
			var task2 = WebServiceFunc.GetInstitutionsForSchedule();
			var task3 = WebServiceFunc.GetStates(1);
			await Task.WhenAll(task1, task2, task3);
			if (task1.Result == null || task2.Result == null || task3.Result == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return false;
			}

			Order = task1.Result;
			DdlInstitutions = task2.Result.OrderBy(q => q.CompanyName).ToObservableCollection();
			DdlStates = task3.Result.OrderBy(q => q.Name).ToArray();

			var institutionRowIds = DdlInstitutions.Select(q => q.RowId).ToArray();
			var slotTasks = institutionRowIds.Select(q => WebServiceFunc.GetBookingSlots(q)).ToArray();
			await Task.WhenAll(slotTasks);
			if (slotTasks.Any(q => q.Result == null))
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return false;
			}
			for(int i = 0; i < institutionRowIds.Length; i++)
			{
				AllBookingSlots.Add(institutionRowIds[i], slotTasks[i].Result);
			}
			

			Model = Order.UserProfile;
			PatientOrderItems = Order.Pois.ToObservableCollection();

			UIFunc.HideLoading();
			return true;
		}



		void ItemTapped(ItemTapCommandContext context)
		{
			var item = (PatientOrderItem)context.Item;
			SelectedPatientOrderItem = item;
			CalcAll();
		}

		void CalcAll()
		{
			CalcPatients();
			CalcAppointment();
		}

		void CalcPatients()
		{
			PatientTabText = Globalization.T("Patients") + " (" + PatientOrderItems.Count + ")";

			foreach (var poitem in PatientOrderItems)
			{
				var selected = (poitem == SelectedPatientOrderItem);
				var patient = poitem.Patient;
				patient.BackgroundColor = selected ? Color.FromHex("#d12323") : Color.Transparent;
				patient.TextColor = selected ? Color.FromHex("#fff") : Color.FromHex("#333");
				patient.BorderColor = selected ? Color.FromHex("#8c8c8c") : Color.FromHex("#8c8c8c");
			}
		}

		void CalcAppointment()
		{

		}

		CalendarCellStyle CalendarEvaluateCellStyle(CalendarCell cell)
		{
			if (cell is CalendarDayCell)
			{
				var dcell = (CalendarDayCell)cell;
				if ( new[] { 1, 4, 12, 25, 26 }.Contains(dcell.Date.Day))
				{
					return new CalendarCellStyle
					{
						BackgroundColor = Color.FromHex("#00FF00"),
						BorderColor = Color.Green,
					};
				}
			}

			return null; // default style
		}

		public override async Task OnAppearingEx(ContentPageEx view)
		{
			//view.
			//BusinessCodeFocusTo = true;
		}

		public async Task Commit()
		{
			//if (!await (new AuthenticateTask()).Run(BusinessCode, UserName, Password))
			//{
			//	return;
			//}

			////var homeViewModel = new HomeViewModel();
			//var homeViewModel = new SerialViewModel();
			//await NavFunc.NavigateToAsync(homeViewModel);
		}

		public override async Task<bool> BeforePageClose()
		{
			UIFunc.ExitApp();
			return false;
		}

		public async Task Cancel()
		{
			
		}

		//void LoadDebug()
		//{
		//	PatientHeaderModels[General].HasError = true;

		//	DdlStates = new[]
		//	{
		//		new State { CountryId = 1, Name = "BC", RowId = new Guid("55872198-BE90-4D5E-B607-279700DBA029") },
		//		new State { CountryId = 1, Name = "NS", RowId = new Guid("D25C24A2-88D2-409E-A035-2B0F183C1C77") },
		//		new State { CountryId = 1, Name = "NL", RowId = new Guid("ACF70332-0ED2-484B-9E72-4C23F58909FA") },
		//		new State { CountryId = 1, Name = "ON", RowId = new Guid("75D55A3F-FD2E-4EBA-A597-53E5A5BE532C") },
		//	};

		//	DdlInstitutions = new[]
		//	{
		//		new UserProfile{ RowId = new Guid("1DB56EDF-C171-42A9-8C82-64A79E21C159"), CompanyName = "School 1" },
		//		new UserProfile{ RowId = new Guid("C9CF120F-830C-4226-86EC-AE61AAE51B76"), CompanyName = "School 2" },
		//		new UserProfile{ RowId = new Guid("4E58878C-F951-49CA-A040-510F6AB33A23"), CompanyName = "School 3" },
		//	}.ToObservableCollection();

		//	Model = new UserProfile
		//	{
		//		FirstName = "First",
		//		LastName = "Last",
		//		AddressLine1 = "AddressLine1",
		//		City = "City",
		//		ProvinceOrStateRowId = new Guid("75D55A3F-FD2E-4EBA-A597-53E5A5BE532C"),
		//		Postcode = "Postcode",
		//		Phone = "Phone",

		//		Email = "email@mail.com",
		//		Password = "12345",
		//		PasswordRepeat = "12345",

		//		BorderColor = Color.Red,

		//		//DdlStates = DdlStates,
		//	};

		//	//Model.ProvinceOrState = DdlStates[1];
		//	//Task.Delay

		//	Patients = new[]
		//	{
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Piter",
		//			LastName = "Pen",
		//			BirthDate = new DateTime(2003,11,7),
		//			Gender = "Male",
		//		},
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Glen",
		//			LastName = "Robinson",
		//			BirthDate = new DateTime(1977,8,17),
		//			Gender = "Female",
		//		},
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Gabija",
		//			LastName = "Summers",
		//		},
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Vincenzo",
		//			LastName = "Saunders",
		//		},
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Kalum",
		//			LastName = "Edwards",
		//		},
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Reggie",
		//			LastName = "Neal",
		//		},
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Torin",
		//			LastName = "Mclean",
		//		},
		//		new Patient
		//		{
		//			RowId = Guid.NewGuid(),
		//			FirstName = "Paulina",
		//			LastName = "Valenzuela",
		//		},
		//	}.ToObservableCollection();

		//	Patients[0].PatientOrderItem.InstitutionProfileRowId = DdlInstitutions[0].RowId;
		//	Patients[1].PatientOrderItem.InstitutionProfileRowId = DdlInstitutions[1].RowId;
		//}


		const string General = nameof(General);
		const string ScreenQuestionnaire = nameof(ScreenQuestionnaire);
		const string LabConsent = nameof(LabConsent);
		const string Appointment = nameof(Appointment);
		const string TestRelated = nameof(TestRelated);
		string[] AllPatientTabs = { General, ScreenQuestionnaire, LabConsent, Appointment, TestRelated };
		public class PatientHeaderModel : ViewModelBase
		{
			public bool HasError { get; set; }
			public string ErrorText { get; set; } = "\u231B";
		}


	}
}


