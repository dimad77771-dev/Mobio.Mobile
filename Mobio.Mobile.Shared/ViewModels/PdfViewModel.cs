using OneBuilder.Model;
using OneBuilder.WebServices;
using PropertyChanged;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OneBuilder.Mobile.Constants;
using System.Windows.Input;
using ZXing;
using Telerik.XamarinForms.PdfViewer;
using OneBuilder.Mobile.Behaviors;

namespace OneBuilder.Mobile.ViewModels
{
	public class PdfViewModel : PageViewModel
	{
		public byte[] PdfBytes;
		public string PdfFileName;
		public DocumentSource PdfSource { get; set; }
		public Double ZoomLevel { get; set; }
		public RadPdfViewerOperationBehaviorManager PdfManager { get; set; } = new RadPdfViewerOperationBehaviorManager();
		public Command PrintCommand => CommandFunc.CreateAsync(Print);


		public override async Task Init()
		{
			
		}

		public override async Task InitAfterBinding()
		{
			U.RequestMainThread(async () =>
			{
				await Task.Yield();
				await Task.Delay(1000);
				PdfManager.Control.ZoomToLevel(1.5f);
			});
		}

		public async Task Print()
		{
			PrintFunc.Print(PdfBytes, PdfFileName);
		}
	}
}

