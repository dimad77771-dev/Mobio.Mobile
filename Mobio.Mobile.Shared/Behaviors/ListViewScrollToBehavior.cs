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
	public class ListViewScrollToBehavior : Behavior<ListView>
	{
		private ListView associatedObject;

		public static readonly BindableProperty ScrollToProperty =
			BindableProperty.Create(nameof(ScrollTo), typeof(object), typeof(ListViewScrollToBehavior), null, propertyChanged: OnAttachBehaviorChanged);

		protected override void OnAttachedTo(ListView bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(ListView bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as ListViewScrollToBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				var attachBehavior = (object)newValue;
				if (attachBehavior != null)
				{
					var listview = behavior.associatedObject;
					listview.ScrollTo(attachBehavior, ScrollToPosition.MakeVisible, animated: true);
				}
			}
		}

		public object ScrollTo
		{
			get
			{
				return (object)GetValue(ScrollToProperty);
			}
			set
			{
				SetValue(ScrollToProperty, value);
			}
		}
	}
}
