using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	[TypeConverter(typeof(FontExConverter))]
	public class FontEx
	{
		public Font Font { get; set; }

		public FontEx(Font font)
		{
			this.Font = font;
		}

		public static FontEx Name2Font(string value)
		{
			var prop = typeof(FontStyle).GetProperty(value);
			if (prop == null) throw new ArgumentException("FontStyle." + value + " not found");
			var font = (Font)prop.GetValue(null);
			return new FontEx(font);
		}
	}

	public sealed class FontExConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			var font = FontEx.Name2Font(value);
			return font;
		}
	}
}
