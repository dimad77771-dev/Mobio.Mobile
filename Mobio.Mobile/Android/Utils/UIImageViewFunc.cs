using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Drawing;
using Android.Widget;

namespace OneBuilder.Mobile.Droid.Utils
{
	public static class UIImageViewFunc
	{
	  //  public static void FitToSize(this UIImageView self)
	  //  {
			//var image = self.Image;
			//if (image != null && self.Frame.Height > 0)
			//{
			//	var vww = self.Frame.Width;
			//	var vhh = self.Frame.Height;
			//	var vkoef = vww / vhh;
			//	var iww = image.Size.Width;
			//	var ihh = image.Size.Height;
			//	var ikoef = iww / ihh;

			//	double rww, rhh, rxx, ryy;
			//	if (ikoef < vkoef)
			//	{
			//		rhh = vhh;
			//		rww = (iww / ihh) * rhh;
			//		ryy = 0;
			//		rxx = (vww - rww) / 2;
			//	}
			//	else
			//	{
			//		rww = vww;
			//		rhh = (ihh / iww) * rww;
			//		rxx = 0;
			//		ryy = (vhh - rhh) / 2;
			//	}
			//	var koef = (double)rww / (double)iww;
			//	var image2 = image.Resize(koef);
			//	self.Image = image2;
			//	self.Bounds = new CGRect(rxx, ryy, rww, rhh);
			//	image.Dispose();
			//}
	  //  }


	  // public static void ClipToSize(this ImageView self)
	  //  {
			//var image = self.Image;
			//if (image != null && self.Frame.Height > 0)
			//{
			//	var vww = self.Frame.Width;
			//	var vhh = self.Frame.Height;
			//	var vkoef = vww / vhh;
			//	var iww = image.Size.Width;
			//	var ihh = image.Size.Height;
			//	var ikoef = iww / ihh;

			//	double cww, chh, cxx, cyy;
			//	double koef;
			//	if (ikoef < vkoef)
			//	{
			//		cww = iww;
			//		chh = (vhh / vww) * cww;
			//		cxx = 0;
			//		cyy = (ihh - chh) / 2;
			//		koef = vww / cww;
			//	}
			//	else
			//	{
			//		chh = ihh;
			//		cww = (vww / vhh) * chh;
			//		cyy = 0;
			//		cxx = (iww - cww) / 2;
			//		koef = vhh / chh;
			//	}
			//	var image2 = image.ClipAndResize(new CGRect(cxx, cyy, cww, chh), koef);
			//	self.Image = image2;
			//	self.Bounds = new CGRect(0, 0, vww, vhh);
			//	image.Dispose();
			//}
	  //  }
	}
}
