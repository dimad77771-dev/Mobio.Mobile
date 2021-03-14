using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using OneBuilder.Mobile.ViewModels;
using OneBuilder.WebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class PrintReportFunc
	{
		static bool USE_PDF_VIEWER = true;
		public static async Task PrintCore(byte[] content, string fileName, string title)
		{
			if (!USE_PDF_VIEWER)
			{
				PrintFunc.Print(content, fileName);
			}
			else
			{
				var pdfViewModel = new PdfViewModel();
				pdfViewModel.HeaderTitle = U.TabletMode ? title : "";
				pdfViewModel.PdfBytes = content;
				pdfViewModel.PdfSource = content;
				pdfViewModel.PdfFileName = fileName;
				pdfViewModel.ZoomLevel = 0.2f;
				await NavFunc.NavigateToAsync(pdfViewModel);
			}
		}


		public async static Task PrintOrder(Guid headerRowId, int headerNumber)
		{
			UIFunc.ShowLoading();
			var bytes = await WebServiceFunc.GenerateOrderReport(headerRowId);
			UIFunc.HideLoading();
			if (bytes == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return;
			}

			await PrintCore(bytes, "" + headerNumber + ".pdf", "Order #" + headerNumber);
		}

	}

}
