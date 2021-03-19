using HtmlAgilityPack;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class ImageFunc
	{
		public static byte[] GetBytesFromBase64String(string arg)
		{
			if (string.IsNullOrEmpty(arg))
			{
				return null;
			}

			var s1 = "data:image/jpeg;base64,";
			if (arg.StartsWith(s1))
			{
				arg = arg.Substring(s1.Length);
			}
			var bytes = Convert.FromBase64String(arg);
			return bytes;
		}

		public static ImageSource GetStreamFromBase64String(string arg)
		{
			if (string.IsNullOrEmpty(arg))
			{
				return null;
			}

			var s1 = "data:image/jpeg;base64,";
			if (arg.StartsWith(s1))
			{
				arg = arg.Substring(s1.Length);
			}
			var bytes = Convert.FromBase64String(arg);

			var source = ImageSource.FromStream(() =>
			{
				return new MemoryStream(bytes);
			});

			return source;
		}

	}
}
