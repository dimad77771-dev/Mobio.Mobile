using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneBuilder.Mobile.Converters
{
    public class T : IMarkupExtension
	{
		public string ResourceId { get; set; }
		public bool ToUpper { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			var value = Globalization.T(ResourceId.ToString());
			if (ToUpper)
			{
				value = value.ToUpper();
			}
			return value;
		}
	}
}
