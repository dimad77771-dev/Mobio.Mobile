using System;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace OneBuilder.Mobile
{
	public class EventWeak<T>
	{
		public WeakReference targetObject;
		public MethodInfo methodInfo;
		public Object methodParm;
		public int methodParmCount;
		public string taginfo;

		enum RunModes { Sync, Async };
		RunModes RunMode;

		public EventWeak(Action<T> arg, T parm)
		{
			targetObject = new WeakReference(arg.Target);
			methodInfo = arg.Method;

			methodParmCount = 1;
			methodParm = parm;
			RunMode = RunModes.Sync;
		}


		public EventWeak(Func<T, Task> arg, T parm)
		{
			targetObject = new WeakReference(arg.Target);
			methodInfo = arg.Method;

			methodParmCount = 1;
			methodParm = parm;
			RunMode = RunModes.Async;
		}

		public EventWeak(Func<T, Task> arg)
		{
			targetObject = new WeakReference(arg.Target);
			methodInfo = arg.Method;

			methodParmCount = 0;
			RunMode = RunModes.Async;

			taginfo = arg.Target.GetType().FullName + "---" + arg.Target.ToString();
		}

		public void Run(object sender = null, EventArgs e = null)
		{
			if (this.RunMode != RunModes.Sync) throw new ArgumentException("sync/async mode error");
			if (!targetObject.IsAlive) throw new ArgumentException("targetObject is not Alive");
			var obj = targetObject.Target;

			object[] parms = null;
			if (methodParmCount == 1)
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
			if (methodParmCount == 1)
			{
				parms = new object[] { methodParm };
			}
			else throw new ArgumentException("methodParmCount error");

			var ret = methodInfo.Invoke(obj, parms);
			if (ret is Task == false) throw new ArgumentException("return is not Task");
			return (Task)ret;
		}

		public Task RunAsync(T arg)
		{
			if (this.RunMode != RunModes.Async) throw new ArgumentException("sync/async mode error");
			if (!targetObject.IsAlive) throw new ArgumentException("targetObject is not Alive");
			var obj = targetObject.Target;

			object[] parms = null;
			if (methodParmCount == 0)
			{
				parms = new object[] { arg };
			}
			else throw new ArgumentException("methodParmCount error");

			var ret = methodInfo.Invoke(obj, parms);
			if (ret is Task == false) throw new ArgumentException("return is not Task");
			return (Task)ret;
		}

		public bool IsAlive
		{
			get
			{
				return targetObject.IsAlive;
			}
		}
	}
}






