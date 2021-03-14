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
	public class RadListViewOperationBehavior : Behavior<RadListView>
	{
		public RadListView associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(RadListViewOperationBehaviorManager), typeof(RadListViewOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(RadListView bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
			{
				BindingContext = associatedObject.BindingContext;
			};
		}

		protected override void OnDetachingFrom(RadListView bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as RadListViewOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				System.Diagnostics.Debug.WriteLine("UUUUU");
				((RadListViewOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public RadListViewOperationBehaviorManager Manager
		{
			get
			{
				return (RadListViewOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class RadListViewOperationBehaviorManager
	{
		public RadListViewOperationBehavior Behavior { get; set; }
		public RadListView Control => (Behavior.associatedObject as RadListView);

		public void CollapseAll()
		{
			if (Control != null)
			{
				var dataView = Control.GetDataView();
				if (dataView != null)
				{
					var groups = dataView.GetGroups().ToArray();
					dataView.CollapseAll();
				}
			}
		}

		public void EndItemSwipe(bool isAnimated = false)
		{
			if (Control != null)
			{
				Control.EndItemSwipe(isAnimated);
			}
		}

	}
}
