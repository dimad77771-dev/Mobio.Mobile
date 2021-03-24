using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class LoginModel : ViewModelBase
	{
		public string email { get; set; }
		public string password { get; set; }
		public bool IsNewRow { get; set; } = false;
	}
}
