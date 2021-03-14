using OneBuilder.Model;
using OneBuilder.WebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class UnhandledExceptionProccesing
	{
		public static void LogUnhandledException(Exception ex)
		{
			var now = DateTime.Now;
			var dir = GetErrorDir();
			var lastname = Path.Combine(dir, GetLastErrorFile());
			var filename = Path.Combine(dir, now.ToString("yyyyMMddHHmmss") + ".txt");
			var info = now.ToString("yyyy-MM-dd HH:mm:ss");
			var error = ex.ToString();
			info += "\n\n" + ex.ToString();
			File.WriteAllText(lastname, info);
			File.WriteAllText(filename, info);
			Console.WriteLine("" + error);
			Console.Out.Flush();

			throw new AggregateException(ex);
		}

		static string GetErrorDir()
		{

			var dir = U.GetLocalFilesDir();
			var edir = dir + "/" + "errors";
			if (!Directory.Exists(edir))
			{
				Directory.CreateDirectory(edir);
			}
			return edir;
		}

		static string GetLastErrorFile()
		{
			return "Lasterror.text";
		}


		async public static Task<bool> SendErrorServer()
		{
			var dir = GetErrorDir();
			if (Directory.Exists(dir))
			{
				var files = Directory.EnumerateFiles(dir).Where(q => !q.Contains(GetLastErrorFile())).OrderBy(q => q).ToArray();
				if (files.Length > 0)
				{
					var arg = new PushErrorLogArgument
					{
						UserRowId = UserOptions.GetUserRowId(),
						UserName = UserOptions.GetUser(),
						LogFiles = new List<PushErrorLogArgumentFile>(),
						BusinessRowId = UserOptions.GetBusinessRowId(),
					};

					foreach (var file in files)
					{
						var errtext = File.ReadAllText(file);
						arg.LogFiles.Add(new PushErrorLogArgumentFile { ErrorFile = file, ErrorText = errtext });
					}

					UIFunc.ShowLoading("Send error log...");
					var ret = await WebServiceFunc.PushErrorLog(arg);
					if (!string.IsNullOrEmpty(ret))
					{
						UIFunc.HideLoading();
						return false;
					}

					foreach (var file in files)
					{
						File.Delete(file);
					}
				}
			}

			UIFunc.HideLoading();
			return true;
		}
	}
}
