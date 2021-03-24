using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using OneBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class TaskFunc
	{
		public static Task<T> FromResult<T>(T value)
		{
			var tsc = new TaskCompletionSource<T>();
			tsc.SetResult(value);
			return tsc.Task;
		}
			 

		public static bool HasError(this IEnumerable<Task<object>> tasks)
		{
			return (tasks.Any(q => q.Result == null));
		}
	}
}
