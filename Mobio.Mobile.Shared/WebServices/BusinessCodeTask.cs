//using OneBuilder.Mobile;
//using OneBuilder.Model;
//using OneBuilder.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace OneBuilder.WebServices
//{
//	public class BusinessCodeTask
//	{
//		async public Task<Boolean> Run(string businessCode)
//		{
//			UIFunc.ShowLoading("Authorization...");
//			var url0 = "BusinessGet?businesscode=" + Uri.EscapeDataString(businessCode);
//			var rez = await WebService.Instance.Get(url0);
//			if (!rez.Success)
//			{
//				UIFunc.HideLoading();
//				await UIFunc.AlertError("Error in business authorization procedure");
//				return false;
//			}

//			var result = WebServiceFunc.DeserializeObjectFromStream<BusinessCodeReturn>(rez.Value);
//			if (!String.IsNullOrEmpty(result.Error))
//			{
//				UIFunc.HideLoading();
//				await UIFunc.AlertError(result.Error);
//				return false;
//			}
//			var businessRowId = result.BusinessRowId;
			

//			DB.BeginTransaction();
//			UserOptions.SetBusinessInfo(businessRowId, businessCode);
//			DB.Commit();

//			UIFunc.HideLoading();
//			return true;
//		}
//	}

//	public class BusinessCodeReturn
//	{
//		public String Error;
//		public Guid BusinessRowId;
//		public String BusinessCode;
//	}
//}
