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
		public UserProfile Model;

		public Command CommitCommand => CommandFunc.CreateAsync(Commit);
		public Command CancelCommand => CommandFunc.CreateAsync(Cancel);

		//public Command EntryCompletedCommand => CommandFunc.CreateAsync(BusinessCodeReturn);

		//public Keyboard BusinessCodeKeyboard => Keyboard.Create(KeyboardFlags.None);
		//public Keyboard UserNameKeyboard => Keyboard.Create(KeyboardFlags.None);
		//public Keyboard PasswordKeyboard => Keyboard.Create(KeyboardFlags.None);

		public Boolean IsCommit { get; set; }

		public override async Task Init()
		{
			HeaderTitle = "Register";
			IsBackVisible = true;

			if (U.IsDebug)
			{
				Model = new UserProfile
				{
					FirstName = "First",
					LastName = "Last",
					AddressLine1 = "AddressLine1",
					City = "City",
					ProvinceOrStateRowId = null,
					Postcode = "Postcode",
					Phone = "Phone",

					Email = "email@mail.com",
					Password = "12345",
					PasswordRepeat = "12345",

					BorderColor = Color.Red,
				};
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

