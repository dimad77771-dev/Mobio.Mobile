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
	public static class TaskExtensions
	{
		public static bool HasError(this IEnumerable<Task<object>> tasks)
		{
			return (tasks.Any(q => q.Result == null));
		}
	}
}
