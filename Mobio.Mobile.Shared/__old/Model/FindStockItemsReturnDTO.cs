using OneBuilder.Mobile.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class FindStockItemsDTO
	{
		public Guid StockItemRowId { get; set; }
		public Guid? BoxItemRowId { get; set; }
		public StockItemType ItemType { get; set; }
		public String Name { get; set; }
		public Int32? SerialNum { get; set; }
		public Guid? LaborRowId { get; set; }
		public String ShortInfo { get; set; }
		public TimesheetMaterialStatus StockStatus { get; set; }
		public FindStockItemsDTO[] ChildItems { get; set; } = new FindStockItemsDTO[0];
	}

	public enum StockItemType
	{
		ProductItem = 0,
		Box = 1
	}
}
