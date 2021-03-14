using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class FormatFunc
	{
		public static String MoneyFormat = "$###,###,###,###,##0.00";
		public static String DateShortFormat = "MM/dd/yyyy";
		public static String UnitFormat = "################0.#####";
		public static String QtyFormat = "################0.#####";


		public static String FormatMoney(this Decimal self)
		{
			return self.ToString(MoneyFormat);
		}
		public static String FormatMoney(this Decimal? self)
		{
			return self == null ? "" : ((Decimal)self).ToString(MoneyFormat);
		}

		public static String FormatUnit(this Decimal self)
		{
			return self.ToString(UnitFormat);
		}
		public static String FormatUnit(this Decimal? self)
		{
			return self == null ? "" : ((Decimal)self).ToString(UnitFormat);
		}

		public static String FormatQty(this Decimal self)
		{
			return self.ToString(QtyFormat);
		}
		public static String FormatQty(this Decimal? self)
		{
			return self == null ? "" : ((Decimal)self).ToString(QtyFormat);
		}


		public static String FormatShort(this DateTime self)
		{
			return self.ToString(DateShortFormat);
		}
		public static String FormatShort(this DateTime? self)
		{
			return self == null ? "" : ((DateTime)self).ToString(DateShortFormat);
		}

		public static String FormatDate(this DateTime self)
		{
			return self.ToString(DateShortFormat);
		}
		public static String FormatDate(this DateTime? self)
		{
			return self == null ? "" : ((DateTime)self).ToString(DateShortFormat);
		}


		//public static String FormatDateTime(this DateTime dat)
		//{
		//	return dat.ToString("d") + ", " + dat.ToString("t");
		//}
		//public static String FormatDateTime(this DateTime? arg)
		//{
		//	return arg == null ? "" : FormatDateTime((DateTime)arg);
		//}

		public static String FormatHHMM(this DateTime dat)
		{
			return dat.ToString(@"hh\:mm");
		}
		public static String FormatHHMM(this DateTime? arg)
		{
			return arg == null ? "" : FormatHHMM((DateTime)arg);
		}


		public static String FormatDateWithHHMM(this DateTime dat)
		{
			return dat.FormatDate() + " " + dat.FormatHHMM();
		}
		public static String FormatDateWithHHMM(this DateTime? arg)
		{
			return arg == null ? "" : FormatDateWithHHMM((DateTime)arg);
		}
		

		public static String DecimalPoint
		{
			get
			{
				var str = (12.34M).ToString();
				if (str.Length != 5) throw new Exception("Unknown decimal point");
				return str.Substring(2, 1);
			}
		}

		public static String Decimal2String(Decimal? arg)
		{
			if (arg == null)
			{
				return "";
			}
			return arg.ToString();
		}

		public static Decimal? String2Decimal(String arg)
		{
			Decimal ret;

			if (Decimal.TryParse(arg, out ret))
			{
				return ret;
			}
			else
			{
				return null;
			}
		}

	}
}
