using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneBuilder.Mobile;
using OneBuilder.Model;
using OneBuilder.Sqlite;

namespace OneBuilder.WebServices
{
    public class AuthenticateTask
    {
        async public Task<Boolean> Run(string businessCode, string username, string password)
        {
			//var rezz = await WebServiceFunc.Login(businessCode, username, password);

			UIFunc.ShowLoading("Authorization...");
			var url0 = "Authenticate?businessCode=" + Uri.EscapeDataString(businessCode) 
							+ "&username=" + Uri.EscapeDataString(username)
							+ "&password=" + Uri.EscapeDataString(password);
            var rez = await WebService.Instance.Get(url0);
            if (!rez.Success)
            {
                UIFunc.HideLoading();
                await UIFunc.AlertError("Error in authorization procedure");
                return false;
            }

            var result = WebServiceFunc.DeserializeObjectFromStream<AuthenticateReturn>(rez.Value);
            if (!String.IsNullOrEmpty(result.Error))
            {
                UIFunc.HideLoading();
                await UIFunc.AlertError(result.Error);
                return false;
            }

			var optrow = UserOptions.GetCurrent();
			optrow.Username = username;
			optrow.Password = password;
			optrow.UserRowId = result.UserRowId;
			optrow.BusinessRowId = result.BusinessRowId;
			optrow.BusinessCode = result.BusinessCode;
			DB.Update(optrow);

            UIFunc.HideLoading();
            return true;
        }
    }

    public class AuthenticateReturn
    {
		public Guid UserRowId;
		public string UserName;
		public string Error;
		public Guid BusinessRowId;
		public String BusinessCode;
	}
}
