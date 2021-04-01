using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using OneBuilder.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace OneBuilder.WebServices
{
	public static class WebServiceFunc
	{
		async public static Task<byte[]> GetInitInfo()
		{
			var url0 = "GetInitInfo?" + UserOptions.BusinessWebString();

			var rez = await WebService.Instance.GetGzipBytes(url0);
			if (!rez.Success) return null;

			return rez.Bytes;
		}

		async public static Task<GetCheckApkReturn> GetCheckApkVersion(int apkversion)
		{
			var url0 = "OnePosCheckApkVersion?apkversion=" + apkversion + "&" + UserOptions.BusinessWebString();

			var rez = await WebService.Instance.Get(url0);
			if (!rez.Success) return null;

			var result = DeserializeObjectFromStream<GetCheckApkReturn>(rez.Value);

			return result;
		}

		async public static Task<Order> GetOrder(Guid id)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/account/getorder?id=" + id;

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.GetAsync(url);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return null;
			}

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = DeserializeObjectFromWebServer<Order>(json);
				if (result.IsError)
				{
					return null;
				}
				return result.data;
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return null;
			}
		}

		async public static Task<UserProfile> GetProfile(Guid id)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/account/getprofile?id=" + id;

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.GetAsync(url);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return null;
			}

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = DeserializeObjectFromWebServer<UserProfile>(json);
				if (result.IsError)
				{
					return null;
				}
				return result.data;
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return null;
			}
		}

		async public static Task<Order[]> GetUserOrders(Guid id)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/account/getUserOrders?id=" + id;

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.GetAsync(url);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return null;
			}

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = DeserializeObjectFromWebServer<Order[]>(json);
				if (result.IsError)
				{
					return null;
				}
				return result.data == null ? new Order[0] : result.data;
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return null;
			}
		}

		async public static Task<ScheduleItemSlot[]> GetBookingSlots(Guid id)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/utils/getBookingSlots?id=" + id;

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.GetAsync(url);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return null;
			}

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = DeserializeObjectFromWebServer<ScheduleItemSlot[]>(json);
				if (result.IsError)
				{
					return null;
				}
				return result.data == null ? new ScheduleItemSlot[0] : result.data;
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return null;
			}
		}

		async public static Task<UserProfile[]> GetInstitutionsForSchedule()
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/utils/getInstitutionsForSchedule";

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.GetAsync(url);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return null;
			}

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var rez = DeserializeObjectFromWebServer<UserProfile[]>(json);
				if (rez.IsError)
				{
					return null;
				}
				var rrr = rez.data ?? new UserProfile[0];
				return rrr;
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return null;
			}
		}

		async public static Task<State[]> GetStates(int countryId)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/utils/getstates?countryId=" + countryId;

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.GetAsync(url);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return null;
			}

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = DeserializeObjectFromWebServer<State[]>(json);
				if (result.IsError)
				{
					return null;
				}
				return result.data ?? new State[0];
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return null;
			}
		}

		async public static Task<(bool,string,Order)> SaveOrder(Order order)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/account/saveOrder";
			var json = JsonConvert.SerializeObject(order);
			var content = new StringContent(json);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.PostAsync(url, content);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return (false,"",null);
			}

			if (response.IsSuccessStatusCode)
			{
				var rjson = await response.Content.ReadAsStringAsync();
				var rinfo = JsonConvert.DeserializeObject<PostRetrunInfo>(rjson);
				if (rinfo.Status == "Ok")
				{
					var jobject = JObject.Parse(rjson);
					var ojson = jobject["Data"].ToString();
					var order2 = JsonConvert.DeserializeObject<Order>(ojson);

					return (true, "", order2);
				}
				else
				{
					return (false, rinfo.Error, null);
				}
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return (false, "", null);
			}
		}

		async public static Task<Guid> CreateOrUpdateProfile(UserProfile model)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/account/createorupdateprofile";
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.PostAsync(url, content);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return default(Guid);
			}

			if (response.IsSuccessStatusCode)
			{
				var rjson = await response.Content.ReadAsStringAsync();
				var rinfo = JsonConvert.DeserializeObject<PostRetrunInfo>(rjson);
				if (rinfo.Status == "Ok")
				{
					var jobject = JObject.Parse(rjson);
					var resultRowId = (jobject["Data"]["RowId"] as JToken).ToObject<Guid>();
					if (resultRowId == default(Guid)) throw new Exception();
					return resultRowId;
				}
				else
				{
					return default(Guid);
				}
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return default(Guid);
			}
		}

		const string ASPNET_APPLICATIONCOOKIE = ".AspNet.ApplicationCookie";
		async public static Task<(bool, Guid?, string, string)> SubmitLogin(LoginModel model)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/home/submitlogin";
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.PostAsync(url, content);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return (false, null, "", "");
			}

			if (response.IsSuccessStatusCode)
			{
				var rjson = await response.Content.ReadAsStringAsync();
				var rinfo = JsonConvert.DeserializeObject<PostRetrunInfo>(rjson);
				if (rinfo?.Status == "Ok")
				{
					var responseCookies = cookies.GetCookies(new Uri(WebService.WEBBASEADR)).Cast<Cookie>();
					var cookie = responseCookies.SingleOrDefault(q => q.Name == ASPNET_APPLICATIONCOOKIE);
					if (cookie == null || string.IsNullOrEmpty(cookie.Value))
					{
						return (false, null, $"<{ASPNET_APPLICATIONCOOKIE}> not found", "");
					}
					var aspxauth = cookie.Value;
					//var aspxauth = "";

					var jobject = JObject.Parse(rjson);
					var resultRowId = (jobject["Data"]["RowId"] as JToken).ToObject<Guid>();
					if (resultRowId == default(Guid)) throw new Exception();
					return (true, resultRowId, "", aspxauth);
				}
				else
				{
					return (false, null, rinfo?.Error, "");
				}
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return (false, null, "", "");
			}
		}

		async public static Task<(bool, string)> UpdatePassword(ChangePasswordModel model)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler();
			handler.CookieContainer = cookies;
			SetupAspxauth(cookies);

			var httpClient = new HttpClient(handler);
			var url = WebService.WEBBASEADR + @"/account/updatePassword";
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage response = null;
			try
			{
				var result = httpClient.PostAsync(url, content);
				response = await result;
			}
			catch (Exception ex)
			{
				var error = ex.ToString();
				return (false, "");
			}

			if (response.IsSuccessStatusCode)
			{
				var rjson = await response.Content.ReadAsStringAsync();
				var rinfo = JsonConvert.DeserializeObject<PostRetrunInfo>(rjson);
				if (rinfo?.Status == "Ok")
				{
					return (true, "");
				}
				else
				{
					return (false, rinfo?.Error);
				}
			}
			else
			{
				var error = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(error))
				{
					error = "Internal error";
				}
				return (false, "");
			}
		}

		static void SetupAspxauth(CookieContainer cookies)
		{
			cookies.Add(new Uri(WebService.WEBBASEADR), new Cookie(ASPNET_APPLICATIONCOOKIE, UserOptions.GetAspxauth()));
		}

		public class PostRetrunInfo
		{
			public string Status { get; set; }
			public string Error { get; set; }
		}

		async public static Task<String> PushErrorLog(PushErrorLogArgument arg)
		{
			var url0 = "PushErrorLog";
			var json = JsonConvert.SerializeObject(arg);

			var ret = await WebService.Instance.Post(url0, json, true);
			if (ret.Success)
			{
				var result = DeserializeStringFromStream(ret.Value);
				return result;
			}
			else
			{
				return ret.Value;
			}
		}
		public static T DeserializeObjectFromStream<T>(string arg)
		{
			var jobject = JObject.Parse(arg);
			var result = JsonConvert.DeserializeObject<T>(jobject["Result"].ToString());
			return result;
		}
		public static string DeserializeStringFromStream(string arg)
		{
			var jobject = JObject.Parse(arg);
			return jobject["Result"].ToString();
		}
		public static DeserializeObjectFromWebServerReturn<T> DeserializeObjectFromWebServer<T>(string arg)
		{
			var jobject = JObject.Parse(arg);
			var data = jobject["Data"];
			var jdata = data as JToken;
			if (jdata == null)
			{
				return new DeserializeObjectFromWebServerReturn<T> { IsError = true };
			}
			else
			{
				if (string.IsNullOrEmpty(jdata.ToString()))
				{
					return new DeserializeObjectFromWebServerReturn<T>
					{
						data = default(T),
						IsError = false,
					};
				}
				else
				{
					var result = JsonConvert.DeserializeObject<T>(jdata.ToString());
					return new DeserializeObjectFromWebServerReturn<T>
					{
						data = result,
						IsError = false,
					};
				}
			}
		}
		public class DeserializeObjectFromWebServerReturn<T>
		{
			public T data { get; set; }
			public bool IsError { get; set; }
		}
	}

	public class GetCheckApkReturn
	{
		public String Error;
		public Guid CurrentServerDBVersion;
	}
	public class PushErrorLogArgument
	{
		public List<PushErrorLogArgumentFile> LogFiles;
		public String UserName;
		public Guid BusinessRowId;
		public Guid UserRowId;
	}
	public class PushErrorLogArgumentFile
	{
		public String ErrorFile;
		public String ErrorText;
	}



}
