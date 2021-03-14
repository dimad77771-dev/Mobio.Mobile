using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using OneBuilder.Mobile.Droid.Renderers;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Droid.Utils;

[assembly: ExportRenderer(typeof(ImageEx), typeof(ImageExRenderer))]
namespace OneBuilder.Mobile.Droid.Renderers
{
	class ImageExRenderer : ImageRenderer
	{
		public ImageExRenderer(Android.Content.Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				UpdateImage2();
				UpdateHidden();
				UpdateIsVisible2();
			}
		}

		public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
		{
			if (ElementEx.IsBackgroundImage)
			{
				return new SizeRequest(Size.Zero);
			}
			var result = base.GetDesiredSize(widthConstraint, heightConstraint);
			return result;
		}

		void UpdateImage2()
		{
			var img2 = ElementEx.Image2;
			var img2pressed = ElementEx.Image2Pressed;
			if (img2 != null)
			{
				if (img2pressed == null)
				{
					var drawable = UIImageFunc.ImageSourceEx2UIImage(img2, isDrawableSimple: false);
					Control.SetImageDrawable(drawable);
					((IVisualElementController)Element).NativeSizeChanged();
				}
				else
				{
					var drawable1 = UIImageFunc.ImageSourceEx2UIImage(img2pressed, isDrawableSimple: true);
					var drawable2 = UIImageFunc.ImageSourceEx2UIImage(img2, isDrawableSimple: true);
					var states = new StateListDrawable();
					states.AddState(new int[] { Android.Resource.Attribute.StatePressed }, drawable1);
					states.AddState(new int[] { }, drawable2);
					Control.SetImageDrawable(states);
					((IVisualElementController)Element).NativeSizeChanged();
				}
			}
		}

		void UpdateHidden()
		{
			var hidden_old = Control.Visibility;
			var hidden_new = ElementEx.Hidden ? ViewStates.Invisible : ViewStates.Visible;
			if (hidden_new != hidden_old)
			{
				Control.Visibility = hidden_new;
			}
		}

		void UpdateIsVisible2()
		{
			if (ElementEx.IsVisible2 != null)
			{
				ElementEx.IsVisible = (bool)ElementEx.IsVisible2;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == ImageEx.HiddenProperty.PropertyName)
			{
				UpdateHidden();
			}
			else if (e.PropertyName == ImageEx.IsVisible2Property.PropertyName)
			{
				UpdateIsVisible2();
			}
			else if (e.PropertyName == ImageEx.Image2Property.PropertyName)
			{
				UpdateImage2();
			}
		}


		//protected override void OnLayout(bool changed, int l, int t, int r, int b)
		//{
		//	base.OnLayout(changed, l, t, r, b);

		//	if (ElementEx.ClipToSize)
		//	{
		//		Control.ClipToSize();
		//	}
		//}



		ImageEx ElementEx
		{
			get
			{
				return (ImageEx)Element;
			}
		}
	}
}
