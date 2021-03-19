using System;
using System.Globalization;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Converters
{
	public class ThreeStateBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (object.Equals(parameter, "+"))
			{
				return object.Equals(value, true);
			}
			else if (object.Equals(parameter, "-"))
			{
				return object.Equals(value, false);
			}
			else throw new ArgumentException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (object.Equals(parameter, "+"))
			{
				return object.Equals(value, true);
			}
			else if (object.Equals(parameter, "-"))
			{
				return object.Equals(value, false);
			}
			else throw new ArgumentException();
		}
	}
}
