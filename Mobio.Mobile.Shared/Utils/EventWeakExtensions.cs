using System;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace OneBuilder.Mobile
{
	public static class EventWeakExtensions
	{
		public static Func<T, Task> BuildEventWeak<T>(this Func<T, Task> callback)
		{
			var action1 = new EventWeak<T>(callback);
			Func<T, Task> runaction = async (arg) => await action1.RunAsync(arg);
			return runaction;
		}

		public static Func<Task> BuildEventWeak(this Func<Task> callback)
		{
			var action1 = new EventWeak(callback);
			Func<Task> runaction = async () => await action1.RunAsync();
			return runaction;
		}
	}
}






