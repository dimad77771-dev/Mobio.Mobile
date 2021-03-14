using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public partial class StockLocationDTO
	{
		public Guid RowId { get; set; }
		public Guid BusinessRowId { get; set; }
		public string Name { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public Guid? CountryRowId { get; set; }
		public Guid? StateRowId { get; set; }
		public string PostalCode { get; set; }
		public string Phone { get; set; }
		public string CellNumber { get; set; }
		public string Email { get; set; }
		public string Note { get; set; }
		public bool IsActive { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }
		public int StockLocationType { get; set; }
	}
}
