using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using System.Reflection;

namespace OneBuilder.Mobile.Droid.Utils
{
	public static class ResourceManagerA
	{
		public static Resources GetResources()
		{
			return MainActivity.Instance.Resources;
		}

		public static Bitmap GetBitmap(int id)
		{
			return BitmapFactory.DecodeResource(GetResources(), id);
		}


		public static Bitmap GetBitmap(string name)
		{
			return BitmapFactory.DecodeResource(GetResources(), IdFromTitle(name, typeof(Drawable)));
		}

		public static Drawable GetDrawable(int id)
		{
			return GetResources().GetDrawable(id);
		}

		private static int IdFromTitle(string title, Type type)
		{
			string withoutExtension = System.IO.Path.GetFileNameWithoutExtension(title);
			return GetId(type, withoutExtension);
		}

		private static int GetId(Type type, string propertyName)
		{
			FieldInfo fieldInfo = Enumerable.FirstOrDefault<FieldInfo>(Enumerable.Select<FieldInfo, FieldInfo>((IEnumerable<FieldInfo>)type.GetFields(), (Func<FieldInfo, FieldInfo>)(p => p)), (Func<FieldInfo, bool>)(p => p.Name == propertyName));
			if (fieldInfo != (FieldInfo)null)
				return (int)fieldInfo.GetValue((object)type);
			return 0;
		}

	}


	public class R : Resource
	{
		public static Android.Content.Res.Resources GetResources()
		{
			return MainActivity.Instance.Resources;
		}


		public static Android.Graphics.Drawables.Drawable GetDrawable(int id)
		{
			return GetResources().GetDrawable(id);
		}





	}
}