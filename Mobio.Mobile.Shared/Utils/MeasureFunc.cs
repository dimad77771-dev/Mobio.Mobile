using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Linq.Expressions;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using OneBuilder.Mobile.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class MeasureFunc
	{
		private static PropertyInfo platformProperty;
		public static SizeRequest Measure(VisualElement view, double widthConstraint, double heightConstraint, MeasureFlags flags = MeasureFlags.IncludeMargins)
		{
			var req = view.Measure(double.PositiveInfinity, double.PositiveInfinity, MeasureFlags.IncludeMargins);
			return req;
		}


		struct CachLabelMeasureKey
		{
			public Font font;
			public double fontSize;
			public string fontFamily;
			public LineBreakMode lineBreakMode;
			public String text;
			public Thickness margin;
			public double widthConstraint;
			public double heightConstraint;
			public MeasureFlags flags;
		}
		static Dictionary<CachLabelMeasureKey, SizeRequest> CachLabelMeasure = new Dictionary<CachLabelMeasureKey, SizeRequest>();

		public static SizeRequest MeasureLabel(Label label, string text, double widthConstraint = double.PositiveInfinity, double heightConstraint = double.PositiveInfinity, MeasureFlags flags = MeasureFlags.IncludeMargins)
		{
			var key = new CachLabelMeasureKey
			{
				font = label.Font,
				fontSize = label.FontSize,
				fontFamily = label.FontFamily,
				lineBreakMode = label.LineBreakMode,
				text = text,
				margin = label.Margin,
				widthConstraint = widthConstraint,
				heightConstraint = heightConstraint,
				flags = flags,
			};
			SizeRequest size;
			if (!CachLabelMeasure.TryGetValue(key, out size))
			{
				var extext = label.Text;
				label.Text = text;
				size = Measure(label, widthConstraint, heightConstraint, flags);
				label.Text = extext;

				CachLabelMeasure.Add(key, size);
			}

			return size;
		}


	}

}