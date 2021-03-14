using System;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace OneBuilder.Mobile
{
	public class EventWeak
	{
		public WeakReference targetObject;
		public MethodInfo methodInfo;
		public Object methodParm;
		public int methodParmCount;

		enum RunModes { Sync, Async };
		RunModes RunMode;

		public EventWeak(Action arg)
		{
			targetObject = new WeakReference(arg.Target);
			methodInfo = arg.Method;

			methodParmCount = 0;
			RunMode = RunModes.Sync;
		}

		public EventWeak(Func<Task> arg)
		{
			targetObject = new WeakReference(arg.Target);
			methodInfo = arg.Method;

			methodParmCount = 0;
			RunMode = RunModes.Async;
		}


		public void Run(object sender = null, EventArgs e = null)
		{
			if (this.RunMode != RunModes.Sync) throw new ArgumentException("sync/async mode error");
			if (!targetObject.IsAlive) throw new ArgumentException("targetObject is not Alive");
			var obj = targetObject.Target;

			object[] parms = null;
			if (methodParmCount == 0)
			{
				parms = new object[0];
			}
			else if (methodParmCount == 1)
			{
				parms = new object[] { methodParm };
			}
			else throw new ArgumentException("methodParmCount error");

			methodInfo.Invoke(obj, parms);
		}


		public Task RunAsync()
		{
			if (this.RunMode != RunModes.Async) throw new ArgumentException("sync/async mode error");
			if (!targetObject.IsAlive) throw new ArgumentException("targetObject is not Alive");
			var obj = targetObject.Target;

			object[] parms = null;
			if (methodParmCount == 0)
			{
				parms = new object[0];
			}
			else if (methodParmCount == 1)
			{
				parms = new object[] { methodParm };
			}
			else throw new ArgumentException("methodParmCount error");

			var ret = methodInfo.Invoke(obj, parms);
			if (ret is Task == false) throw new ArgumentException("return is not Task");
			return (Task)ret;
		}


		//public static EventHandler Get(Action arg)
		//{
		//    var proxy = new EventWeak(arg);
		//    var ev = new EventHandler(proxy.Run);
		//    return ev;
		//}
		////public static EventHandler Get(Action<Object> arg, object parm)
		////{
		////    var proxy = new EventWeak(arg, parm);
		////    var ev = new EventHandler(proxy.Run);
		////    return ev;
		////}
		//public static EventHandler Get<T>(Action<T> arg, T parm)
		//{
		//    var proxy = new EventWeak<T>(arg, parm);
		//    var ev = new EventHandler(proxy.Run);
		//    return ev;
		//}


	}
}






