using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class BarcodeFunc
	{
		public static Barcode2SerialResult Barcode2Serial(string barcode)
		{
			if (string.IsNullOrEmpty(barcode))
			{
				return new Barcode2SerialResult { Result = Barcode2SerialResultType.Empty };
			}

			var sbarcode = (barcode ?? "").Replace("SN", "").Trim();
			if (Int32.TryParse(sbarcode, out var outval))
			{
				return new Barcode2SerialResult
				{
					Result = Barcode2SerialResultType.Valid,
					Serial = outval,
				};
			}
			else
			{
				return new Barcode2SerialResult { Result = Barcode2SerialResultType.Invalid };
			}
		}

		public class Barcode2SerialResult
		{
			public Barcode2SerialResultType Result { get; set; }
			public int Serial { get; set; }
		}

		public enum Barcode2SerialResultType
		{
			Empty,
			Invalid,
			Valid,
		}
	}
}
