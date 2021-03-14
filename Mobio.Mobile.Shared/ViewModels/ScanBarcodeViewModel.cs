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

namespace OneBuilder.Mobile.ViewModels
{
	public class ScanBarcodeViewModel : PageViewModel
	{
		public SerialViewModel ParentView { get; set; }
		public Boolean IsMultiple { get; set; }

		public ICommand FlashCommand => CommandFunc.CreateAsync(Flash);
		public ICommand ManualCodeCommand => CommandFunc.CreateAsync(ManualCode);
		public Boolean ManualCodePopupIsOpen { get; set; }
		public Boolean IsShowManualCodePopup { get; set; } = U.IsDebug;

		public Boolean IsScanning { get; set; }
		public Boolean IsTorchOn { get; set; }
		public Result ScanResult { get; set; }
		public ICommand ScanResultCommand => CommandFunc.CreateAsync(OnScanCode);

		public Boolean IsCommit { get; set; } = false;
		public String ScanCode { get; set; } = "";
		public ICommand ReturnCommand => CommandFunc.CreateAsync(ManualCommit);
		public ICommand ManualCommitCommand => CommandFunc.CreateAsync(ManualCommit);
		public ICommand CloseCommand => CommandFunc.CreateAsync(Close);
		

		private const string DefaultZXingTopText = "Hold your phone up to the barcode";
		public String ZXingTopText { get; set; } = DefaultZXingTopText;
		public Color ZXingTopTextColor { get; set; } = Color.White;
		public String ZXingBottomText { get; set; }
		public Color ZXingBottomTextColor { get; set; } = Color.White;

				


		public override async Task Init()
		{
			HeaderTitle = "Scan barcode";
		}

		public async Task OnScanCode()
		{
			Device.BeginInvokeOnMainThread(async () => {
				await OnScanCodeCore(ScanResult.Text, false);
			});
		}

		public async Task ManualCommit()
		{
			await OnScanCodeCore(ScanCode, true);
		}

		public async Task Close()
		{
			await NavFunc.Pop();
		}


		public async Task OnScanCodeCore(string barcode, bool isManual)
		{
			if (string.IsNullOrEmpty(barcode))
			{
				return;
			}

			IsCommit = true;
			ScanCode = barcode;
			await ParentView.OnScanCodeCore(ScanCode);

			if (!IsMultiple)
			{
				await NavFunc.Pop();
			}
		}

		public async Task Flash()
		{
			IsTorchOn = !IsTorchOn;
		}


		public async Task ManualCode()
		{
			ManualCodePopupIsOpen = true;
		}


		public async override Task OnAppearing()
		{
			IsScanning = true;
		}
		public async override Task OnDisappearing()
		{
			IsScanning = false;
		}



	}
}
