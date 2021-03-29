using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Droid.Renderers;

[assembly: ExportRenderer(typeof(LabelEx), typeof(LabelExRenderer))]
namespace OneBuilder.Mobile.Droid.Renderers
{
	class LabelExRenderer : LabelRenderer
	{
		public LabelExRenderer(Android.Content.Context context) : base(context)
		{
		}


		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (ElementEx == null || Control == null)
			{
				return;
			}

			if (e.NewElement != null)
			{
				UpdateAdjustsFontSizeToFitWidth();
			}


			var letterSpacing = ElementEx.LetterSpacing;
			if (letterSpacing > 0)
			{
				this.Control.LetterSpacing = letterSpacing;
				this.UpdateLayout();
			}
		}


		


		void UpdateAdjustsFontSizeToFitWidth()
		{
			//var color = ElementEx.AdjustsFontSizeToFitWidth;
			if (ElementEx.AdjustsFontSizeToFitWidth)
			{
				//Control.AdjustsFontSizeToFitWidth = true;
				//Control.Lines = 1;
				//Control.BackgroundColor = UIColor.Yellow;
			}
		}




		LabelEx ElementEx
		{
			get
			{
				return (LabelEx)Element;
			}
		}
	}
}
