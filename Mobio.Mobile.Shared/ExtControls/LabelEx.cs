using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public class LabelEx : Label
	{
		public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(nameof(LetterSpacing), typeof(float), typeof(LabelEx), -1.0f);
		public static readonly BindableProperty AdjustsFontSizeToFitWidthProperty = BindableProperty.Create("AdjustsFontSizeToFitWidth", typeof(Boolean), typeof(LabelEx), false);
		public static readonly BindableProperty Font2Property = BindableProperty.Create("Font2", typeof(FontEx), typeof(LabelEx), default(FontEx), propertyChanged:Font2PropertyChanged);

		public float LetterSpacing
		{
			get
			{
				return (float)this.GetValue(LabelEx.LetterSpacingProperty);
			}
			set
			{
				this.SetValue(LabelEx.LetterSpacingProperty, (object)value);
			}
		}

		public Boolean AdjustsFontSizeToFitWidth
		{
			get
			{
				return (Boolean)this.GetValue(LabelEx.AdjustsFontSizeToFitWidthProperty);
			}
			set
			{
				this.SetValue(LabelEx.AdjustsFontSizeToFitWidthProperty, (object)value);
			}
		}

		static void Font2PropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null)
			{
				((LabelEx)bindable).Font2 = (FontEx)newValue;
			}
		}

		public FontEx Font2
		{
			set
			{
				if (value != null)
				{
					this.Font = value.Font;
				}
			}
		}

		public Font Font9
		{
			set
			{
				this.Font = value;

			}
		}


		protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
		{
			var size = base.OnSizeRequest(widthConstraint, heightConstraint);

			/*
			var prop1 = this.GetType().GetProperty("Platform", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			var prop2 = this.GetType().GetProperty("IsPlatformEnabled", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			var prop3 = this.GetType().GetProperty("RealParent", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			var platform = prop1.GetValue(this);// as Xamarin.Forms.Platform.iOS.Platform;
			var isPlatformEnabled = prop2.GetValue(this);
			//var methods3 = platform.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			//var z11 = string.Join(";", methods3.Select(q => q.Name));
			//var method3 = platform.GetType().GetMethod("Xamarin.Forms.IPlatform.GetNativeSize", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			//var parms = new object[] { this, widthConstraint, heightConstraint };
			//var sz = method3.Invoke(platform, parms);
			//platform.GetNativeSize(
			//platform.Get

			var realParent = prop3.GetValue(this);
			if (realParent != null)
			{
				var platform3 = prop1.GetValue(realParent);
				var realParent4 = prop3.GetValue(realParent);
				var platform4 = prop1.GetValue(realParent4);

				var realParent5 = prop3.GetValue(realParent4);
				var platform5 = prop1.GetValue(realParent5);

			}
			//internal Element RealParent { get; private set; }

			//var p = this.Parent;
			//var p2 = p?.Parent;

			//if (Platform == null || !IsPlatformEnabled)
			//{
			//	return new SizeRequest(new Size(-1, -1));
			//}
			//return Platform.GetNativeSize(this, widthConstraint, heightConstraint);
			*/



			return size;
		}
		

		public LabelEx()
		{
		}
	}
}
