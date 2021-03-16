using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using OneBuilder.Model;
using System.Linq.Expressions;
using SQLite;
using OneBuilder.Mobile;

namespace OneBuilder.Sqlite
{
	public class DB
	{
		static object locker = new object();

		public SQLiteConnection database;

		public DB(SQLiteConnection conn)
		{
			database = conn;
			Current = database;
			Build();
		}

		public static void Build()
		{
			var database = Current;
			//if (U.IsDebug)
			//{
			//	//database.TraceListener = new DBLogger();
			//	database.Trace = true;
			//	database.Tracer = (q) => U.Log4("SQL=" + q);
			//}

			DB.CreateTable<UserOptions>();



			UserOptions.Init();
			//UserOptions.SetBusinessInfo(new Guid("1F19A836-DDC9-459B-B96F-10A5D7BA4446"), "ABC");
			//DBCach.Init();
			//database.Execute("update UserOptions set NetworkRestriction = 1 where NetworkRestriction is null"); //"Default" property in model class don't work normal

			//var aaa = DB.Table<StockItem>().Count();


			//DBZDebugData.Run(database);

			Globalization.DbInit();

		}

		public static SQLiteConnection Current { get; set; }
		public static List<Type> AllTables = new List<Type>();

		public static TableQuery<T> Table<T>() where T : new()
		{
			return Current.Table<T>();
        }


		public static int Update(Object obj)
		{
			return Current.Update(obj);
		}

		public static int Insert(Object obj)
		{
			U.Log("Insert=" + obj.GetType().FullName);
			//U.Log("Insert=" + );
			return Current.Insert(obj);
		}


		public static int Delete(Object obj)
		{
			return Current.Delete(obj);
		}

		public static int DeleteAll<T>()
		{
			return Current.DeleteAll<T>();
		}


		public static int InsertOrReplace(Object obj)
		{
			return Current.InsertOrReplace(obj);
		}

		public static int InsertAll(IEnumerable objs)
		{
			return Current.InsertAll(objs);
		}




		public static List<T> Query<T>(string query, params object[] args) where T : new()
		{
			return Current.Query<T>(query, args);
		}

		public static int Execute(string query, params object[] args)
		{
			return Current.Execute(query, args);
		}

		public static T ExecuteScalar<T>(string query, params object[] args)
		{
			return Current.ExecuteScalar<T>(query, args);
		}

		public static void CreateTable<T>(CreateFlags createFlags = CreateFlags.None)
		{
			Current.CreateTable(typeof(T), createFlags);
			AllTables.Add(typeof(T));
		}


		public static void LockForRead()
		{
			lockDatabaseObject.EnterReadLock();
		}
		public static void UnlockForRead()
		{
			lockDatabaseObject.ExitReadLock();
		}

		public static void LockForWrite()
		{
			lockDatabaseObject.EnterWriteLock();
		}
		public static void UnlockForWrite()
		{
			lockDatabaseObject.ExitWriteLock();
		}
		

		public static void BeginTransaction()
		{
			lockDatabaseObject.EnterWriteLock();
            Current.BeginTransaction();
        }

		public static void Commit()
		{
			Current.Commit();
            ExitWriteLock();
        }

		public static void Rollback()
		{
			Current.Rollback();
            ExitWriteLock();
        }

		private static void ExitWriteLock()
		{
			if (lockDatabaseObject.IsWriteLockHeld)
			{
				lockDatabaseObject.ExitWriteLock();
			}
		}


        //static object lockDatabaseObject2 = new object();
		public static ReaderWriterLockSlim lockDatabaseObject = new ReaderWriterLockSlim();

		public static LockReadHolder ReadLocker
		{
			get
			{
				return new LockReadHolder();
			}
		}
		public class LockReadHolder : IDisposable
		{
			bool alreadyReadLockHeld;

			public LockReadHolder()
			{
				if (DB.lockDatabaseObject.IsReadLockHeld)
				{
					alreadyReadLockHeld = true;
					return;
				}
				DB.LockForRead();
			}

			public void Dispose()
			{
				if (alreadyReadLockHeld)
				{
					return;
				}
				DB.UnlockForRead();
			}
		}
		

		public static TableQuery<UserOptions> TableUserOptions
		{
			get
			{
				return DB.Table<UserOptions>();
			}
		}
    }
}
