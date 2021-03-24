using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class ChangePasswordModel : ViewModelBase
	{
		public string currentPassword { get; set; }
		public string newPassword { get; set; }
		public string passwordRepeat { get; set; }
		public bool IsNewRow { get; set; } = false;
	}
}
