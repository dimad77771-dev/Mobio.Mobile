using PropertyChanged;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.DataControls;
using Xamarin.Forms;
using Telerik.XamarinForms.Input;

namespace OneBuilder.Mobile.Behaviors
{
	public class RadListPickerOperationBehavior : Behavior<RadListPicker>
	{
		public RadListPicker associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(RadListPickerOperationBehaviorManager), typeof(RadListPickerOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(RadListPicker bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(RadListPicker bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as RadListPickerOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				((RadListPickerOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public RadListPickerOperationBehaviorManager Manager
		{
			get
			{
				return (RadListPickerOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class RadListPickerOperationBehaviorManager
	{
		public RadListPickerOperationBehavior Behavior { get; set; }
		public RadListPicker Control => (Behavior.associatedObject as RadListPicker);

		public void ToggleCommand()
		{
			if (Control != null)
			{
				Control.ToggleCommand.Execute(null);
			}
		}
	}
}
