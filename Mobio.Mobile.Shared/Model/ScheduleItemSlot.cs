using OneBuilder.Mobile;
using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace OneBuilder.Model
{
	public class ScheduleItemSlot : ViewModelBase
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

		//public TextDecorations TextDecoration { get; set; } //=> (IsFull == true ? TextDecorations.Strikethrough : TextDecorations.None);
		//не получилось. глюк при перерисовке. TextDecoration = TextDecorations.Strikethrough "залипает"

		public Boolean IsSelectedSlot { get; set; }
		public Color TextColor => IsSelectedSlot ? U.GetAppColor("WhiteTextColor") : U.GetAppColor("BlackLightTextColor");
		public Color BackgroundColor => IsSelectedSlot ? U.GetAppColor("GreenTextColor") : Color.Transparent;
		public Color BorderColor => IsSelectedSlot ? Color.Transparent : Color.Transparent;

		public string FullSlotText
		{
			get
			{
				return Start.ToString("HH:mm") + "-" + Finish.ToString("HH:mm");
			}
		}

	}
}
