using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Foundation;
using OneBuilder.Mobile;
using Telerik.XamarinForms.Input;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LabelEx), typeof(Mobio.Mobile.iOS.LabelExRenderer))]
namespace Mobio.Mobile.iOS
{
	class LabelExRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);
			if (ElementEx == null || Control == null)
			{
				return;
			}
			

			var letterSpacing = ElementEx.LetterSpacing;
			if (letterSpacing > 0)
			{
				var text = Control.Text;
				var attributedString = new NSMutableAttributedString(text);
				var nsKern = new NSString("NSKern");
				var spacing = NSObject.FromObject(letterSpacing * 10);
				var range = new NSRange(0, text.Length);
				attributedString.AddAttribute(nsKern, spacing, range);
				Control.AttributedText = attributedString;
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
