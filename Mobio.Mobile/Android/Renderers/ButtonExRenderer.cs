using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Droid.Renderers;
using OneBuilder.Mobile.Droid.Utils;

[assembly: ExportRenderer(typeof(ButtonEx), typeof(ButtonExRenderer))]
namespace OneBuilder.Mobile.Droid.Renderers
{
	class ButtonExRenderer : ButtonRenderer
	{
		public ButtonExRenderer(Android.Content.Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);
			

			if (e.NewElement != null)
			{
				UpdateTitleShadowColor();
				UpdateImage2();
			}
		}

		//public override SizeF SizeThatFits(SizeF size)
		//{
		//	var contentEdgeInsets = Control.ContentEdgeInsets;
		//	var eww = contentEdgeInsets.Left + contentEdgeInsets.Right;
		//	var ehh = contentEdgeInsets.Top + contentEdgeInsets.Bottom;
		//	var cww = size.Width - eww;
		//	var chh = size.Height - ehh;

		//	var size2 = new CGSize(cww, double.MaxValue);
		//	var size3 = Control.TitleLabel.SizeThatFits(size2);
		//	var ww = size3.Width + eww;
		//	var hh = size3.Height + ehh;
		//	return new SizeF(ww, hh);


		//	var result = base.SizeThatFits(size);
		//	return result;
		//}



		


		void UpdateImage2()
		{
			var img2 = ElementEx.Image2;
			if (img2 != null)
			{
				var drawable1 = UIImageFunc.ImageSourceEx2UIImage(img2, isDrawableSimple: true);
				var drawable2 = ElementEx.Image2Down != null ? 
					UIImageFunc.ImageSourceEx2UIImage(ElementEx.Image2Down, isDrawableSimple: true) :
					UIImageFunc.ImageSourceEx2UIImage(ElementEx.Image2, isDrawableSimple: true, isdown: true);


				var states = new StateListDrawable();
				states.AddState(new int[] { Android.Resource.Attribute.StatePressed }, drawable2);
				states.AddState(new int[] { }, drawable1);
				Control.Background = states;

				((IVisualElementController)Element).NativeSizeChanged();
			}
		}


		void UpdateTitleShadowColor()
		{
			var color = ElementEx.TitleShadowColor;
			
			if (color != default(Color))
			{
				Control.SetShadowLayer(2, 1, 1, color.ToAndroid());
			}
		}



		ButtonEx ElementEx
		{
			get
			{
				return (ButtonEx)Element;
			}
		}
	}
}
