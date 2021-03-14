using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Behaviors
{
	public class FocusBehavior : Behavior<VisualElement>
	{
		private VisualElement associatedObject;

		public static readonly BindableProperty FocusToProperty =
			BindableProperty.Create(nameof(FocusTo), typeof(bool), typeof(FocusBehavior), false, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(VisualElement bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(VisualElement bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as FocusBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				var attachBehavior = (bool)newValue;
				if (attachBehavior)
				{
					var view = behavior.associatedObject;
					view.Focus();
					behavior.FocusTo = false;
				}
			}
		}

		public bool FocusTo
		{
			get
			{
				return (bool)GetValue(FocusToProperty);
			}
			set
			{
				SetValue(FocusToProperty, value);
			}
		}
	}
}
