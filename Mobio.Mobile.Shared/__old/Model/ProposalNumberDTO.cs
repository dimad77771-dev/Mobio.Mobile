using OneBuilder.Mobile.Constants;
using System;
using OneBuilder.Mobile;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class ProposalNumberDTO
	{
		public Guid TimesheetRowId { get; set; }
		public Guid ProjectRowId { get; set; }
		public int ProposalNumber { get; set; }

		public string Name => "" + ProposalNumber;
	}
}
