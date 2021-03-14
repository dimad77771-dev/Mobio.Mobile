using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace OneBuilder.Mobile
{
	public class ZXingDefaultOverlayEx : ZXingDefaultOverlay
	{
		private Label TopTextLabel;
		private Label BottomTextLabel;

		public ZXingDefaultOverlayEx() : base()
		{
			if (this.RowDefinitions.Count != 3) throw new ArgumentException();
			var labels = this.Children.OfType<Label>().ToArray();
			if (labels.Length != 2) throw new ArgumentException();

			TopTextLabel = labels.Single(q => Grid.GetRow(q) == 0 && Grid.GetColumn(q) == 0);
			BottomTextLabel = labels.Single(q => Grid.GetRow(q) == 2 && Grid.GetColumn(q) == 0);

			UpdateTopTextColor();
			UpdateBottomTextColor();
		}

		public static readonly BindableProperty TopTextColorProperty = BindableProperty.Create(nameof(TopTextColor), typeof(Color), typeof(ZXingDefaultOverlayEx), Color.White, propertyChanged: TopTextColorPropertyChanged);
		public static readonly BindableProperty BottomTextColorProperty = BindableProperty.Create(nameof(BottomTextColor), typeof(Color), typeof(ZXingDefaultOverlayEx), Color.White, propertyChanged: BottomTextColorPropertyChanged);

		static void TopTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = (ZXingDefaultOverlayEx)bindable;
			if (newValue != null)
			{
				element.UpdateTopTextColor();
			}
		}

		public Color TopTextColor
		{
			get
			{
				return (Color)this.GetValue(ZXingDefaultOverlayEx.TopTextColorProperty);
			}
			set
			{
				this.SetValue(ZXingDefaultOverlayEx.TopTextColorProperty, (object)value);
			}
		}

		static void BottomTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = (ZXingDefaultOverlayEx)bindable;
			if (newValue != null)
			{
				element.UpdateBottomTextColor();
			}
		}

		public Color BottomTextColor
		{
			get
			{
				return (Color)this.GetValue(ZXingDefaultOverlayEx.BottomTextColorProperty);
			}
			set
			{
				this.SetValue(ZXingDefaultOverlayEx.BottomTextColorProperty, (object)value);
			}
		}



		public void UpdateTopTextColor()
		{
			TopTextLabel.TextColor = TopTextColor;
		}


		public void UpdateBottomTextColor()
		{
			BottomTextLabel.TextColor = BottomTextColor;
		}

	}
}
