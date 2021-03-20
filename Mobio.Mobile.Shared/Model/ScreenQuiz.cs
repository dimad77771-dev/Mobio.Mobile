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

		public ScreenQuiz()
		{
			CheckBoxeNames = new[] { "Fever", "Cough", "RunnyNoise", "LossOfTaste", "DifficultyBreathing", "SoreThroat", "Nausea", "Tired", "ContactWithCovid", "TravelReturn", "TravelPressured" };
			this.PropertyChanged += CheckBoxesPropertyChanged;
		}

		private bool inCheckBoxesPropertyChanged = false;
		private void CheckBoxesPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (inCheckBoxesPropertyChanged) return;

			inCheckBoxesPropertyChanged = true;

			if (CheckBoxeNames.Contains(e.PropertyName))
			{
				var value = (bool?)this.GetType().GetProperty(e.PropertyName).GetValue(this);
				this.GetType().GetProperty(e.PropertyName + "Yes").SetValue(this, (value == true));
				this.GetType().GetProperty(e.PropertyName + "No").SetValue(this, (value == false));
			}

			for (int k = 0; k < 2; k++)
			{
				var bval = (k == 0 ? true : false);
				var vstr = (k == 0 ? "Yes" : "No");
				var ostr = (k == 0 ? "No" : "Yes");
				foreach (var vprop in CheckBoxeNames)
				{
					var fprop = vprop + vstr;
					var oprop = vprop + ostr;
					if (e.PropertyName == fprop)
					{
						var fval = (bool)this.GetType().GetProperty(fprop).GetValue(this);
						var oval = (bool)this.GetType().GetProperty(oprop).GetValue(this);
						bool? nval;
						if (fval == false && oval == false)
						{
							nval = null;
						}
						else if (fval == true && oval == false)
						{
							nval = bval;
						}
						else if (fval == false && oval == true)
						{
							nval = !bval;
						}
						else if (fval == true && oval == true)
						{
							nval = bval;
							this.GetType().GetProperty(oprop).SetValue(this, false);
						}
						else throw new Exception();

						this.GetType().GetProperty(vprop).SetValue(this, nval);
					}
				}
			}

			inCheckBoxesPropertyChanged = false;
		}

		private string[] CheckBoxeNames;
		public bool FeverYes { get; set; }
		public bool CoughYes { get; set; }
		public bool RunnyNoiseYes { get; set; }
		public bool LossOfTasteYes { get; set; }
		public bool DifficultyBreathingYes { get; set; }
		public bool SoreThroatYes { get; set; }
		public bool NauseaYes { get; set; }
		public bool TiredYes { get; set; }
		public bool ContactWithCovidYes { get; set; }
		public bool TravelReturnYes { get; set; }
		public bool TravelPressuredYes { get; set; }
		public bool FeverNo { get; set; }
		public bool CoughNo { get; set; }
		public bool RunnyNoiseNo { get; set; }
		public bool LossOfTasteNo { get; set; }
		public bool DifficultyBreathingNo { get; set; }
		public bool SoreThroatNo { get; set; }
		public bool NauseaNo { get; set; }
		public bool TiredNo { get; set; }
		public bool ContactWithCovidNo { get; set; }
		public bool TravelReturnNo { get; set; }
		public bool TravelPressuredNo { get; set; }


		public string _Fever => (Fever == null ? "NULL" : Fever.ToString());
		public string _TotalCheckText
		{
			get
			{
				return ""
				+ (Fever == null ? "NULL" : Fever.ToString()) + ";"
				+ (TravelPressured == null ? "NULL" : TravelPressured.ToString()) + ";"
				+ (Tired == null ? "NULL" : Tired.ToString()) + ";"
				+ (Nausea == null ? "NULL" : Nausea.ToString()) + ";"
				;
			}
		}
	}

}
