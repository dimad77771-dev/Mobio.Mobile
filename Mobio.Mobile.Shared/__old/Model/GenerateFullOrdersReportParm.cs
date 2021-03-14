using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class GenerateFullOrdersReportParm
	{
		public Guid[] TimesheetMaterialRecordHeaderRowIds { get; set; }
		public Int32? SerialNum { get; set; }
		public String SearchText { get; set; }
		public String SearchColumn { get; set; }
		public DateTime? SearchDateFrom { get; set; }
		public DateTime? SearchDateTo { get; set; }
	}
}
