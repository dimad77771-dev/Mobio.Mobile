using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public partial class MainPage2
	{
		public MainPage2()
		{
			InitializeComponent();

			Telerik.XamarinForms.Input.RadButton a;
			Telerik.XamarinForms.Primitives.RadBorder b;
			LabelEx b1;
			RadListPicker b6;
			//b.Padding
			//a.VerticalContentAlignment
			//b1.Margin
			//a.TextColor
			//a.VerticalOptions
			//b.BackgroundColor
			//Grid.GetRowSpan


			var sitems = new List<SerialPickerItem>();
			sitems.Add(new SerialPickerItem { Name = "Polyforce Adhesive, 0.375 L #6142", FullName = "Polyforce Adhesive, 0.375 L #6142" });
			sitems.Add(new SerialPickerItem { Name = "PolyFlex -J, 1.5 L #2142", FullName = "PolyFlex -J, 1.5 L #2142" });
			sitems.Add(new SerialPickerItem { Name = "Polyforce Adhesive, 0.375 L #14142", FullName = "Polyforce Adhesive, 0.375 L #14142" });
			serialPicker.ItemsSource = sitems;
			//serialPicker.WidthRequest
			//serialPicker.ItemTemplate
			//b1.TextColor
			//a.Padding
			//b1.Padd
			//b1.VerticalTextAlignment
			//b1.BackgroundColor
			//Grid.ColumnSpanProperty
			//b.BorderColor;
			//a.FontSize = 20;
			//a.FontAttributes
			//a.BackgroundColor
			//a.HorizontalContentAlignment
			//a.Padding
		}

		public class SerialPickerItem
		{
			public string Name { get; set; }
			public string FullName { get; set; }
		}
	}
}
