using KTI_DashBoard.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Security.Cryptography.Certificates;

namespace KTI_DashBoard.Helpers
{
    internal class UsersHelper
    {
        public static List<UsersModel> _Users = new List<UsersModel>();
        public static async Task<List<UsersModel>> GetUsers()
        {
            using (SqlCommand cmd = new SqlCommand("select  * from users", DbConnectionHelper.con))
            {
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    _Users.Clear();

                    while (await reader.ReadAsync())
                    {
                        var model = new UsersModel
                        {
                            id = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            EMPID = reader.GetInt32(3),

                        };
                        _Users.Add(model);
                    }
                }
            }
            return _Users;
        }
        public static async Task<bool> AddUser(UsersModel model)
        {
            bool isDup = _Users.Any(x => x.EMPID == model.EMPID);
            if (isDup)
                return false;

            using (SqlCommand cmd = new SqlCommand("insert into users (UserName,Password,EMPID)values(@usrName,@Pass,@EMP)",DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@usrName", model.UserName);
                cmd.Parameters.AddWithValue("@Pass", model.Password);
                cmd.Parameters.AddWithValue("@EMP", model.EMPID);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
        public static async Task<bool> UpdateUser(UsersModel model)
        {
            using (SqlCommand cmd = new SqlCommand("Update users set UserName = @usrName,Password = @Pass where id =@id", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@usrName", model.UserName);
                cmd.Parameters.AddWithValue("@Pass", model.Password);
                cmd.Parameters.AddWithValue("@id", model.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
    }
}
