using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class ScheduleItem
	{
		public Guid RowId { get; set; }
		public Guid InstitutionProfileRowId { get; set; }
		public DateTime Date { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public int Interval { get; set; }
		public int Capacity { get; set; }
		public DateTime? Created { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? Modified { get; set; }
		public Guid? ModifiedBy { get; set; }
		public string Ip_Created { get; set; }
		public string Ip_Modified { get; set; }
		//public UserProfile UserProfile { get; set; }
		public List<ScheduleItemSlot> ScheduleItemSlots { get; set; }
	}
}
