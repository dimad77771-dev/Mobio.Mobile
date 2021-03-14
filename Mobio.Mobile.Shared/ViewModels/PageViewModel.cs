using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneBuilder.Mobile.ViewModels
{
	public abstract class PageViewModel : ViewModelBase
	{
		public string HeaderTitle { get; set; }
		public bool IsBackVisible { get; set; } = true;
		public bool IsInfoAvailable { get; set; } = false;
		public Page Page { get; set; }

		public ICommand NavigateBackCommand => CommandFunc.CreateAsync(NavFunc.NavigateBackAsync);

		public virtual async Task Init()
		{
		}

		public virtual async Task InitAfterBinding()
		{
		}


		public virtual async Task<bool> BeforePageClose()
		{
			return true;
		}

		public virtual async Task OnAppearing()
		{
		}

		public virtual async Task OnAppearingEx(ContentPageEx view)
		{
		}


		public virtual async Task OnDisappearing()
		{
		}

	}
}
