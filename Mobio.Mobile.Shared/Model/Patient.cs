using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using T = Telerik.XamarinForms.Input;

namespace OneBuilder.Model
{
	public class Patient : ViewModelBase
	{
		public Guid RowId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string IdentificationDocumentImage { get; set; }
		public string IdentificationDocumentNumber { get; set; }
		public DateTime? IdentificationDocumentExpDate { get; set; }
		public string IdentificationDocumentType { get; set; }
		public string Photo { get; set; }
		public DateTime? BirthDate { get; set; }
		public Guid? IdentificationDocumentRowId { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public Guid? StateOrProvinceRowId { get; set; }
		public int? CountryId { get; set; }
		public string Postcode { get; set; }
		public string HealthCareCardNumber { get; set; }
		public string FamilyPhysicianName { get; set; }
		public string FamilyPhysicianAddress { get; set; }
		public string FamilyPhysicianPhone { get; set; }
		public string FamilyPhysicianEmail { get; set; }
		public string Ip_Created { get; set; }
		public string Ip_Modified { get; set; }

		public PatientOrderItem PatientOrderItem { get; set; }

		public string FullPatientName => LastName + FirstName != "" ? LastName + ", " + FirstName : "New Record";
		public Color TextColor { get; set; }
		public Color BackgroundColor { get; set; }
		public Color BorderColor { get; set; }

		public RegisterViewModel RegisterViewModel { get; set; }
		public DateTime CalendarDisplayDate { get; set; }
		public ObservableCollection<T.Appointment> CalendarAppointments { get; set; }
		public Func<CalendarCell, CalendarCellStyle> CalendarSetStyleForCell { get; set; }

		public ObservableCollection<ScheduleItemSlot> ScheduleItemSlots { get; set; }
	}
}
