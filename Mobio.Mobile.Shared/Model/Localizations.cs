using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace OneBuilder.Model
{
	public class Localizations
	{
		public int pk { get; set; }
		public string ResourceId { get; set; }
		public string Value { get; set; }
		public string LocaleId { get; set; }
		public string ResourceSet { get; set; }
		public string Type { get; set; }
		public string BinFile { get; set; }
		public string TextFile { get; set; }
		public string Filename { get; set; }
		public string Comment { get; set; }
		public int? ValueType { get; set; }
		public DateTime? Updated { get; set; }
	}
}
