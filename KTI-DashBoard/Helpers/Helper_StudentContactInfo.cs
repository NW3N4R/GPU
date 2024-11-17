using KTI_DashBoard.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    public class Helper_StudentContactInfo
    {
        public static List<StudentContactInfo> _Contacts = new List<StudentContactInfo>();
        public static List<StudentContactInfo> ar_Contacts = new List<StudentContactInfo>();
        public static async Task<List<StudentContactInfo>> GetContacts()
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
        public static async Task<List<StudentContactInfo>> ar_GetContacts()
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
