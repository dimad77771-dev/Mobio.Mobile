using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class GetAllTimesheetMaterialRecordsResult
	{
		public TimesheetMaterialRecordHeaderDTO[] TimesheetMaterialRecordHeaders { get; set; }
		public TimesheetMaterialRecordDTO[] TimesheetMaterialRecords { get; set; }
	}
}
