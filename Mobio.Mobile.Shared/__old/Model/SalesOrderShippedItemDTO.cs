using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class SalesOrderShippedItemDTO
	{
		public Guid RowId { get; set; }
		public Guid BusinessRowId { get; set; }
		public Guid SalesOrderRowId { get; set; }
		public Guid StockItemRowId { get; set; }
		public decimal Qty { get; set; }
		public decimal Cost { get; set; }
		public decimal Price { get; set; }
		public bool? IsActive { get; set; }
		public bool IsRefund { get; set; }
		public int Status { get; set; }
		public DateTime? StatusChangedDate { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }

		public StockItemDTO StockItemRow { get; set; }
	}

	public static class SalesOrderShippedItemStatus
	{
		public const int Shipped = 0;
		public const int Prepared = 1;

		public static string GetText(int status)
		{
			if (status == Shipped) return "Shipped";
			else if (status == Prepared) return "Prepared";
			else throw new ArgumentException();
		}
	}
}
