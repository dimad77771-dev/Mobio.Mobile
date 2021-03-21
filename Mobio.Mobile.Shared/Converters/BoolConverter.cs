using System;
using System.Globalization;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Converters
{
	public class BoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return object.Equals(value, true);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return object.Equals(value, true);
		}
	}
}
