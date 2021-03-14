using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public class ButtonEx : Button
	{
		public static readonly BindableProperty TitleShadowColorProperty = BindableProperty.Create("TitleShadowColor", typeof(Color), typeof(ButtonEx), Color.Default);
		public static readonly BindableProperty Image2Property = BindableProperty.Create("Image2", typeof(ImageSourceEx), typeof(ButtonEx), null);
		public static readonly BindableProperty Image2DownProperty = BindableProperty.Create("Image2Down", typeof(ImageSourceEx), typeof(ButtonEx), null);


		public event EventHandler LongClicked;

		public ImageSourceEx Image2
		{
			get
			{
				return (ImageSourceEx)this.GetValue(ButtonEx.Image2Property);
			}
			set
			{
				this.SetValue(ButtonEx.Image2Property, (object)value);
			}
		}

		public ImageSourceEx Image2Down
		{
			get
			{
				return (ImageSourceEx)this.GetValue(ButtonEx.Image2DownProperty);
			}
			set
			{
				this.SetValue(ButtonEx.Image2DownProperty, (object)value);
			}
		}

		public Color TitleShadowColor
		{
			get
			{
				return (Color)this.GetValue(ButtonEx.TitleShadowColorProperty);
			}
			set
			{
				this.SetValue(ButtonEx.TitleShadowColorProperty, (object)value);
			}
		}
		

		public ButtonEx()
		{
		}


		public void SetClicked(Func<Task> action)
		{
			this.Clicked += async (s,e) => await action();
		}
		public void SetClicked2(Action action)
		{
			this.Clicked += (s,e) => action();
		}


		public void SendLongClicked()
		{
			if (LongClicked != null)
			{
				LongClicked(this, EventArgs.Empty);
			}
		}
	}
}
