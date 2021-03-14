using OneBuilder.Mobile;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Sqlite
{
	public static class DbUtil
	{
		public static void ExplainSql(string sql0)
		{
			var sql = "EXPLAIN QUERY PLAN " + sql0;
			var infos = DB.Query<ExplainSqlInfo>(sql);

			var val = "\n\n-------------------------\nEXPLAIN QUERY PLAN:\n" + sql0 + "\n";
			foreach(var info in infos.OrderBy(q => q.selectid).ThenBy(q => q.order).ThenBy(q => q.from))
			{
				val += $"selectid:{info.selectid}\n" + $"order:{info.order}\n" + $"from:{info.from}\n" + $"detail:{info.detail}\n";
			}
			val += "\n-------------------------\n";
			U.Log(val);
		}

		class ExplainSqlInfo
		{
			public int selectid { get; set; }
			public int order { get; set; }
			public int from { get; set; }
			public string detail { get; set; }
		}


		public static T FirstOrDefault<T>(this TableQuery<T> tableQuery, Expression<Func<T, bool>> predExpr)
		{
			var ret = tableQuery.Where(predExpr).FirstOrDefault();
			return ret;
		}

		public static T First<T>(this TableQuery<T> tableQuery, Expression<Func<T, bool>> predExpr)
		{
			var ret = tableQuery.Where(predExpr).First();
			return ret;
		}

		public static T SingleOrDefault<T>(this TableQuery<T> tableQuery, Expression<Func<T, bool>> predExpr)
		{
			var ret = tableQuery.Where(predExpr).SingleOrDefault();
			return ret;
		}
	
		public static T Single<T>(this TableQuery<T> tableQuery, Expression<Func<T, bool>> predExpr)
		{
			var ret = tableQuery.Where(predExpr).Single();
			return ret;
		}

		public static bool Any<T>(this TableQuery<T> tableQuery, Expression<Func<T, bool>> predExpr)
		{
			var ret = tableQuery.Where(predExpr).Any();
			return ret;
		}

	}
}
