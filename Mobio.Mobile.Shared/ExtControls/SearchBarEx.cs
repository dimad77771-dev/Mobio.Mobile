using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace OneBuilder.Mobile
{
	public class SearchBarEx : SearchBar
	{
		public SearchBarEx()
		{
		}
	}

	public static class SearchBarExtensions
	{
		public static void SetupEvent(this SearchBar self, Func<Task> action)
		{
			self.SearchButtonPressed += async (s, e) => await action();

			self.TextChanged += async (s, e) =>
			{
				if (String.IsNullOrEmpty(self.Text))
				{
					await action();
				}
			};
		}

		public static void SetupEvent2(this SearchBar self, Action action)
		{
			self.SearchButtonPressed += (s, e) => action();

			self.TextChanged += (s, e) =>
			{
				if (String.IsNullOrEmpty(self.Text))
				{
					action();
				}
			};
		}

		
	}
}
