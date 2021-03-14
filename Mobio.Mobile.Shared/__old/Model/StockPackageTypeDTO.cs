using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public partial class StockPackageTypeDTO
	{
		public Guid RowId { get; set; }
		public Guid BusinessRowId { get; set; }
		public Guid MaterialRowId { get; set; }
		public decimal? Qty { get; set; }
		public decimal? BaseCost { get; set; }
		public decimal? PackCost { get; set; }
		public decimal? LabourCost { get; set; }
		public decimal? Cost { get; set; }
		public decimal? Price { get; set; }
		public int SerialNum { get; set; }
		public bool? IsActive { get; set; }
		public string Sku { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }

		public MaterialDTO MaterialRow { get; set; }

		public string Name
		{
			get { return IsOpen ? "Open Pkg" : Qty.Value.ToString("0.0#########") + " " + MaterialRow.UnitMeasureRow.ShortName; }
		}

		public bool IsOpen
		{
			get { return Qty == null; }
		}

		public void CalculateCost()
		{
			Cost = (BaseCost ?? 0) + (PackCost ?? 0) + (LabourCost ?? 0);
		}


		public string FullName
		{
			get
			{
				var qtyText = this.IsOpen ? "Open Pkg" : this.Qty.Value.ToString("0.0#########") + " " + MaterialRow.UnitMeasureRow.ShortName;
				var name = MaterialRow.Name + ", " + qtyText;
				return name;
			}
		}
	}
}
