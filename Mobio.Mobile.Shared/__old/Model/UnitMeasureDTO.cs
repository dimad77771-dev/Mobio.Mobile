using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class UnitMeasureDTO
	{
		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string ShortName { get; set; }
		public Guid BusinessRowId { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }
	}
}
