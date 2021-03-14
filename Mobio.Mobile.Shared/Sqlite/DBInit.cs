using OneBuilder.Mobile;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Sqlite
{
	static public class DBInit
	{
		static public void Init()
		{
			//TestCopyDB();
			var folder = U.GetLocalFilesDir();
			DatabaseFilename = "OneBuilder.db3";
			DatabasePath = Path.Combine(folder, DatabaseFilename);
			//File.Delete(DatabasePath);
			//File.Copy(DatabasePath, Path.Combine(folder, "123.db3"), true);
			//File.Copy(Path.Combine(folder, "123.db3"), DatabasePath, true);

			var conn = ConnectDB(DatabasePath);
			SetDatabaseConnection(conn);
		}


		private static bool IsRunSetDatabaseConnection = false;
		public static void SetDatabaseConnection(SQLiteConnection connection)
		{
			if (IsRunSetDatabaseConnection) return;
			var database = new DB(connection);
			IsRunSetDatabaseConnection = true;
		}

		public static void CloseDatabaseConnection()
		{
			if (!IsRunSetDatabaseConnection) return;
			DisconnectDB(DB.Current);
			IsRunSetDatabaseConnection = false;
		}


		static public SQLiteConnection ConnectDB(string path)
		{
			var conn = new SQLiteConnection(path, storeDateTimeAsTicks: true);

			conn.Execute("PRAGMA cache_size=20000");

			return conn;
		}


		static public void DisconnectDB(SQLiteConnection conn)
		{
			conn.Close();
			conn.Dispose();
		}


		static void TestCopyDB()
		{
#if __IOS__
			var dbname = "OneBuilderInventory2.db3";
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string database = Path.Combine(path, dbname);
			string fileName = "DBFile/" + dbname;
			string localHtmlUrl = Path.Combine(Foundation.NSBundle.MainBundle.BundlePath, fileName);
			//if (!File.Exists(database))
			{
				File.Copy(localHtmlUrl, database, true);
			}
#endif
		}


		static public string DatabasePath;
		static public string DatabaseFilename;
	}



	public static class DBUpgrade
	{
		public static void UpgradeAll()
		{
		}
	}
}
