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
using Telerik.XamarinForms.PdfViewer;

namespace OneBuilder.Mobile.Behaviors
{
	public class EntryExOperationBehavior : Behavior<EntryEx>
	{
		public EntryEx associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(EntryExOperationBehaviorManager), typeof(EntryExOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(EntryEx bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
			{
				BindingContext = associatedObject.BindingContext;
			};
		}

		protected override void OnDetachingFrom(EntryEx bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as EntryExOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				((EntryExOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public EntryExOperationBehaviorManager Manager
		{
			get
			{
				return (EntryExOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class EntryExOperationBehaviorManager
	{
		public EntryExOperationBehavior Behavior { get; set; }
		public EntryEx Control => (Behavior.associatedObject as EntryEx);
	}
}
