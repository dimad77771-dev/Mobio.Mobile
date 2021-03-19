using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class State : ViewModelBase
	{
		public int CountryId { get; set; }
		public Guid RowId { get; set; }
		public string Name { get; set; }
	}
}
