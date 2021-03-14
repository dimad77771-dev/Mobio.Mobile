using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class ScheduleItemSlot
	{
		public Guid RowId { get; set; }
		public Guid ScheduleItemRowId { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public string Slot { get; set; }
		public DateTime? Created { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? Modified { get; set; }
		public Guid? ModifiedBy { get; set; }
		public bool? IsFull { get; set; }
		public string Ip_Modified { get; set; }
		public string Ip_Created { get; set; }
		//public virtual ICollection<Appointment> Appointments { get; set; }
		//public ScheduleItem ScheduleItem { get; set; }
	}
}
