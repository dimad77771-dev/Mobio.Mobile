using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Drawing;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Droid.Renderers;

[assembly: ExportRenderer(typeof(SearchBarEx), typeof(SearchBarExRenderer))]
namespace OneBuilder.Mobile.Droid.Renderers
{
	class SearchBarExRenderer : SearchBarRenderer
	{
		public SearchBarExRenderer(Android.Content.Context context) : base(context)
		{
		}


		protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
		{
			base.OnElementChanged(e);


			if (e.NewElement != null)
			{
				AddToolbarHide();
			}
		}



		void AddToolbarHide()
		{
			//var toolbar = new UIToolbar (new RectangleF(0.0f, 0.0f, 200f, 44.0f));
			////toolbar.TintColor = UIColor.White;
			////toolbar.BarStyle = UIBarStyle.Black;
			//toolbar.Translucent = true;

			//var toolbarItems = new List<UIBarButtonItem>();

			//toolbarItems.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));
			//toolbarItems.Add(new UIBarButtonItem("Hide", UIBarButtonItemStyle.Plain, (s,e) =>
			//	{
			//		Control.ResignFirstResponder();
			//	}));
			//toolbar.Items = toolbarItems.ToArray();
			//Control.InputAccessoryView = toolbar;
		}



		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
		}


		SearchBarEx ElementEx
		{
			get
			{
				return (SearchBarEx)Element;
			}
		}
	}
}
