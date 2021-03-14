using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Telerik.XamarinForms.DataControls;

namespace OneBuilder.Mobile.Behaviors
{
	public class RadListViewEndItemSwipeBehavior : Behavior<RadListView>
	{
		private RadListView associatedObject;

		public static readonly BindableProperty EndItemSwipeProperty =
			BindableProperty.Create(nameof(EndItemSwipe), typeof(bool), typeof(RadListViewEndItemSwipeBehavior), false, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(RadListView bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(RadListView bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as RadListViewEndItemSwipeBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				var attachBehavior = (bool)newValue;
				if (attachBehavior)
				{
					var view = behavior.associatedObject;
					view.EndItemSwipe();
					behavior.EndItemSwipe = false;
				}
			}
		}

		public bool EndItemSwipe
		{
			get
			{
				return (bool)GetValue(EndItemSwipeProperty);
			}
			set
			{
				SetValue(EndItemSwipeProperty, value);
			}
		}
	}
}
