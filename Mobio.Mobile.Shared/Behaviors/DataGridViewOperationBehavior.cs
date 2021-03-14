using PropertyChanged;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using DevExpress.XamarinForms.DataGrid;

namespace OneBuilder.Mobile.Behaviors
{
	public class DataGridViewOperationBehavior : Behavior<DataGridView>
	{
		public DataGridView associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(DataGridViewOperationBehaviorManager), typeof(DataGridViewOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(DataGridView bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(DataGridView bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as DataGridViewOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				((DataGridViewOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public DataGridViewOperationBehaviorManager Manager
		{
			get
			{
				return (DataGridViewOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class DataGridViewOperationBehaviorManager
	{
		public DataGridViewOperationBehavior Behavior { get; set; }
		public DataGridView Control => (Behavior.associatedObject as DataGridView);
	}
}
