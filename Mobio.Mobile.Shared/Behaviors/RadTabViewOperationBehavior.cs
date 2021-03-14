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
using Telerik.XamarinForms.Primitives;

namespace OneBuilder.Mobile.Behaviors
{
	public class RadTabViewOperationBehavior : Behavior<RadTabView>
	{
		public RadTabView associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(RadTabViewOperationBehaviorManager), typeof(RadTabViewOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(RadTabView bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(RadTabView bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as RadTabViewOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				((RadTabViewOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public RadTabViewOperationBehaviorManager Manager
		{
			get
			{
				return (RadTabViewOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class RadTabViewOperationBehaviorManager
	{
		public RadTabViewOperationBehavior Behavior { get; set; }
		public RadTabView Control => (Behavior.associatedObject as RadTabView);
	}
}
