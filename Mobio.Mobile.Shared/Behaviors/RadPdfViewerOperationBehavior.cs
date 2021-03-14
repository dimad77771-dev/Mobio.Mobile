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
	public class RadPdfViewerOperationBehavior : Behavior<RadPdfViewer>
	{
		public RadPdfViewer associatedObject;

		public static readonly BindableProperty ManagerProperty =
			BindableProperty.Create(nameof(Manager), typeof(RadPdfViewerOperationBehaviorManager), typeof(RadPdfViewerOperationBehavior), defaultValue: null, propertyChanged: OnAttachBehaviorChanged, defaultBindingMode: BindingMode.TwoWay);

		protected override void OnAttachedTo(RadPdfViewer bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
				BindingContext = associatedObject.BindingContext;
		}

		protected override void OnDetachingFrom(RadPdfViewer bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnAttachBehaviorChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as RadPdfViewerOperationBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				((RadPdfViewerOperationBehaviorManager)(newValue)).Behavior = behavior;
			}
		}

		public RadPdfViewerOperationBehaviorManager Manager
		{
			get
			{
				return (RadPdfViewerOperationBehaviorManager)GetValue(ManagerProperty);
			}
			set
			{
				SetValue(ManagerProperty, value);
			}
		}
	}

	public class RadPdfViewerOperationBehaviorManager
	{
		public RadPdfViewerOperationBehavior Behavior { get; set; }
		public RadPdfViewer Control => (Behavior.associatedObject as RadPdfViewer);

		public void CollapseAll(double zoomLevel)
		{
			if (Control != null)
			{
				Control.ZoomToLevel(zoomLevel);
			}
		}


	}
}
