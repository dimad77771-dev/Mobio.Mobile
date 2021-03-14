using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using System.Reflection;

namespace OneBuilder.Mobile.Droid.Utils
{
	public static class UIImageFunc
	{
		//public static UIImage CreateResizableImage2(this UIImage self, UIEdgeInsets capInsets, UIImageResizingMode resizingMode, bool halfresize = true)
		//{
		//	//use <halfresize> for convert Android-image from "drawable-nodpi"
		//	if (halfresize)
		//	{
		//		nfloat koef = (nfloat)0.5;
		//		self = self.Resize(koef); //for Android-style
		//		return self.CreateResizableImage(new UIEdgeInsets(capInsets.Top * koef, capInsets.Left * koef, capInsets.Bottom * koef, capInsets.Right * koef), UIImageResizingMode.Stretch);
		//	}


		//	return self.CreateResizableImage(capInsets, UIImageResizingMode.Stretch);
		//}


		//public static UIImage ImageFromColor(UIColor color)
		//{
		//	var rect = new RectangleF(0, 0, 1, 1);

		//	UIGraphics.BeginImageContext(rect.Size);

		//	var ctx = UIGraphics.GetCurrentContext();
		//	ctx.SetFillColor(color.CGColor);
		//	//ctx.FillColorWithColor = (color.CGColor);
		//	ctx.FillRect(rect);

		//	var img = UIGraphics.GetImageFromCurrentImageContext();
		//	UIGraphics.EndImageContext();

		//	return img;
		//}


		public static Drawable ImageSourceEx2UIImage(ImageSourceEx imageEx, bool isDrawableSimple, bool isdown = false)
		{
			var filename = imageEx.FileName + (isdown ? "_down" : "");
			var resourceId = File2ResourceId(filename);
			if (resourceId == -1)
			{
				if (isdown)
				{
					return ImageSourceEx2UIImage(imageEx, isDrawableSimple, isdown:false);
				}
				throw new ImageFileNotFoundException(filename);
			}

			var drawable = isDrawableSimple ?
					DrawableSimpleExt.FromResource(resourceId) :
					ContextCompat.GetDrawable(U.Context, resourceId);
			return drawable;

			//var resId = File2ResourceId(imageEx.FileName);
			//if (imageEx.HasEdgeInsets)
			//{
			//	//смотри "https://github.com/KamiSempai/NinePatchBuildUtils" и "https://gist.github.com/briangriffey/4391807"
			//	// - но пока решил не вдаваться в дебри
			//	var koef = (imageEx.Halfresize ? 0.5 : 1.0);
			//	var top = (int)(imageEx.Top * koef);
			//	var left = (int)(imageEx.Left * koef);
			//	var right = (int)(imageEx.Right * koef);
			//	var bottom = (int)(imageEx.Bottom * koef);

			//	var bitmap = ResourceManagerA.GetBitmap(resId);
			//	var drawable = NinePatchBitmapFactory.createNinePathWithCapInsets(ResourceManagerA.GetResources(), bitmap, top, left, bottom, right, imageEx.FileName);
			//	return drawable;
			//}
			//else
			//{
			//	var drawable = ContextCompat.GetDrawable(U.Context, resId);
			//	return drawable;
			//}
		}

		static Dictionary<string, Int32> file2ResourceIdSaved = new Dictionary<string, Int32>();
		static Int32 File2ResourceId(String file)
		{
			Int32 ret;
			if (file2ResourceIdSaved.TryGetValue(file, out ret))
			{
				return ret;
			}
			else
			{
				var restype = typeof(Resource.Drawable);

				var prop = restype.GetField(file, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
				if (prop == null)
				{
					return -1;
				}
				var id = (Int32)(prop.GetValue(null));
				file2ResourceIdSaved.Add(file, id);
				return id;
			}
		}

		
	}


	public class ImageFileNotFoundException : Exception
	{
		public ImageFileNotFoundException(string filename) : base("filename=" + filename)
		{
		}
	}
}

