using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using OneBuilder.Mobile;
using Telerik.XamarinForms.Input;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(RadButtonEx), typeof(Mobio.Mobile.iOS.CustomRadButtonExRenderer))]
namespace Mobio.Mobile.iOS
{
    public class CustomRadButtonExRenderer : Telerik.XamarinForms.InputRenderer.iOS.ButtonRenderer
    {
        public CustomRadButtonExRenderer() : base()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Control.SetTitleColor(UIColor.Gray, UIControlState.Disabled);
				//Control.SetTitleColor(UIColor.Red, UIControlState.Normal);

				Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
				Control.TitleLabel.TextAlignment = UITextAlignment.Center;
			}
        }
    }
}
