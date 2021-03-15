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

namespace OneBuilder.Mobile.ViewModels
{
	public class RegisterViewModel : PageViewModel
	{
		public UserProfile Model { get; set; }
		public State[] DdlStates { get; set; }  //url: "/utils/getCountries",
		public String[] DdlGenders { get; set; } = new[] { "Male", "Female", "Other" };
		public ObservableCollection<Patient> Patients { get; set; }
		public Patient SelectedPatient { get; set; }

		public ObservableCollection<UserProfile> DdlInstitutions { get; set; }
		

		public String PatientTabText { get; set; }


		public Command CommitCommand => CommandFunc.CreateAsync(Commit);
		public Command CancelCommand => CommandFunc.CreateAsync(Cancel);
		public ICommand ItemTapCommand { get; set; }

		//public Command EntryCompletedCommand => CommandFunc.CreateAsync(BusinessCodeReturn);

		//public Keyboard BusinessCodeKeyboard => Keyboard.Create(KeyboardFlags.None);
		//public Keyboard UserNameKeyboard => Keyboard.Create(KeyboardFlags.None);
		//public Keyboard PasswordKeyboard => Keyboard.Create(KeyboardFlags.None);

		public Boolean IsCommit { get; set; }

		public override async Task Init()
		{
			//await UnhandledExceptionProccesing.SendErrorServer();

			HeaderTitle = "Register";
			IsBackVisible = true;

			ItemTapCommand = new Command<ItemTapCommandContext>(this.ItemTapped);

			if (U.IsDebug)
			{
				DdlStates = new[]
				{
					new State { CountryId = 1, Name = "BC", RowId = new Guid("55872198-BE90-4D5E-B607-279700DBA029") },
					new State { CountryId = 1, Name = "NS", RowId = new Guid("D25C24A2-88D2-409E-A035-2B0F183C1C77") },
					new State { CountryId = 1, Name = "NL", RowId = new Guid("ACF70332-0ED2-484B-9E72-4C23F58909FA") },
					new State { CountryId = 1, Name = "ON", RowId = new Guid("75D55A3F-FD2E-4EBA-A597-53E5A5BE532C") },
				};

				DdlInstitutions = new[]
				{
					new UserProfile{ RowId = new Guid("1DB56EDF-C171-42A9-8C82-64A79E21C159"), CompanyName = "School 1" },
					new UserProfile{ RowId = new Guid("C9CF120F-830C-4226-86EC-AE61AAE51B76"), CompanyName = "School 2" },
					new UserProfile{ RowId = new Guid("4E58878C-F951-49CA-A040-510F6AB33A23"), CompanyName = "School 3" },
				}.ToObservableCollection();

				Model = new UserProfile
				{
					FirstName = "First",
					LastName = "Last",
					AddressLine1 = "AddressLine1",
					City = "City",
					ProvinceOrStateRowId = new Guid("75D55A3F-FD2E-4EBA-A597-53E5A5BE532C"),
					Postcode = "Postcode",
					Phone = "Phone",

					Email = "email@mail.com",
					Password = "12345",
					PasswordRepeat = "12345",

					BorderColor = Color.Red,

					DdlStates = DdlStates,
				};

				//Model.ProvinceOrState = DdlStates[1];
				//Task.Delay

				Patients = new[]
				{
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Piter",
						LastName = "Pen",
						BirthDate = new DateTime(2003,11,7),
						Gender = "Male",
					},
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Glen",
						LastName = "Robinson",
						BirthDate = new DateTime(1977,8,17),
						Gender = "Female",
					},
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Gabija",
						LastName = "Summers",
					},
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Vincenzo",
						LastName = "Saunders",
					},
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Kalum",
						LastName = "Edwards",
					},
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Reggie",
						LastName = "Neal",
					},
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Torin",
						LastName = "Mclean",
					},
					new Patient
					{
						RowId = Guid.NewGuid(),
						FirstName = "Paulina",
						LastName = "Valenzuela",
					},
				}.ToObservableCollection();

				Patients[0].PatientOrderItem.InstitutionProfileRowId = DdlInstitutions[0].RowId;
				Patients[1].PatientOrderItem.InstitutionProfileRowId = DdlInstitutions[1].RowId;

				Patients.Select(q => q.PatientOrderItem).ForEach(q => q.DdlInstitutions = DdlInstitutions);


				if (Patients.Any())
				{
					SelectedPatient = Patients.First();
				}

				RecalcPatients();
			}
		}



		void ItemTapped(ItemTapCommandContext context)
		{
			var patient = (Patient)context.Item;
			SelectedPatient = patient;
			RecalcPatients();
		}

		void RecalcPatients()
		{
			PatientTabText = "Patients (" + Patients.Count + ")";

			foreach (var patient in Patients)
			{
				var selected = (patient == SelectedPatient);
				patient.BackgroundColor = selected ? Color.FromHex("#d12323") : Color.Transparent;
				patient.TextColor = selected ? Color.FromHex("#fff") : Color.FromHex("#333");
				patient.BorderColor = selected ? Color.FromHex("#8c8c8c") : Color.FromHex("#8c8c8c");

			}
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



	}
}

