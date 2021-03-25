using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using OneBuilder.Mobile;
using Telerik.XamarinForms.Input;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(RadEntryEx), typeof(Mobio.Mobile.iOS.RadEntryExRenderer))]
namespace Mobio.Mobile.iOS
{
    public class RadEntryExRenderer : Telerik.XamarinForms.InputRenderer.iOS.EntryRenderer
    {
        public RadEntryExRenderer() : base()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<RadEntry> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				if (e.NewElement != null)
				{
					if (e.NewElement.IsPassword)
					{
						Control.TextContentType = UITextContentType.NewPassword;
						//Control.TextContentType = new NSString("");
						//Control.TextContentType = UITextContentType.OneTimeCode;

						//Control.SecureTextEntry = false;
						//var aa = Control.SecureTextEntry;
					}
				}
				
			}
		}
    }
}
