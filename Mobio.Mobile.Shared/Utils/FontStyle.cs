using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class FontStyle
	{
		public static Font F21
		{
			get
			{
				return Font.OfSize("Roboto", 15);
			}
		}

		public static Font F22
		{
			get
			{
				return Font.OfSize("Roboto", 18);
			}
		}

		public static Font F23
		{
			get
			{
				return Font.OfSize("Roboto", 22);
			}
		}

		public static Font F24
		{
			get
			{
				return Font.OfSize("Roboto", 24);
			}
		}

		public static Font F25
		{
			get
			{
				return Font.OfSize("Roboto", 30);
			}
		}

		public static Font labelFormTitle
		{
			get
			{
				return Font.OfSize("Segoe UI", (U.TabletMode ? 16 : 16));
			}
		}

		public static Font labelFormData
		{
			get
			{
				return Font.OfSize("Segoe UI", (U.TabletMode ? 16 : 16));
			}
		}

		public static Font labelListData
		{
			get
			{
				return Font.OfSize("Segoe UI", (U.TabletMode ? 14 : 14));
			}
		}

		public static Font labelListDataSmall
		{
			get
			{
				return Font.OfSize("Segoe UI", (U.TabletMode ? 12 : 12));
			}
		}

		public static Font labelTabControlStandart
		{
			get
			{
				return Font.OfSize("Segoe UI", (U.TabletMode ? 14 : 14));
			}
		}

		public static Font labelTabControlSmall
		{
			get
			{
				return Font.OfSize("Segoe UI", (U.TabletMode ? 12 : 12));
			}
		}

	}
}
