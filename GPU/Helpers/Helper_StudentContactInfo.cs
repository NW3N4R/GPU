using GPU.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace GPU.Helpers
{
    public class Helper_StudentContactInfo
    {
        public static ObservableCollection<StudentContactInfo> _Contacts = new ObservableCollection<StudentContactInfo>();
        public static ObservableCollection<StudentContactInfo> ar_Contacts = new ObservableCollection<StudentContactInfo>();
        public static async Task<ObservableCollection<StudentContactInfo>> GetContacts()
        {
            using (SqlCommand cmd = new SqlCommand("select * from StudentContactInfo", DbConnectionHelper.con))
            {
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
        public static async Task<ObservableCollection<StudentContactInfo>> ar_GetContacts()
        {
            using (SqlCommand cmd = new SqlCommand("select * from ar_StudentContactInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    ar_Contacts.Clear();
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
                        ar_Contacts.Add(contact);
                    }

                }
            }
            return _Contacts;
        }
    }
}
