using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class GetAllTimesheetMaterialRecordsQuery
	{
		public string FindText { get; set; }
		public string SerialNum { get; set; }
		public DateTime? RecordDateFrom { get; set; }
		public DateTime? RecordDateTo { get; set; }
	}
}
