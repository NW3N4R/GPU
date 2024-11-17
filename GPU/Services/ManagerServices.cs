using GPU.Helpers;
using GPU.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GPU.Services
{
    public class ManagerServices
    {
        public static List<UsersModel> _Users = new List<UsersModel>();
        public static List<UsersAccessToDepsModel> _auths = new List<UsersAccessToDepsModel>();
        public static List<employeesModel> _employees = new List<employeesModel>();
        public static async Task LoadManagers()
        {
            using (SqlCommand cmd = new SqlCommand("SelectManagers", DbConnectionHelper.con))
            {
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    _Users.Clear();
                    _auths.Clear();
                    _employees.Clear();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {

                            UsersModel usersModel = new UsersModel
                            {
                                id = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Password = reader.GetString(2),
                                EMPID = reader.GetInt32(3),
                                CanDelete = reader.GetBoolean(4),
                            };
                            _Users.Add(usersModel);
                        }
                    }
                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employeesModel em = new employeesModel
                            {
                                id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                StuList = reader.GetBoolean(2),
                                ArchList = reader.GetBoolean(3),
                            };
                            _employees.Add(em);
                        }
                    }
                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var model = new UsersAccessToDepsModel
                            {

                                id = reader.GetInt32(0),
                                usrid = reader.GetInt32(1),
                                depid = reader.GetInt32(2),
                            };
                            _auths.Add(model);
                        }
                    }
                }
            }
        }
    }
}
