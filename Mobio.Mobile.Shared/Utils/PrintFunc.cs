#if __ANDROID__
	using Android.Content;
	using Android.OS;
	using Android.Print;
	using Java.IO;
#elif __IOS__
	using UIKit;
	using Foundation;
#endif
using HtmlAgilityPack;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;
using System.Diagnostics;

namespace OneBuilder.Mobile
{
#if __ANDROID__
	public static class PrintFunc
	{
		public static void Print(byte[] content, string fileName)
		{
			//Android print code goes here
			Stream inputStream = new MemoryStream(content);
			if (inputStream.CanSeek)
				//Reset the position of PDF document stream to be printed
				inputStream.Position = 0;
			//Create a new file in the Personal folder with the given name
			string createdFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
			//Save the stream to the created file
			using (var dest = System.IO.File.OpenWrite(createdFilePath))
				inputStream.CopyTo(dest);
			string filePath = createdFilePath;
			var printManager = (PrintManager)Droid.MainActivity.Instance.GetSystemService(Context.PrintService);
			PrintDocumentAdapter pda = new CustomPrintDocumentAdapter(filePath);
			//Print with null PrintAttributes
			printManager.Print(fileName, pda, null);
		}
	}

	internal class CustomPrintDocumentAdapter : PrintDocumentAdapter
	{
		private string filePath;
		public CustomPrintDocumentAdapter(string filePath)
		{
			this.filePath = filePath;
		}
		public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
		{
			if (cancellationSignal.IsCanceled)
			{
				callback.OnLayoutCancelled();
				return;
			}
			callback.OnLayoutFinished(new PrintDocumentInfo.Builder(filePath)
			.SetContentType(PrintContentType.Document)
			.Build(), true);
		}

		public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
		{
			try
			{
				using (InputStream input = new FileInputStream(filePath))
				{
					using (OutputStream output = new FileOutputStream(destination.FileDescriptor))
					{
						var buf = new byte[1024];
						int bytesRead;
						while ((bytesRead = input.Read(buf)) > 0)
						{
							output.Write(buf, 0, bytesRead);
						}
					}
				}
				callback.OnWriteFinished(new[] { PageRange.AllPages });
			}
			catch (Exception exception)
			{
				throw new AggregateException(exception);
			}
		}
	}
#endif


#if __IOS__
	public static class PrintFunc
	{
		public static void Print(byte[] content, string fileName)
		{
			var folder = U.GetLocalFilesDir();
			var fullname = Path.Combine(folder, fileName);
			File.WriteAllBytes(fullname, content);
			var url = NSUrl.FromFilename(fullname);

			if (UIPrintInteractionController.CanPrint(url))
			{
				var printInfo = UIPrintInfo.PrintInfo;
				var printer = UIPrintInteractionController.SharedPrintController;
				printInfo.Duplex = UIPrintInfoDuplex.LongEdge;
				printInfo.OutputType = UIPrintInfoOutputType.General;
				printer.PrintInfo = printInfo;
				printer.PrintingItem = url;
				printer.ShowsPageRange = false;
				printer.Present(true, (controller, completed, error) =>
				{
					Debug.WriteLine(completed ? "Printing completed" : $"Printing did not complete : {controller} {error}");
				});
			}
		}
	}
#endif
}
