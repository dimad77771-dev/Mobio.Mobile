using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class UserProfile
	{
		public Guid RowId { get; set; }
		public bool? IsCorporate { get; set; }
		public bool? IsNew { get; set; }
		public string CompanyName { get; set; }
		public string InvestigationNumber { get; set; }
		public int Type { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public string Web { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public int? CountryId { get; set; }
		public string City { get; set; }
		public Guid? ProvinceOrStateRowId { get; set; }
		public string Postcode { get; set; }
		public string UserRowId { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Modified { get; set; }
		public Guid? CreatedBy { get; set; }
		public Guid? ModifiedBy { get; set; }
		public string Ip_Created { get; set; }
		public string Ip_Modified { get; set; }
		public List<ScheduleItem> ScheduleItems { get; set; }
		public List<ProfileAdditionalData> AdditionalDatas { get; set; }
		public DateTime? CalendarStart { get; set; }


		public string PasswordRepeat { get; set; }
		public Xamarin.Forms.Color BorderColor { get; set; }
	}
}
