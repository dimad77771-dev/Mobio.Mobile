using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class CommandFunc
	{
		public static Command CreateAsync(Func<Task> action)
		{
			return new Command(async () => await action());
		}
		public static Command CreateAsync(Func<Task> action, Func<bool> canExecute)
		{
			return new Command(async () => await action(), canExecute);
		}
		public static Command CreateAsync(Func<object, Task> action)
		{
			return new Command(async (pm) => await action(pm));
		}
		public static Command CreateAsync(Func<string, Task> action)
		{
			return new Command(async (pm) => await action((string)pm));
		}

		public static Command Create(Action<string> action)
		{
			return new Command((pm) => action((string)pm));
		}
		public static Command Create(Action<object> action)
		{
			return new Command((pm) => action(pm));
		}
		public static Command Create(Action action)
		{
			return new Command((pm) => action());
		}


		public static Command CreateListViewCommand<T>(Func<T, Task> action)
		{
			return new Command(async (q) => 
			{
				var qrow = (T)(((ItemTapCommandContext)q).Item);
				await action(qrow);
			});
		}

		public static void ChangeAllCanExecute(object viewmodel)
		{
			var props = viewmodel.GetType().GetProperties().Where(q => q.PropertyType.Equals(typeof(Command))).ToArray();
			var commands = props.Select(q => (Command)q.GetValue(viewmodel)).ToArray();
			commands.ForEach(q => q.ChangeCanExecute());
		}

	}
}
