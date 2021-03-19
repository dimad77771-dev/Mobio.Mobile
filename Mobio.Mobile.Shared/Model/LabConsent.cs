using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class LabConsent : ViewModelBase
	{
		public Guid RowId { get; set; }
		public bool? Auth1 { get; set; }
		public bool? Auth2 { get; set; }
		public bool? Auth3 { get; set; }
		public bool? Auth4 { get; set; }
		public bool? Auth5 { get; set; }
		public bool? Auth6 { get; set; }
	}

}
