using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class ValidatorFunc
	{
		public static bool IsEmptyFieldValue(object arg)
		{
			if (arg == null) return true;
			return string.IsNullOrEmpty(arg.ToString().Trim());
		}

		static Regex ValidEmailRegex = CreateValidEmailRegex();

		private static Regex CreateValidEmailRegex()
		{
			string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
				+ @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
				+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

			return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
		}

		public static bool IsValidEmail(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress)) return true;

			var isValid = ValidEmailRegex.IsMatch(emailAddress);
			return isValid;
		}

	}
}
