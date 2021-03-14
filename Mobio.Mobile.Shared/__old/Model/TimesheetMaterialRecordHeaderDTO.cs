using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneBuilder.Mobile;

namespace OneBuilder.Model
{
	public class TimesheetMaterialRecordHeaderDTO
	{
		public Guid RowId { get; set; }
		public Guid? LabourRowId { get; set; }
		public String LabourName { get; set; }
		public Guid? CreatedBy { get; set; }
		public String CreatedByName { get; set; }
		public Guid? InspectionRowId { get; set; }
		public String InspectionName { get; set; }
		public Guid? TimesheetRowId { get; set; }
		public String TimesheetName { get; set; }
		public DateTime? TimesheetStartDate { get; set; }
		public TimesheetMaterialRecordType RecordType { get; set; }
		public DateTime? RecordDate { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Int32 HeaderNumber { get; set; }

		public string RecordDateString => RecordDate == null ? "" : RecordDate.Value.ToString("ddd, MMM dd \\'yy");
		public string RecordTypeString => TimesheetMaterialRecordDTO.TimesheetMaterialRecordType2Name(RecordType);
		public string TimesheetFullName => TimesheetRowId == null ? "" : TimesheetName + " (" + TimesheetStartDate.FormatShort() + ")";
	}

}
