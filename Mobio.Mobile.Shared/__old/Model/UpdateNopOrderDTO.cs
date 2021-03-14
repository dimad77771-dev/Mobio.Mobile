using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class UpdateNopOrderDTO
	{
		public Guid SalesOrderRowId { get; set; }
		public List<UpdateNopOrderShippedItemDTO> ShippedItems { get; set; } = new List<UpdateNopOrderShippedItemDTO>();
		public List<Guid> AddedItems { get; set; } = new List<Guid>();
		public List<Guid> DeletedItems { get; set; } = new List<Guid>();
	}

	public class UpdateNopOrderShippedItemDTO
	{
		public Guid StockItemRowId { get; set; }
		public Int32 Status { get; set; }
	}

}
