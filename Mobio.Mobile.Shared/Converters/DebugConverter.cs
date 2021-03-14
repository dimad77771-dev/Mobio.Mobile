using System;
using System.Globalization;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Converters
{
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
