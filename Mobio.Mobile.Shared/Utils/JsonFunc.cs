using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class JsonFunc
	{
		public static T CloneObject<T>(T obj)
		{
			var json = JsonConvert.SerializeObject(obj);
			var ret = JsonConvert.DeserializeObject<T>(json);
			return ret;
		}
	}
}
