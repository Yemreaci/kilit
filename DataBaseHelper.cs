using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp2
{
    public class Device
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string IpAddress { get; set; }
    }
    public class DataBaseHelper
    {
        private SQLiteConnection database;

        public DataBaseHelper()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "devices.db3");
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Device>();
        }

        public void AddDevice(Device device)
        {
            database.Insert(device);
        }

        public List<Device> GetAllDevices()
        {
            return database.Table<Device>().ToList();
        }
    }
}
