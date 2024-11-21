using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using System.Data.SQLite;
using System.Data.Entity;
using System.Linq;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KTI_DashBoard.LocalStore
{
    class BackupJobModel : INotifyPropertyChanged
    {
        public int JobID { get; set; }
        string _jobName;
        public string JobName
        {
            get
            {
                return _jobName;
            }
            set
            {
                if (_jobName != value)
                {
                    _jobName = value;
                    OnPropertyChanged(nameof(JobName));
                }
            }
        }

        string _Directory;
        public string Directory
        {
            get
            {
                return _Directory;
            }
            set
            {
                if (_Directory != value)
                {
                    _Directory = value;
                    OnPropertyChanged(nameof(Directory));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class CrudWithJobs
    {
        public List<BackupJobModel> _BackupJobs;
        LocalDatabase _Database;
        public CrudWithJobs()
        {
            _BackupJobs = new List<BackupJobModel>();
            _Database = new LocalDatabase();
            _Database.CreateJobTable();
        }
        public async Task<List<BackupJobModel>> ReadAll()
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from jobs", _Database._conn);
            SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var model = new BackupJobModel
                {
                    JobID = reader.GetInt32(0),
                    JobName = reader.GetString(1),
                    Directory = reader.GetString(2)
                };
                _BackupJobs.Add(model);
            }

            return _BackupJobs;
        }
        public async Task AddAsync(BackupJobModel backupJob)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into jobs (JobName,Directory)values(@JobName,@Directory)", _Database._conn);
            cmd.Parameters.AddWithValue("@JobName", backupJob.JobName);
            cmd.Parameters.AddWithValue("@Directory", backupJob.Directory);
            int rf = await cmd.ExecuteNonQueryAsync();
            if (rf > 0)
            {
                long lastInsertedId = _Database._conn.LastInsertRowId;
                backupJob.JobID = rf;
                _BackupJobs.Add(backupJob);
            }
        }

        public async Task DeleteAsync(int id)
        {
            SQLiteCommand cmd = new SQLiteCommand("delete from jobs where id = @id", _Database._conn);
            cmd.Parameters.AddWithValue("@id", id);
            int rf = await cmd.ExecuteNonQueryAsync();
            var row = _BackupJobs.First(x => x.JobID == id);
            _BackupJobs.Remove(row);
        }
    }
}
