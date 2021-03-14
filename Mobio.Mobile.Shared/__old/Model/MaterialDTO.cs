using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneBuilder.Model
{
	public partial class MaterialDTO
	{
		public Guid RowId { get; set; }
		public Guid BusinessRowId { get; set; }
		public Guid? ParentRowId { get; set; }
		public Guid? InspectionRowId { get; set; }
		public Guid? TimesheetRowId { get; set; }
		public Guid UnitMeasureRowId { get; set; }
		public Guid SecondUnitMeasureRowId { get; set; }
		public decimal SecondUnitMeasureQty { get; set; }
		public string Name { get; set; }
		public decimal Cost { get; set; }
		public decimal Price { get; set; }
		public int OrderNum { get; set; }
		public bool? HasSerialNum { get; set; }
		public int SerialNum { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }

		public UnitMeasureDTO UnitMeasureRow { get; set; }
	}
}
