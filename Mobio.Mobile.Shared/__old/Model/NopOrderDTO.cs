using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class NopOrderDTO
	{
		public Guid SalesOrderRowId { get; set; }
		public String OrderNumber { get; set; }
		public DateTime OrderDate { get; set; }
		public String ClientName { get; set; }

		public SalesOrderItemDTO[] SalesOrderItems { get; set; }
		public SalesOrderShippedItemDTO[] SalesOrderShippedItems { get; set; }
		public StockItemDTO[] AvailableStockItems { get; set; }
	}
}
