using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform;

namespace OneBuilder.Mobile
{
	public class BoxView2 : View
	{
		public static readonly BindableProperty ColorProperty = BindableProperty.Create("Color", typeof(Color), typeof(BoxView), Color.Default);

		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		[Obsolete("Use OnMeasure")]
		protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
		{
			return new SizeRequest(new Size(40, 40));
		}

		public bool IsTouch { get; set; }
		public event EventHandler TouchChanged;
		public void TouchChangedInvoke(bool isTouch)
		{
			IsTouch = isTouch;
			TouchChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}