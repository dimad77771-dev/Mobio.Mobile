using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public class RadDataFormEx : RadDataForm
	{
		public static readonly BindableProperty FocusTextEditorProperty = BindableProperty.Create(
			nameof(FocusTextEditor), typeof(Int32), typeof(RadDataFormEx), -1, 
			propertyChanged: OnFocusTextEditorChanged, 
			defaultBindingMode: BindingMode.TwoWay);

		public Int32 FocusTextEditor
		{
			get
			{
				return (Int32)this.GetValue(RadDataFormEx.FocusTextEditorProperty);
			}
			set
			{
				this.SetValue(RadDataFormEx.FocusTextEditorProperty, (object)value);
			}
		}

		static void OnFocusTextEditorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var bobj = (RadDataFormEx)vw;
			bobj.FocusTextEditor = -1;
		}
	}
}
