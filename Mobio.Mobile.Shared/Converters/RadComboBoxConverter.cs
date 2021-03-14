using System;
using System.Globalization;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Converters
{
    public class RadComboBoxConverter : BindableObject, IValueConverter
	{
		public RadComboBoxConverter()
		{
			var ggg = 100;
		}

		private OneBuilder.Model.State[] _ddlStates;
		public OneBuilder.Model.State[] DdlStates 
		{ 
			get
			{
				return _ddlStates;
			}
			set
			{
				_ddlStates = value;
			}
		}

		public static readonly BindableProperty StartAnimationProperty =
			BindableProperty.Create(nameof(StartAnimation), typeof(object), typeof(RadComboBoxConverter), null, propertyChanged: OnStartAnimationChanged, defaultBindingMode: BindingMode.TwoWay);

		public object StartAnimation
		{
			get
			{
				return (bool)GetValue(StartAnimationProperty);
			}
			set
			{
				SetValue(StartAnimationProperty, value);
			}
		}

		static void OnStartAnimationChanged(BindableObject vw, object oldValue, object newValue)
		{
			var aaa = 100;
		}

		public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
			//var vv = (OneBuilder.Model.State)null;
			//return vv;
			return value;
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
			return value;
		}
    }
}
