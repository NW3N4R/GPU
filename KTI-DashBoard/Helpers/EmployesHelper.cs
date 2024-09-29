using KTI_DashBoard.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    class EmployesHelper
    {
        public static List<employeesModel> _employees = new List<employeesModel>();

        public static async Task<List<employeesModel>> GetEmployees()
        {
            using (SqlCommand cmd = new SqlCommand("select  * from employes", DbConnectionHelper.con))
            {
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    _employees.Clear();
                    while (await reader.ReadAsync())
                    {
                        var employee = new employeesModel
                        {
                            id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Suspended = reader.GetBoolean(2),

                        };
                        _employees.Add(employee);
                    }

                }
            }
            return _employees;
        }
        public static async Task<bool> AddEmploye(employeesModel employees)
        {
            using (SqlCommand cmd = new SqlCommand("insert into employes (FullName,suspended)values(@Name,@Sus)", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@Name", employees.Name);
                cmd.Parameters.AddWithValue("@Sus", false);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
        public static async Task<bool> UpdateEmployees(employeesModel employees)
        {
            using (SqlCommand cmd = new SqlCommand("update employes set FullName = @Name where id =@id",DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@Name", employees.Name);
                cmd.Parameters.AddWithValue("@id", employees.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
        public static async Task<bool> SuspendEmployee(employeesModel employees)
        {
            using (SqlCommand cmd = new SqlCommand("update employes set suspended = @sus where id =@id", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@sus", !employees.Suspended);
                cmd.Parameters.AddWithValue("@id", employees.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
    }
}
