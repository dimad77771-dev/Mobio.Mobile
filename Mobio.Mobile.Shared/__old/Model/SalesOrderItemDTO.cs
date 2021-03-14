using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class SalesOrderItemDTO
	{
		public Guid RowId { get; set; }
		public Guid BusinessRowId { get; set; }
		public Guid SalesOrderRowId { get; set; }
		public Guid StockLocationRowId { get; set; }
		public Guid PackageTypeRowId { get; set; }
		public int UnitsQty { get; set; }
		public decimal PackageQty { get; set; }
		public decimal Price { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }

		public StockPackageTypeDTO PackageTypeRow { get; set; }
		public StockLocationDTO StockLocationRow { get; set; }

		public string ProductFullName => PackageTypeRow.FullName;
		public Decimal TotalPrice => Price * UnitsQty;
	}
}
