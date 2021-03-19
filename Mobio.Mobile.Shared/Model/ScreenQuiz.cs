using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class ScreenQuiz : ViewModelBase
	{
		public Guid RowId { get; set; }
		public bool? Fever { get; set; }
		public bool? Cough { get; set; }
		public bool? RunnyNoise { get; set; }
		public bool? LossOfTaste { get; set; }
		public bool? DifficultyBreathing { get; set; }
		public bool? SoreThroat { get; set; }
		public bool? Nausea { get; set; }
		public bool? Tired { get; set; }
		public bool? ContactWithCovid { get; set; }
		public bool? TravelReturn { get; set; }
		public bool? TravelPressured { get; set; }
	}

}
