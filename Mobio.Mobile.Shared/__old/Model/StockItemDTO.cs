using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class StockItemDTO
	{
		public Guid RowId { get; set; }
		public Guid BusinessRowId { get; set; }
		public Guid? StockItemLocationRowId { get; set; }
		public int ItemType { get; set; }
		public int Status { get; set; }
		public Guid? BoxItemRowId { get; set; }
		public Guid? PackageTypeRowId { get; set; }
		public Guid? MaterialRowId { get; set; }
		public int? SerialNum { get; set; }
		public decimal Qty { get; set; }
		public string Notes { get; set; }
		public DateTime? CreatedDate { get; set; }
		public bool? IsActive { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }

		public MaterialDTO MaterialRow { get; set; }
		public StockPackageTypeDTO PackageTypeRow { get; set; }
		public StockLocationDTO StockItemLocationRow { get; set; }
	}
}
