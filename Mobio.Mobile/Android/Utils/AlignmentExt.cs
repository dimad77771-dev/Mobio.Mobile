using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace OneBuilder.Mobile.Droid.Utils
{
	public static class AlignmentExt
	{
		public static GravityFlags ToHorizontalGravityFlags(this Xamarin.Forms.TextAlignment alignment)
		{
			switch (alignment)
			{
				case Xamarin.Forms.TextAlignment.Center:
					return GravityFlags.AxisSpecified;
				case Xamarin.Forms.TextAlignment.End:
					return GravityFlags.End;
				default:
					return GravityFlags.Start;
			}
		}

		public static GravityFlags ToVerticalGravityFlags(this Xamarin.Forms.TextAlignment alignment)
		{
			switch (alignment)
			{
				case Xamarin.Forms.TextAlignment.Start:
					return GravityFlags.Top;
				case Xamarin.Forms.TextAlignment.End:
					return GravityFlags.Bottom;
				default:
					return GravityFlags.CenterVertical;
			}
		}
	}
}
