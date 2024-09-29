using KTI_DashBoard.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    internal class UsersAccessToDepsHelper
    {
        public static List<UsersAccessToDepsModel> _auths = new List<UsersAccessToDepsModel>();

        public static async Task<List<UsersAccessToDepsModel>> getAccess()
        {
            using (SqlCommand cmd = new SqlCommand("select  * from userAccessToDep", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _auths.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new UsersAccessToDepsModel
                        {
                            id = rd.GetInt32(0),
                            usrid = rd.GetInt32(1),
                            depid = rd.GetInt32(2),
                        };
                        _auths.Add(model);
                    }
                }
            }
            return _auths;
        }


        public static async Task<bool> setAccess(UsersAccessToDepsModel model)
        {
            if (_auths.Any(x => x.usrid == model.usrid && x.depid == model.depid))
            {
                return false;
            }

            using (SqlCommand cmd = new SqlCommand("insert into userAccessToDep(usrid,depid)values(@usrid,@depid)", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@usrid", model.usrid);
                cmd.Parameters.AddWithValue("@depid", model.depid);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }

        public static async Task<bool> DeleteAccess(int id)
        {
            using (SqlCommand cmd = new SqlCommand("delete from userAccessToDep where id =@id", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
    }
}
