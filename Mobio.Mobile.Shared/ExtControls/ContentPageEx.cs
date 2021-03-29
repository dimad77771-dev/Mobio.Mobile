using FormsControls.Base;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public class ContentPageEx : AnimationPage
	{
		public ContentPageEx() : base()
		{
			PageAnimation = new SlidePageAnimation { Duration = 200, Subtype = AnimationSubtype.FromLeft };
			//this.Visual = VisualMarker.Material;
		}


		protected override bool OnBackButtonPressed()
		{
			var ret = base.OnBackButtonPressed();
			if (ret) return true;

			//if (ModalViewControllerFunc.HasOpenModalWindow())
			//{
			//	var currentModalWindow = ModalViewControllerFunc.GetLastModalWindow();
			//	SysFunc.RequestMainThread(async () =>
			//	{
			//		await (currentModalWindow as IModalViewServiceSupport2).OpenerModalService.ModalWindowCloseWithReturn(false);
			//	});
			//	return true;
			//}

			SysFunc.RequestMainThread(async () =>
			{
				await NavFunc.Pop();
			});
			return true;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			var viewmode = BindingContext as PageViewModel;
			if (viewmode != null)
			{
				await viewmode.OnAppearing();
				await viewmode.OnAppearingEx(this);
			}
		}

		protected override async void OnDisappearing()
		{
			var viewmode = BindingContext as PageViewModel;
			if (viewmode != null)
			{
				await viewmode.OnDisappearing();
			}

			base.OnDisappearing();
		}


	}
}
