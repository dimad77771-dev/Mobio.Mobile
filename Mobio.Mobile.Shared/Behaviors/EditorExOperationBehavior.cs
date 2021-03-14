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
	public class EditorExOperationBehavior : Behavior<EditorEx>
	{
		public EditorEx associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(EditorExOperationBehaviorManager), typeof(EditorExOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(EditorEx bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(EditorEx bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as EditorExOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				((EditorExOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public EditorExOperationBehaviorManager Manager
		{
			get
			{
				return (EditorExOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class EditorExOperationBehaviorManager
	{
		public EditorExOperationBehavior Behavior { get; set; }
		public EditorEx Control => (Behavior.associatedObject as EditorEx);
	}
}
