using GPU.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace GPU.Helpers
{
    public class Helper_StudentContactInfo
    {
        public static ObservableCollection<StudentContactInfo> _Contacts = new ObservableCollection<StudentContactInfo>();
        public static async Task<ObservableCollection<StudentContactInfo>> GetContacts(string queary)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = queary;
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _Contacts.Clear();
                    while (await rd.ReadAsync())
                    {
                        var contact = new StudentContactInfo
                        {
                            Id = rd.GetInt32(0),
                            Phone = rd.GetString(1),
                            StudentEmail = rd.GetString(2),
                            Province = rd.GetString(3),
                            City = rd.GetString(4),
                            District = rd.GetString(5),
                            Village = rd.GetString(6),
                            SID = rd.GetInt32(7),
                        };
                        _Contacts.Add(contact);
                    }

                }
            }
            return _Contacts;
        }
    }
}
