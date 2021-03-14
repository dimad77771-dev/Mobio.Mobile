using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public class RadListViewEx : RadListView
	{
		public static readonly BindableProperty AnimationInsertItemProperty = BindableProperty.Create("AnimationInsertItem", typeof(Int32), typeof(RadListViewEx), -1);

		public Int32 AnimationInsertItem
		{
			get
			{
				return (Int32)this.GetValue(RadListViewEx.AnimationInsertItemProperty);
			}
			set
			{
				this.SetValue(RadListViewEx.AnimationInsertItemProperty, (object)value);
			}
		}
	}
}
