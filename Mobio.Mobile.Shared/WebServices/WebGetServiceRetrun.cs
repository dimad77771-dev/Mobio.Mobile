using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBuilder.WebServices
{
	public class WebGetServiceRetrun
	{
		public Boolean Success;
		public String Value;
		public Boolean ErrorNotFound;
	}

	public class WebGetBytesServiceRetrun
	{
		public byte[] Bytes;
		public Boolean Success;
		public String ErrorText;
		public Boolean ErrorNotFound;
	}

}
