using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class UpdateNopOrderResult
	{
		public Boolean IsSuccess { get; set; }
		public String ErrorText { get; set; }
		public NopOrderDTO NewItem { get; set; }
	}
}
