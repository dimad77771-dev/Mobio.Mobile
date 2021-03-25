using Newtonsoft.Json;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Constants;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace OneBuilder.Model
{
	public class PatientOrderItem : ViewModelBase
	{
		public Guid RowId { get; set; }
		public Guid OrderRowId { get; set; }
		public Guid PatientRowId { get; set; }
		public string QRCode { get; set; }
		public bool? IsNew { get; set; }
		public Guid? PatientOrderItemStatusRowId { get; set; }
		public Guid? AssignedServiceProviderRowId { get; set; }
		public Guid? UserProfileRowId { get; set; }
		public Guid? InstitutionProfileRowId { get; set; }
		public string TestKitId { get; set; }
		public bool? TestKitValidated { get; set; }
		public DateTime? TestKitUsedDate { get; set; }
		public string TestKitPhoto { get; set; }
		public string TestKitType { get; set; }
		public string ResultPhoto { get; set; }
		public bool? IsResult1Positive { get; set; }
		public bool? IsResult2Positive { get; set; }
		public bool? IsResult3Positive { get; set; }
		public string Result1Value { get; set; }
		public string Result2Value { get; set; }
		public string Result3Value { get; set; }
		public string ResultInterpretation { get; set; }
		public string VideoEvidenceUrl { get; set; }
		public DateTime? Created { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? Modified { get; set; }
		public Guid? ModifiedBy { get; set; }
		public string Ip_Created { get; set; }
		public string Ip_Modified { get; set; }
		public Patient Patient { get; set; }
		//public Order Order { get; set; }
		public UserProfile InstrititionProfile { get; set; }
		public UserProfile PatientProfile { get; set; }
		public Appointment Appointment { get; set; }
		public ScreenQuiz ScreenQuiz { get; set; }
		public LabConsent LabConsent { get; set; }

		[JsonIgnore]
		public bool IsShowQRCode => !string.IsNullOrEmpty(QRCode);
		[JsonIgnore]
		public ImageSource QRCodeImageSource => ImageFunc.GetStreamFromBase64String(QRCode);

		[JsonIgnore]
		public bool IsShowTestKitPhoto => !string.IsNullOrEmpty(TestKitPhoto);
		[JsonIgnore]
		public ImageSource TestKitPhotoImageSource => ImageFunc.GetStreamFromBase64String(TestKitPhoto);

		[JsonIgnore]
		public bool IsShowResultPhoto => !string.IsNullOrEmpty(ResultPhoto);
		[JsonIgnore]
		public ImageSource ResultPhotoImageSource => ImageFunc.GetStreamFromBase64String(ResultPhoto);

		[JsonIgnore]
		public string _ResultInterpretation => string.IsNullOrEmpty(ResultInterpretation) ? " " : ResultInterpretation;


		public bool IsNewRow { get; set; } = false;
		public bool IsInitRow { get; set; } = false;

		[JsonIgnore]
		public bool IsHasError { get; set; } = false;

		//public ObservableCollection<UserProfile> DdlInstitutions { get; set; } = new ObservableCollection<UserProfile>();
		//public UserProfile InstitutionProfile
		//{
		//	get
		//	{
		//		return DdlInstitutions.SingleOrDefault(q => q.RowId == InstitutionProfileRowId);
		//	}
		//	set
		//	{
		//		InstitutionProfileRowId = value?.RowId;
		//	}
		//}
	}
}
