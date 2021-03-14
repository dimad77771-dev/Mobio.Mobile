using OneBuilder.Mobile.Constants;
using System;
using OneBuilder.Mobile;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class TimesheetDTO
	{
		public Guid RowId { get; set; }
		public String Name { get; set; }
		public DateTime StartDate { get; set; }
		public Guid? ProjectRowId { get; set; }
		public Boolean IsLocked { get; set; }

		public String StartDateName => "Start at " + StartDate.FormatShort();
		public String FullName => Name + " (" + StartDate.FormatShort() + ")";
	}
}
