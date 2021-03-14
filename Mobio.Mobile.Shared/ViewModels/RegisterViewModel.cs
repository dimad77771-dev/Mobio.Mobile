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

namespace OneBuilder.Mobile.ViewModels
{
	public class RegisterViewModel : PageViewModel
	{
		public UserProfile Model { get; set; }
		public State[] DdlStates { get; set; }
		public string[] AAAA { get; set; } = new[] { "1", "2", "33" };
		//url: "/utils/getCountries",

		public Command CommitCommand => CommandFunc.CreateAsync(Commit);
		public Command CancelCommand => CommandFunc.CreateAsync(Cancel);

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

			if (U.IsDebug)
			{
				DdlStates = new[]
				{
					new State { CountryId = 1, Name = "BC", RowId = new Guid("55872198-BE90-4D5E-B607-279700DBA029") },
					new State { CountryId = 1, Name = "NS", RowId = new Guid("D25C24A2-88D2-409E-A035-2B0F183C1C77") },
					new State { CountryId = 1, Name = "NL", RowId = new Guid("ACF70332-0ED2-484B-9E72-4C23F58909FA") },
					new State { CountryId = 1, Name = "ON", RowId = new Guid("75D55A3F-FD2E-4EBA-A597-53E5A5BE532C") },
				};

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

