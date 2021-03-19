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
	public class Order : ViewModelBase
	{
		public Guid RowId { get; set; }
		public Guid UserProfileRowId { get; set; }
		public int Number { get; set; }
		public DateTime Date { get; set; }
		public Guid StatusRowId { get; set; }
		public bool? IsPaid { get; set; }
		public bool? IsNew { get; set; }
		public string InvestigationNumber { get; set; }
		public DateTime? Created { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? Modified { get; set; }
		public Guid? ModifiedBy { get; set; }
		public string Ip_Created { get; set; }
		public string Ip_Modified { get; set; }
		public UserProfile UserProfile { get; set; }
		public List<PatientOrderItem> Pois { get; set; }
	}
}
