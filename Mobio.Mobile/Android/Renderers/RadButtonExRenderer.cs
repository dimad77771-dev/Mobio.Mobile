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

[assembly: ExportRenderer(typeof(RadButtonEx), typeof(RadButtonExRenderer))]
namespace OneBuilder.Mobile.Droid.Renderers
{
	class RadButtonExRenderer : Telerik.XamarinForms.InputRenderer.Android.ButtonRenderer
	{
		public RadButtonExRenderer(Android.Content.Context context) : base(context)
		{
		}


		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				//Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
			}
		}



		RadButtonEx ElementEx
		{
			get
			{
				return (RadButtonEx)Element;
			}
		}
	}
}
