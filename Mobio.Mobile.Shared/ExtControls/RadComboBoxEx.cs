using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Telerik.XamarinForms.Input;

namespace OneBuilder.Mobile
{
	public class RadComboBoxEx : RadComboBox
	{
		public RadComboBoxEx()
		{
			this.PropertyChanged += RadComboBoxEx_PropertyChanged;
		}


		private void RadComboBoxEx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(ValueMember))
			{
				if (e.PropertyName == nameof(SelectedItem))
				{
					object value = null;
					if (SelectedItem != null)
					{
						var prop = SelectedItem.GetType().GetProperty(ValueMember);
						if (prop != null)
						{
							value = prop.GetValue(SelectedItem);
						}
					}
					this.Value = value;
				}
			}
		}

		public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(object), typeof(RadComboBoxEx), null, propertyChanged: ValuePropertyChanged);
		public static readonly BindableProperty ValueMemberProperty = BindableProperty.Create("ValueMember", typeof(string), typeof(RadComboBoxEx), "");

		public object Value
		{
			get
			{
				return (object)this.GetValue(RadComboBoxEx.ValueProperty);
			}
			set
			{
				this.SetValue(RadComboBoxEx.ValueProperty, (object)value);
			}
		}

		public string ValueMember
		{
			get
			{
				return (string)this.GetValue(RadComboBoxEx.ValueMemberProperty);
			}
			set
			{
				this.SetValue(RadComboBoxEx.ValueMemberProperty, (object)value);
			}
		}

		static void ValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var comboBox = (RadComboBoxEx)bindable;

			if (!string.IsNullOrEmpty(comboBox.ValueMember))
			{
				var itemsSource = comboBox.ItemsSource;

				if (itemsSource != null)
				{
					object find_item = null;
					foreach (var item in itemsSource)
					{
						var prop = item.GetType().GetProperty(comboBox.ValueMember);
						var value = prop.GetValue(item);
						if (Object.Equals(value, newValue))
						{
							find_item = item;
							break;
						}
					}

					if (find_item == null)
					{
						comboBox.SelectedItem = null;
					}
					else
					{
						comboBox.SelectedItem = find_item;
					}
				}
			}
		}


	}
}
