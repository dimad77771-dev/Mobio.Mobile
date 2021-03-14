//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Linq.Expressions;
//using System.Collections;
//using System.Globalization;
//using System.Reflection;
//using System.Threading.Tasks;
//using OneBuilder.Mobile.ViewModels;
//using System.Diagnostics;

//namespace OneBuilder.Mobile
//{
//	public class MessagingCenter<TArgs>
//	{
//		private List<EventWeak<TArgs>> alist = new List<EventWeak<TArgs>>();

//		public void Subscribe(Func<TArgs, Task> callback)
//		{
//			var action = new EventWeak<TArgs>(callback);
//			alist.Add(action);
//		}

//		public async Task Send(TArgs arg)
//		{
//			var arr = alist.ToArray();
//			//Debug.WriteLine("-------------------------------");
//			//foreach (var action in arr)
//			//{
//			//	Debug.WriteLine("======");
//			//	Debug.WriteLine(action.IsAlive);
//			//	Debug.WriteLine(action.taginfo);
//			//	if (action.IsAlive)
//			//	{
//			//		Debug.WriteLine(action.methodParmCount);
//			//		Debug.WriteLine(action.methodParm);
//			//		Debug.WriteLine(action.methodParm.GetType().FullName);
//			//		Debug.WriteLine(action.methodInfo.Name);
//			//	}
//			//}
//			//Debug.WriteLine("-------------------------------");

//			foreach (var action in arr)
//			{
//				if (action.IsAlive)
//				{
//					await action.RunAsync(arg);
//				}
//				else
//				{
//					alist.Remove(action);
//				}
//			}
//		}

//	}


//	public class MessagingCenters
//	{
//		public static MessagingCenter<MessageCloseView> CloseView = new MessagingCenter<MessageCloseView>();
//		public static MessagingCenter<MessageRowUpdate> BoxesItems = new MessagingCenter<MessageRowUpdate>();
//	}

//	public class MessageCloseView
//	{
//		public MessageCloseView(PageViewModel closedViewModel)
//		{
//			ClosedViewModel = closedViewModel;
//		}

//		public PageViewModel ClosedViewModel { get; set; }
//	}

//	//public static class MessageCloseViewExtensions
//	//{
//	//	public static void SubscribeOnClosed<T>(this T viewmodel, Func<T, Task> callback) where T : PageViewModel
//	//	{
//	//		MessagingCenters.CloseView.Subscribe(async (q) =>
//	//		{
//	//			if (q.ClosedViewModel == viewmodel)
//	//			{
//	//				await callback((T)q.ClosedViewModel);
//	//			}
//	//		});
//	//	}
//	//}

//	public class MessageRowUpdate
//	{
//		public Guid RowId { get; set; }
//		public RowChangeTypes RowChangeType { get; set; }

//		public MessageRowUpdate(Guid rowId, RowChangeTypes rowChangeType)
//		{
//			this.RowId = RowId;
//			this.RowChangeType = rowChangeType;
//		}

//		public enum RowChangeTypes { Insert, Update, Delete };
//	}

	

//}