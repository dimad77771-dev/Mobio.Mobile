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
	public class DrawableSimpleExt : Drawable
	{
		public int DrawableResourceId;

		public DrawableSimpleExt(int drawableResourceId)
			: base()
		{
			DrawableResourceId = drawableResourceId;
		}

		public override void Draw(Canvas canvas)
		{
			var drawable = ResourceManagerA.GetDrawable(DrawableResourceId);
			drawable.SetBounds(0, 0, this.Bounds.Width(), this.Bounds.Height());
			drawable.Draw(canvas);
		}


		public override void SetAlpha(int alpha)
		{
		}
		public override void SetColorFilter(ColorFilter cf)
		{
		}
		public override int Opacity
		{
			get
			{
				return 0;
			}
		}

		public static DrawableSimpleExt FromResource(int resourceId)
		{
			var ret = new DrawableSimpleExt(resourceId);
			return ret;
		}
	}
}
