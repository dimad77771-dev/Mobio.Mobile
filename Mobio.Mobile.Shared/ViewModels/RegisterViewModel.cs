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
		public Boolean IsShowCalendar { get; set; }
		public ObservableCollection<ScheduleItemSlot> CurrentScheduleItemSlots { get; set; }

		public String PatientTabText { get; set; }
		public Boolean IsShowTestRelatedTab { get; set; }

		public Command PatientAddCommand { get; set; }
		public Command PatientDeleteCommand { get; set; }
		public Command CommitCommand { get; set; }
		public Command CancelCommand { get; set; }
		public Command PatientItemTapCommand { get; set; }
		public Command ScheduleItemSlotTapCommand { get; set; }

		public Dictionary<string, PatientHeaderModel> PatientHeaderModels { get; set; } = new Dictionary<string, PatientHeaderModel>();

		public bool NavigationBarButton1IsVisible { get; set; } = true;
		public String NavigationBarButton1Text { get; set; } = Globalization.T("Profile");

		public Boolean IsCommit { get; set; }

		public override async Task Init()
		{
			//await UnhandledExceptionProccesing.SendErrorServer();

			//OrderRowId = new Guid("c1f3ef4a-cf61-4924-994a-6cc2cee8228e");
			OrderRowId = new Guid("4CB06476-B10C-406A-98A2-7A6693A4E590");

			HeaderTitle = "Register";
			IsBackVisible = true;
			AllPatientTabs.ForEach(q => PatientHeaderModels.Add(q, new PatientHeaderModel()));

			
			PatientAddCommand = CommandFunc.CreateAsync(PatientAdd);
			PatientDeleteCommand = CommandFunc.CreateAsync(PatientDelete, () => SelectedPatientOrderItem != null);
			CommitCommand = CommandFunc.CreateAsync(Commit, () => !HasPatientOrderItemError());
			CancelCommand = CommandFunc.CreateAsync(Cancel);
			PatientItemTapCommand = new Command<ItemTapCommandContext>(PatientItemTap);
			ScheduleItemSlotTapCommand = new Command<ItemTapCommandContext>(ScheduleItemSlotTap);
			//this.PropertyChanged += PropertyChangedAction;

			U.RequestMainThread(async () =>
			{
				if (!await LoadData()) return;

				CalendarManager.Control.SelectionChanged += (s, e) => CalcCurrentScheduleItemSlots();
				SelectedPatientOrderItem = PatientOrderItems.FirstOrDefault();
				CalcAll();
			});
		}

		//private void PropertyChangedAction(object sender, PropertyChangedEventArgs e)
		//{
		//	if (e.PropertyName == nameof())
		//}

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
			SetupPatientOrderItems(PatientOrderItems);

			UIFunc.HideLoading();
			return true;
		}


		void PatientItemTap(ItemTapCommandContext context)
		{
			var item = (PatientOrderItem)context.Item;
			SetSelectedPatientOrderItem(item);
		}

		void ScheduleItemSlotTap(ItemTapCommandContext context)
		{
			var item = (ScheduleItemSlot)context.Item;
			if (item.IsFull != true)
			{
				SetCurrentScheduleItemSlotRowId(item.RowId);
			}
			CalcScheduleItemSlots();
		}


		

		void SetSelectedPatientOrderItem(PatientOrderItem item)
		{
			SelectedPatientOrderItem = item;
			CalcAll();
		}

		void SetupPatientOrderItems(IEnumerable<PatientOrderItem> items)
		{
			items.ForEach(q =>
			{
				q.PropertyChanged += (s, e) => OnPatientOrderItemChanged(q, e.PropertyName);
				q.Patient.PropertyChanged += (s, e) => OnPatientOrderItemChanged(q, "Patient." + e.PropertyName);
				q.LabConsent.PropertyChanged += (s, e) => OnPatientOrderItemChanged(q, "LabConsent." + e.PropertyName);
				q.ScreenQuiz.PropertyChanged += (s, e) => OnPatientOrderItemChanged(q, "ScreenQuiz." + e.PropertyName);
			});
		}

		void OnPatientOrderItemChanged(PatientOrderItem pitem, string propertyName)
		{
			if (propertyName == nameof(pitem.InstitutionProfileRowId))
			{
				CalcAppointment();
			}

			var fields = new[] { "InstitutionProfileRowId", "Patient.FirstName", "Patient.LastName", "Patient.BirthDate" };
			if (fields.Contains(propertyName))
			{
				ValidateGeneral();
				CommandEnabledRefresh();
			}

			if (propertyName.StartsWith("LabConsent."))
			{
				ValidateLabConsent();
				CommandEnabledRefresh();
			}

			if (propertyName.StartsWith("ScreenQuiz."))
			{
				ValidateScreenQuestionnaire();
				CommandEnabledRefresh();
			}
		}

		void CalcAll()
		{
			CalcPatients();
			CalcAppointment();
			CalcVisible();
			ValidateAll();
			CommandEnabledRefresh();
		}

		void CommandEnabledRefresh()
		{
			this.ChangeAllCanExecute();
		}

		void CalcVisible()
		{
			IsShowTestRelatedTab = SelectedPatientOrderItem?.PatientOrderItemStatusRowId == new Guid("e9eed1aa-d220-4415-b4af-d7b27baef450");
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
			var institutionProfileRowId = SelectedPatientOrderItem?.InstitutionProfileRowId;
			IsShowCalendar = (institutionProfileRowId != null);
			if (!IsShowCalendar)
			{
				return;
			}

			var calendar = CalendarManager.Control;
			calendar.SetStyleForCell = null;
			calendar.SetStyleForCell = CalendarEvaluateCellStyle;

			var selectedDate = (GetCurrentScheduleItemSlot()?.Start)?.Date;
			calendar.SelectedDate = selectedDate;

			CalcCurrentScheduleItemSlots();
		}

		void CalcCurrentScheduleItemSlots()
		{
			var calendar = CalendarManager.Control;
			var selectedDate = calendar.SelectedDate;
			var allScheduleItemSlots = GetCurrentScheduleItemSlots();
			CurrentScheduleItemSlots = allScheduleItemSlots.Where(q => q.Start.Date == selectedDate).ToObservableCollection();
			CalcScheduleItemSlots();
		}

		void CalcScheduleItemSlots()
		{
			CurrentScheduleItemSlots.ForEach(q =>
			{
				q.IsSelectedSlot = (q.RowId == GetCurrentScheduleItemSlotRowId());
			});
		}

		Guid? GetCurrentScheduleItemSlotRowId()
		{
			var scheduleItemSlotRowId = SelectedPatientOrderItem?.Appointment?.ScheduleItemSlotRowId;
			return scheduleItemSlotRowId;
		}
		void SetCurrentScheduleItemSlotRowId(Guid scheduleItemSlotRowId)
		{
			SelectedPatientOrderItem.Appointment.ScheduleItemSlotRowId = scheduleItemSlotRowId;
			ValidateAppointment();
			CommandEnabledRefresh();
		}



		ScheduleItemSlot GetCurrentScheduleItemSlot()
		{
			var scheduleItemSlots = GetCurrentScheduleItemSlots();
			var scheduleItemSlotRowId = GetCurrentScheduleItemSlotRowId();
			var scheduleItemSlot = scheduleItemSlots.FirstOrDefault(q => q.RowId == scheduleItemSlotRowId);
			return scheduleItemSlot;
		}


		T.CalendarCellStyle CalendarEvaluateCellStyle(T.CalendarCell cell)
		{
			if (cell is T.CalendarDayCell)
			{
				if (IsShowCalendar)
				{
					var dcell = (T.CalendarDayCell)cell;
					var scheduleItemSlots = GetCurrentScheduleItemSlots().Where(q => q.Start.Date == dcell.Date).ToArray();
					if (scheduleItemSlots.Any())
					{
						var hasFree = scheduleItemSlots.Any(q => q.IsFull == false);
						return new T.CalendarCellStyle
						{
							BackgroundColor = (hasFree ? Color.FromHex("#5cb85c") : Color.FromHex("#ac2925")),
							BorderColor = (hasFree ? Color.FromHex("#255625") : Color.FromHex("#761c19")),
							TextColor = Color.White,
						};
					}
				}
			}

			return null; // default style
		}

		ScheduleItemSlot[] GetCurrentScheduleItemSlots()
		{
			var institutionProfileRowId = GetCurrentInstitutionRowId();
			var scheduleItemSlots = new ScheduleItemSlot[0];
			AllBookingSlots.TryGetValue(institutionProfileRowId, out scheduleItemSlots);
			return scheduleItemSlots;
		}

		Guid GetCurrentInstitutionRowId()
		{
			var institutionProfileRowId = SelectedPatientOrderItem?.InstitutionProfileRowId ?? default(Guid);
			return institutionProfileRowId;
		}

		public override async Task OnAppearingEx(ContentPageEx view)
		{
			//view.
			//BusinessCodeFocusTo = true;
		}

		public async Task Commit()
		{
			Order.Pois = PatientOrderItems.ToList();

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

		public async Task PatientAdd()
		{
			var patient = new Patient
			{
				//RowId = Guid.NewGuid(),
				//FirstName = "",
				//LastName = "",
			};
			var appointment = new Appointment
			{
				//RowId = Guid.NewGuid(),
			};
			var screenQuiz = new ScreenQuiz
			{
				//RowId = Guid.NewGuid(),
			};
			var labConsent = new LabConsent
			{
				//RowId = Guid.NewGuid(),
			};

			var patientOrderItem = new PatientOrderItem
			{
				//RowId = Guid.NewGuid(),
				IsNew = true,
				IsNewRow = true,
				Patient = patient,
				Appointment = appointment,
				ScreenQuiz = screenQuiz,
				LabConsent = labConsent,
			};

			SetupPatientOrderItems(new[] { patientOrderItem });
			PatientOrderItems.Add(patientOrderItem);
			SelectedPatientOrderItem = patientOrderItem;
			CalcAll();
		}

		public async Task PatientDelete()
		{
			if (SelectedPatientOrderItem == null) return;
			if (!await UIFunc.ConfirmAsync($"Delete row \"{SelectedPatientOrderItem.Patient.FullPatientName}\"?"))
			{
				return;
			}

			PatientOrderItems.Remove(SelectedPatientOrderItem);
			SelectedPatientOrderItem = PatientOrderItems.FirstOrDefault();
			CalcAll();
		}



		public override async Task<bool> BeforePageClose()
		{
			UIFunc.ExitApp();
			return false;
		}

		public async Task Cancel()
		{
			
		}

		void ValidateAll()
		{
			ValidateGeneral();
			ValidateScreenQuestionnaire();
			ValidateLabConsent();
			ValidateAppointment();
		}

		public ObservableCollection<string> ErrorsGeneral { get; set; } = new ObservableCollection<string>();
		void ValidateGeneral()
		{
			var errors = new List<string>();
			if (SelectedPatientOrderItem != null)
			{
				if (SelectedPatientOrderItem.InstitutionProfileRowId == null)
				{
					errors.Add("InstitutionProfileRowId");
				}
				if (IsEmptyFieldValue(SelectedPatientOrderItem.Patient.FirstName))
				{
					errors.Add("FirstName");
				}
				if (IsEmptyFieldValue(SelectedPatientOrderItem.Patient.LastName))
				{
					errors.Add("LastName");
				}
				if (SelectedPatientOrderItem.Patient.BirthDate == null)
				{
					errors.Add("BirthDate");
				}
			}
			ErrorsGeneral = errors.ToObservableCollection();

			PatientHeaderModels[General].HasError = errors.Any();
		}

		void ValidateScreenQuestionnaire()
		{
			var valid = true;

			if (SelectedPatientOrderItem != null)
			{
				var screenQuiz = SelectedPatientOrderItem.ScreenQuiz;
				var values = new[] { screenQuiz.Fever,screenQuiz.Cough,screenQuiz.RunnyNoise,screenQuiz.LossOfTaste,screenQuiz.DifficultyBreathing,screenQuiz.SoreThroat,
										screenQuiz.Nausea,screenQuiz.Tired,screenQuiz.ContactWithCovid,screenQuiz.TravelReturn,screenQuiz.TravelPressured };
				valid = values.All(q => q != null);
			}

			PatientHeaderModels[ScreenQuestionnaire].HasError = !valid;
		}

		void ValidateLabConsent()
		{
			var valid = true;
			
			if (SelectedPatientOrderItem != null)
			{
				var labConsent = SelectedPatientOrderItem.LabConsent;
				valid = (labConsent.Auth1 == true && labConsent.Auth2 == true && labConsent.Auth3 == true
					&& labConsent.Auth4 == true && labConsent.Auth5 == true && labConsent.Auth6 == true);
			}

			PatientHeaderModels[LabConsent].HasError = !valid;
		}

		void ValidateAppointment()
		{
			var valid = true;
			if (SelectedPatientOrderItem != null)
			{
				valid = (GetCurrentScheduleItemSlotRowId() != null && GetCurrentScheduleItemSlotRowId() != default(Guid));
			}
			PatientHeaderModels[Appointment].HasError = !valid;
		}

		bool HasPatientOrderItemError()
		{
			return false;
			//return PatientHeaderModels.Any(q => q.Value.HasError);
		}

		bool IsEmptyFieldValue(string arg)
		{
			return string.IsNullOrEmpty((arg ?? "").Trim());
		}
		

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

	public class RegisterViewModel_BorderColorConverter : IValueConverter
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


