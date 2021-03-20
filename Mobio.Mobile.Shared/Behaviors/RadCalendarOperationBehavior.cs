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

namespace OneBuilder.Mobile.Behaviors
{
	public class RadCalendarOperationBehavior : Behavior<RadCalendarEx>
	{
		public RadCalendarEx associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(RadCalendarOperationBehaviorManager), typeof(RadCalendarOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(RadCalendarEx bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
			{
				BindingContext = associatedObject.BindingContext;
			};
		}

		protected override void OnDetachingFrom(RadCalendarEx bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as RadCalendarOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				System.Diagnostics.Debug.WriteLine("UUUUU");
				((RadCalendarOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public RadCalendarOperationBehaviorManager Manager
		{
			get
			{
				return (RadCalendarOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class RadCalendarOperationBehaviorManager
	{
		public RadCalendarOperationBehavior Behavior { get; set; }
		public RadCalendarEx Control => (Behavior.associatedObject as RadCalendarEx);
	}
}
