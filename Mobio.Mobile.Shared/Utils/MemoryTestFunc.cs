using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Linq.Expressions;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using OneBuilder.Mobile.ViewModels;
using System.Diagnostics;

namespace OneBuilder.Mobile
{
	public static class MemoryTestFunc
	{
		public static List<WeakReference> List123 = new List<WeakReference>();

		public static void AddList123(object arg)
		{
			List123.Add(new WeakReference(arg));
		}

		public static void PrintList123()
		{
			for (int i = 0; i < 10; i++)
			{
				GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
			}

			Debug.WriteLine("-------PrintList123-----------------");
			for (int i = 0; i < List123.Count; i++)
			{
				Debug.WriteLine("i=" + i + "; IsAlive=" + List123[i].IsAlive);
			}
			Debug.WriteLine("-------PrintList123----------------- END");
		}
	}
}