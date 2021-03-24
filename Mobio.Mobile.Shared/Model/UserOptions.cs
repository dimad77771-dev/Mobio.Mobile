using OneBuilder.Mobile;
using OneBuilder.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneBuilder.Model
{
	public class UserOptions
    {
		[PrimaryKey]
		public Guid RowId { get; set; }
		public Guid CurrentLabourRowId { get; set; }
		public String CurrentLabourName { get; set; }
		public String Username { get; set; }
        public String Password { get; set; }
        public Guid UserRowId { get; set; }
        public Guid DeviceId { get; set; }
		public Int32 UpgradeVersionNum { get; set; }
        public Guid BusinessRowId { get; set; }
		public String BusinessCode { get; set; }
		public Int32? LastServerNumber { get; set; }
		public Guid CurrentMashineRowId { get; set; }
		public String DeviceName { get; set; }
		public Boolean ShowPrices { get; set; }
		public bool IsOfflineMode { get; set; }

		public string CurrentLocaleId { get; set; }
		public Guid UserProfileRowId { get; set; }


		public UserOptions()
		{
			RowId = Guid.NewGuid();
		}
		public UserOptions(Boolean hasInit)
		{
		}

        public static UserOptions GetCurrent()
        {
            return DB.TableUserOptions.FirstOrDefault();
		}

		public static void Clear()
		{
			var brow = GetCurrent();
			if (brow != null)
			{
				brow.BusinessRowId = default(Guid);
				DB.Update(brow);
			}
		}

		public static bool Exists()
		{
			return DB.TableUserOptions.Any(q => q.BusinessRowId != default(Guid));
		}

		public static void Init()
        {
            if (GetCurrent() == null)
            {
				var row = new UserOptions
				{
					RowId = Guid.NewGuid(),
					DeviceId = Guid.NewGuid(),
					DeviceName = U.GetDeviceName(),
					BusinessRowId = default(Guid),
				};
				DB.Insert(row);
            }
        }

		public static void Reset()
		{
			DB.DeleteAll<UserOptions>();
			Init();
		}


		public static String GetUser()
        {
            return (UserOptions.GetCurrent().Username ?? "");
        }

        public static String GetPassword()
        {
            return (UserOptions.GetCurrent().Password ?? "");
        }

        public static Guid GetUserRowId()
        {
            return UserOptions.GetCurrent().UserRowId;
        }

        public static Guid GetDeviceId()
        {
            return UserOptions.GetCurrent().DeviceId;
        }

		public static Guid GetMashineRowId()
		{
			return GetCurrent().CurrentMashineRowId;
		}


        public static void SetUsernamePassword(String username, String password, Guid userProfileRowId)
        {
            var row = GetCurrent();
            row.Username = username;
            row.Password = password;
            row.UserProfileRowId = userProfileRowId;
            DB.Update(row);
        }

		public static void SetBusinessInfo(Guid businessRowId, String businessCode)
		{
			var row = GetCurrent();
			row.BusinessRowId = businessRowId;
			row.BusinessCode = businessCode;
			DB.Update(row);
		}

		public static void SetLabourInfo(Guid labourRowId, String labourName)
		{
			var row = GetCurrent();
			row.CurrentLabourRowId = labourRowId;
			row.CurrentLabourName = labourName;
			DB.Update(row);
		}

		public static Guid GetBusinessRowId()
		{
			return GetCurrent().BusinessRowId;
		}

		public static string GetLocaleId()
		{
			return GetCurrent().CurrentLocaleId;
		}
		public static void SetLocaleId(string localeId)
		{
			var row = GetCurrent();
			row.CurrentLocaleId = localeId;
			DB.Update(row);
		}

		public static Guid GetUserProfileRowId()
		{
			return GetCurrent().UserProfileRowId;
		}



		public static Boolean GetShowPrices()
        {
            return UserOptions.GetCurrent().ShowPrices;
        }
        public static void SetShowPrices(Boolean value)
        {
            var row = UserOptions.GetCurrent();
			row.ShowPrices = value;
			DB.Update(row);
        }


		public static Boolean IsOffline
		{
			get
			{
				return UserOptions.GetCurrent().IsOfflineMode;
			}
		}
		public static Boolean IsOnline
		{
			get
			{
				return (!UserOptions.GetCurrent().IsOfflineMode);
			}
		}


		public static string GetDeviceName()
		{
			return GetCurrent().DeviceName;
		}

		public static string BusinessWebString()
		{
			return "businessrowid=" + GetCurrent().BusinessRowId;
		}

	}
}
