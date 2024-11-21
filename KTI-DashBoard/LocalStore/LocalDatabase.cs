using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace KTI_DashBoard.LocalStore
{
    public class LocalDatabase
    {
        private static readonly string DatabaseFileName = "LocalDatabase.db";
        public readonly SQLiteConnection _conn;
        public LocalDatabase()
        {
            _conn = CreateConnectionAsync();
        }
        public SQLiteConnection CreateConnectionAsync()
        {
            string localFolderPath = ApplicationData.Current.LocalFolder.Path;
            string databasePath = Path.Combine(localFolderPath, DatabaseFileName);

            // Ensure the database file exists
            if (!File.Exists(databasePath))
            {
                File.Create(databasePath).Close();
            }

            SQLiteConnection con = new SQLiteConnection($"Data Source={databasePath}; Version = 3; New = True; Compress = True;");
            con.Open();
            return con;
        }
        public async void CreateJobTable()
        {
            SQLiteCommand cmd = new SQLiteCommand("create table IF NOT EXISTS Jobs (id INTEGER PRIMARY KEY AUTOINCREMENT, JobName text,Directory text)", _conn);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
