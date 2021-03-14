using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using OneBuilder.Model;
using OneBuilder.Mobile;

namespace OneBuilder.WebServices
{
	public class WebService
	{
#if DEBUG
		public static string BASEADR = @"http://192.168.1.81";
		public static string BASESERVICE = BASEADR + @"/HouseNotes.WCF/TimesheetService.svc/";
		public static string WEBBASEADR = @"http://192.168.1.81:8999";

		//public static string BASEADR = @"https://obo.imgroup.ca";
		//public static string BASESERVICE = BASEADR + @"/TimesheetService.svc/";
		//public static string WEBBASEADR = @"https://obo.imgroup.ca";
#else
		public static string BASEADR = @"https://obo.imgroup.ca";
		public static string BASESERVICE = BASEADR + @"/TimesheetService.svc/";
		public static string WEBBASEADR = @"https://obo.imgroup.ca";

		//public static string BASEADR = @"http://192.168.1.81";
		//public static string BASESERVICE = BASEADR + @"/HouseNotes.WCF/TimesheetService.svc/";
		//public static string WEBBASEADR = @"http://192.168.1.81:8999";
#endif

		async public Task<WebGetServiceRetrun> Get(String url0, CancellationToken cancellationToken = default(CancellationToken), Guid? deviceId = null)
		{
			var oret = new WebGetServiceRetrun();
			var httpClient = new HttpClient();
			AddContentHeaders(httpClient.DefaultRequestHeaders);
			var url = BASESERVICE + url0;

			var result = await httpClient.GetAsync(url);
			if (!result.IsSuccessStatusCode)
			{
				var response = await result.Content.ReadAsStringAsync();
				U.Log("Error=" + response);
				oret.Success = false;
				oret.ErrorNotFound = (result.StatusCode == HttpStatusCode.NotFound);
			}
			else
			{
				oret.Success = true;
				var response = await result.Content.ReadAsStringAsync();
				oret.Value = response;
			}

			return oret;
		}

		async public Task<WebPostServiceRetrun> Post(String url0, String json, Boolean NeedValueOnSuccess = false)
		{
			var oret = new WebPostServiceRetrun();
			var httpClient = new HttpClient();

			var url = BASESERVICE + url0;
			var content = new StringContent(json);
			AddContentHeaders(content.Headers);

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.PostAsync(url, content);
				response = await result;
			}
			catch (Exception ex)
			{
				oret.Success = false;
				oret.Value = ex.ToString();
				return oret;
			}

			if (response.IsSuccessStatusCode)
			{
				var ret = await response.Content.ReadAsStringAsync();

				oret.Success = true;
				if (NeedValueOnSuccess)
				{
					oret.Value = await response.Content.ReadAsStringAsync();
				}
				return oret;
			}
			else
			{
				var ret = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(ret))
				{
					ret = "Internal error";
				}

				oret.Success = false;
				oret.Value = ret;
				return oret;
			}
		}

		async public Task<WebGetServiceRetrun> GetGzip(String url0)
		{
			var oret = new WebGetServiceRetrun();
			var httpClient = new HttpClient();
			var url = BASESERVICE + url0;

			var result = await httpClient.GetAsync(url);
			if (!result.IsSuccessStatusCode)
			{
				var response = await result.Content.ReadAsStringAsync();
				U.Log("Error=" + response);
				oret.Success = false;
			}
			else
			{
				oret.Success = true;
				var responseGzip = await result.Content.ReadAsByteArrayAsync();
				var responseBytes = Decompress(responseGzip);
				var response = Encoding.UTF8.GetString(responseBytes);
				oret.Value = response;
			}
			return oret;
		}

		async public Task<WebGetBytesServiceRetrun> GetGzipBytes(String url0)
		{
			var oret = new WebGetBytesServiceRetrun();
			var httpClient = new HttpClient();
			AddContentHeaders(httpClient.DefaultRequestHeaders);
			var url = BASESERVICE + url0;

			var result = await httpClient.GetAsync(url);	
			if (!result.IsSuccessStatusCode)
			{
				var response = await result.Content.ReadAsStringAsync();
				U.Log("Error=" + response);
				oret.Success = false;
			}
			else
			{
				oret.Success = true;
				var responseGzip = await result.Content.ReadAsByteArrayAsync();
				var responseBytes = Decompress(responseGzip);
				oret.Bytes = responseBytes;
			}
			return oret;
		}


		async public Task<WebGetBytesServiceRetrun> GetBytes(String url0)
		{
			var oret = new WebGetBytesServiceRetrun();
			var httpClient = new HttpClient();
			AddContentHeaders(httpClient.DefaultRequestHeaders);
			var url = BASESERVICE + url0;

			var result = await httpClient.GetAsync(url);
			if (!result.IsSuccessStatusCode)
			{
				var response = await result.Content.ReadAsStringAsync();
				U.Log("Error=" + response);
				oret.Success = false;
			}
			else
			{
				oret.Success = true;
				var responseBytes = await result.Content.ReadAsByteArrayAsync();
				oret.Bytes = responseBytes;
			}
			return oret;
		}



		static byte[] Decompress(byte[] data)
		{
			using (var compressedStream = new MemoryStream(data))
			using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
			using (var resultStream = new MemoryStream())
			{
				zipStream.CopyTo(resultStream);
				return resultStream.ToArray();
			}
		}

		void AddContentHeaders(HttpHeaders content)
		{
            content.Add("UserRowId", UserOptions.GetUserRowId().ToString());
            content.Add("DeviceId", UserOptions.GetDeviceId().ToString());
            content.Add("Password", UserOptions.GetPassword());
		}
		

		public static WebService Instance
		{
			get
			{
				return new WebService();
			}
		}



	}



}
