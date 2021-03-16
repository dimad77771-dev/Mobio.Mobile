using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneBuilder.Mobile.Converters
{
    public class T : IMarkupExtension
	{
		public string ResourceId { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			return Globalization.T(ResourceId.ToString());
		}
	}
}
