using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneBuilder.Mobile;


namespace OneBuilder.Model
{
	public class TimesheetMaterialRecordDTO
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
		public Guid StockItemRowId { get; set; }
		public Guid? BoxItemRowId { get; set; }
		public Int32? BoxItemSerialNum { get; set; }
		public StockItemType ItemType { get; set; }
		public String StockItemName { get; set; }
		public Int32? StockItemSerialNum { get; set; }
		public TimesheetMaterialRecordType RecordType { get; set; }
		public DateTime? RecordDate { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Int32 SequenceNo { get; set; }
		public Guid? HeaderRowId { get; set; }
		public String ShortInfo { get; set; }
		public TimesheetMaterialStatus StockStatus { get; set; }

		public string RecordDateString => RecordDate == null ? "" : RecordDate.Value.ToString("ddd, MMM dd \\'yy");
		public string TimesheetFullName => TimesheetRowId == null ? "" : TimesheetName + " (" + TimesheetStartDate.FormatShort() + ")";

		public static string TimesheetMaterialRecordType2Name(TimesheetMaterialRecordType arg)
		{
			if (arg == TimesheetMaterialRecordType.Receive) return "Allocated";
			else if (arg == TimesheetMaterialRecordType.Use) return "Used";
			else if (arg == TimesheetMaterialRecordType.Return) return "Returned";
			else throw new ArgumentException();
		}
	}

	public enum TimesheetMaterialRecordType
	{
		Receive = 0,
		Use = 1,
		Return = 2
	}
	public enum TimesheetMaterialStatus { Allocated, Used, Sold, Warehouse }


}
