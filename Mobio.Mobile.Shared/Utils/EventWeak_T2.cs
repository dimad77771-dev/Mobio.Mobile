using System;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace OneBuilder.Mobile
{
	public class EventWeak<T, S>
	{
		public WeakReference targetObject;
		public MethodInfo methodInfo;
		public Object methodParm1;
		public Object methodParm2;
		public int methodParmCount;

		enum RunModes { Sync, Async };
		RunModes RunMode;


		public EventWeak(Func<T, S, Task> arg, T parm1, S parm2)
		{
			targetObject = new WeakReference(arg.Target);
			methodInfo = arg.Method;

			methodParmCount = 2;
			methodParm1 = parm1;
			methodParm2 = parm2;
			RunMode = RunModes.Async;
		}

		public Task RunAsync()
		{
			if (this.RunMode != RunModes.Async) throw new ArgumentException("sync/async mode error");
			if (!targetObject.IsAlive) throw new ArgumentException("targetObject is not Alive");
			var obj = targetObject.Target;

			object[] parms = null;
			if (methodParmCount == 2)
			{
				parms = new object[] { methodParm1, methodParm2 };
			}
			else throw new ArgumentException("methodParmCount error");

			var ret = methodInfo.Invoke(obj, parms);
			if (ret is Task == false) throw new ArgumentException("return is not Task");
			return (Task)ret;
		}
	}
}






