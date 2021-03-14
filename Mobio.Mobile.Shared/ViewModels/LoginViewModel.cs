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
	public class LoginViewModel : PageViewModel
	{
		public String BusinessCode { get; set; }
		public String UserName { get; set; }
		public String Password { get; set; }

		public Command CommitCommand => CommandFunc.CreateAsync(Commit);
		public Command CancelCommand => CommandFunc.CreateAsync(Cancel);
		public Command BusinessCodeReturnCommand => CommandFunc.CreateAsync(BusinessCodeReturn);
		public Command UserNameReturnCommand => CommandFunc.CreateAsync(UserNameReturn);
		public Command PasswordReturnCommand => CommandFunc.CreateAsync(PasswordReturn);
		public Keyboard BusinessCodeKeyboard => Keyboard.Create(KeyboardFlags.None);
		public Keyboard UserNameKeyboard => Keyboard.Create(KeyboardFlags.None);
		public Keyboard PasswordKeyboard => Keyboard.Create(KeyboardFlags.None);

		public Boolean BusinessCodeFocusTo { get; set; }
		public Boolean UserNameFocusTo { get; set; }
		public Boolean PasswordFocusTo { get; set; }

		public Boolean IsCommit { get; set; }


		public override async Task Init()
		{
			//HeaderTitle = "Login";
			IsBackVisible = false;
			if (U.IsDebug)
			{
				//BusinessCode = "Polyforce"; UserName = "Bogdan"; Password = "123456";
				BusinessCode = "Polyforce"; UserName = "Boris"; Password = @"Slavuta~23!";
				//UserName = "Guest"; Password = "123456";
				if (WebService.WEBBASEADR == @"https://obo.imgroup.ca")
				{
					BusinessCode = "Polyforce"; UserName = "Bogdan"; Password = "$uper.user";
					//Password = @"$uper.user";
				}
			}
		}

		public override async Task OnAppearing()
		{
			BusinessCodeFocusTo = true;
		}

		public async Task Commit()
		{
			//if (string.IsNullOrEmpty(UserName))
			//{
			//	await UIFunc.AlertError("User Name is required");
			//	UserNameFocusTo = true;
			//	return;
			//}

			//IsCommit = true;
			//await NavFunc.Pop();

			if (!await (new AuthenticateTask()).Run(BusinessCode, UserName, Password))
			{
				return;
			}

			//var homeViewModel = new HomeViewModel();
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
			UIFunc.ExitApp();
		}

		public async Task BusinessCodeReturn()
		{
			UserNameFocusTo = true;
		}


		public async Task UserNameReturn()
		{
			PasswordFocusTo = true;
		}

		public async Task PasswordReturn()
		{
		}


	}
}

