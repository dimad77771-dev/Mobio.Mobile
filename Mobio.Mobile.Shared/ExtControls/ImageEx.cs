using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public class ImageEx : Image
	{
		public static readonly BindableProperty Image2Property = BindableProperty.Create("Image2", typeof(ImageSourceEx), typeof(ImageEx), null);
		public static readonly BindableProperty IsBackgroundImageProperty = BindableProperty.Create("IsBackgroundImage", typeof(Boolean), typeof(ImageEx), false);
		public static readonly BindableProperty ClipToSizeProperty = BindableProperty.Create("ClipToSize", typeof(Boolean), typeof(ImageEx), false);
		public static readonly BindableProperty HiddenProperty = BindableProperty.Create("Hidden", typeof(Boolean), typeof(ImageEx), false);
		public static readonly BindableProperty ShowWhenTochOnlyProperty = BindableProperty.Create("ShowWhenTochOnly", typeof(Boolean), typeof(ImageEx), false);
		public static readonly BindableProperty Image2PressedProperty = BindableProperty.Create("Image2Pressed", typeof(ImageSourceEx), typeof(ImageEx), null);
		public static readonly BindableProperty IsVisible2Property = BindableProperty.Create("IsVisible2", typeof(Boolean?), typeof(ImageEx), null); 
		

		public ImageSourceEx Image2
		{
			get
			{
				return (ImageSourceEx)this.GetValue(ImageEx.Image2Property);
			}
			set
			{
				this.SetValue(ImageEx.Image2Property, (object)value);
			}
		}

		public Boolean IsBackgroundImage
		{
			get
			{
				return (Boolean)this.GetValue(ImageEx.IsBackgroundImageProperty);
			}
			set
			{
				this.SetValue(ImageEx.IsBackgroundImageProperty, (object)value);
			}
		}

		public Boolean ShowWhenTochOnly
		{
			get
			{
				return (Boolean)this.GetValue(ImageEx.ShowWhenTochOnlyProperty);
			}
			set
			{
				this.SetValue(ImageEx.ShowWhenTochOnlyProperty, (object)value);
			}
		}

		public ImageSourceEx BackgroundImage2
		{
			set
			{
				IsBackgroundImage = true;
				Image2 = value;
			}
		}


		public Boolean ClipToSize
		{
			get
			{
				return (Boolean)this.GetValue(ImageEx.ClipToSizeProperty);
			}
			set
			{
				this.SetValue(ImageEx.ClipToSizeProperty, (object)value);
			}
		}

		public Boolean Hidden
		{
			get
			{
				return (Boolean)this.GetValue(ImageEx.HiddenProperty);
			}
			set
			{
				this.SetValue(ImageEx.HiddenProperty, (object)value);
			}
		}

		public ImageSourceEx Image2Pressed
		{
			get
			{
				return (ImageSourceEx)this.GetValue(ImageEx.Image2PressedProperty);
			}
			set
			{
				this.SetValue(ImageEx.Image2PressedProperty, (object)value);
			}
		}


		public bool? IsVisible2
		{
			get
			{
				return (bool?)this.GetValue(ImageEx.IsVisible2Property);
			}
			set
			{
				this.SetValue(ImageEx.IsVisible2Property, (object)value);
			}
		}


		//public void OnIsVisible2Changed(Boolean? oldValue, Boolean? newValue)
		//{
		//	if (newValue != null)
		//	{
		//		this.IsVisible = (bool)newValue;
		//	}
		//}

		public ImageEx()
		{
			this.Aspect = Aspect.Fill;
		}


	}
}
