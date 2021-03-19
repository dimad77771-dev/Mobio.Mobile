using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class ProfileAdditionalData : ViewModelBase
	{
		public Guid RowId { get; set; }
		public int PatientTypeId { get; set; }
		public string PatientTypeName { get; set; }
		public Guid UserProfileRowId { get; set; }
		public string Number { get; set; }
	}
}
