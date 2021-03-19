using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class Appointment : ViewModelBase
	{
		public Guid RowId { get; set; }
		public Guid ScheduleItemSlotRowId { get; set; }
		public bool? IsCompleted { get; set; }
		public DateTime? Created { get; set; }
		public Guid? CreatedBy { get; set; }
		public ScheduleItemSlot ScheduleItemSlot { get; set; }
	}

}
