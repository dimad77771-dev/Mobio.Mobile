using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	[TypeConverter(typeof(ImageSourceExConverter))]
	public class ImageSourceEx
	{
		public string FileName;
		public double Bottom;
		public double Left;
		public double Right;
		public double Top;
		public bool HasEdgeInsets;
		public bool Halfresize;
		public bool IsEmptyImage
		{
			get
			{
				return (FileName == "Empty");
			}
		}


		public static ImageSourceEx FromFile(string fileName)
		{
			var img = new ImageSourceEx
			{
				FileName = fileName,
			};
			return img;
		}

		
		public static ImageSourceEx CreateResizableImage2(string fileName, double top, double left, double bottom, double right, bool halfresize = true)
		{
			var img = new ImageSourceEx
			{
				FileName = fileName,
				Bottom = bottom,
				Left = left,
				Right = right,
				Top = top,
				Halfresize = halfresize,
				HasEdgeInsets = true,
			};
			return img;
		}


		private ImageSourceEx() { }
	}

	public sealed class ImageSourceExConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			ImageSourceEx img;

			var vals = (value ?? "").Split(",");
			if (vals.Length == 1)
			{
				img = ImageSourceEx.FromFile(value);
			}
			else if (vals.Length == 5)
			{
				img = ImageSourceEx.CreateResizableImage2(vals[0], Int32.Parse(vals[1]), Int32.Parse(vals[2]), Int32.Parse(vals[3]), Int32.Parse(vals[4]));
			}
			else if (vals.Length == 6)
			{
				img = ImageSourceEx.CreateResizableImage2(vals[0], Int32.Parse(vals[1]), Int32.Parse(vals[2]), Int32.Parse(vals[3]), Int32.Parse(vals[4]), Boolean.Parse(vals[5]));
			}
			else throw new ArgumentException(value);

			return img;
		}
	}
}
