using KTI_DashBoard.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KTI_DashBoard.Helpers
{
    internal class UsersHelper
    {
        public static ObservableCollection<UsersModel> _Users = new ObservableCollection<UsersModel>();
        public static async Task<ObservableCollection<UsersModel>> GetUsers()
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
                            CanDelete = reader.GetBoolean(4),

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

            using (SqlCommand cmd = new SqlCommand("insert into users (UserName,Password,EMPID,CanDelete)values(@usrName,@Pass,@EMP,@CanDelete); select scope_identity();", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@usrName", model.UserName);
                cmd.Parameters.AddWithValue("@Pass", model.Password);
                cmd.Parameters.AddWithValue("@EMP", model.EMPID);
                cmd.Parameters.AddWithValue("@CanDelete", model.CanDelete);
                int rf = (int)(decimal)await cmd.ExecuteScalarAsync();
                if (rf > 0)
                {
                    model.id = rf;
                }
                _Users.Add(model);
                return rf > 0;
            }
        }
        public static async Task<bool> UpdateUser(UsersModel model)
        {
            using (SqlCommand cmd = new SqlCommand("Update users set UserName = @usrName,Password = @Pass , CanDelete = @CanDelete where id =@id", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@usrName", model.UserName);
                cmd.Parameters.AddWithValue("@Pass", model.Password);
                cmd.Parameters.AddWithValue("@id", model.id);
                cmd.Parameters.AddWithValue("@CanDelete", model.CanDelete);
                int rf = await cmd.ExecuteNonQueryAsync();
                if (rf > 0)
                {
                    var existing = _Users.First(x => x.id == model.id);
                    existing.UserName = model.UserName;
                    existing.Password = model.Password;
                    existing.CanDelete = model.CanDelete;
                    existing.EMPID = model.EMPID;

                }
                return rf > 0;
            }
        }
    }
}
