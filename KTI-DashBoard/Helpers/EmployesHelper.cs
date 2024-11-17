using KTI_DashBoard.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KTI_DashBoard.Helpers
{
    class EmployesHelper
    {
        public static ObservableCollection<employeesModel> _employees = new ObservableCollection<employeesModel>();

        public static async Task<ObservableCollection<employeesModel>> GetEmployees()
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
                            StuList = reader.GetBoolean(2),
                            ArchList = reader.GetBoolean(3),

                        };
                        string stu = employee.StuList ? "Y" : "N";
                        string arch = employee.ArchList ? "Y" : "N";
                        employee.stuListContent = $"Student List Auth {stu}";
                        employee.ArchListContent = $"Archive List Auth {arch}";
                        _employees.Add(employee);
                    }

                }
            }
            return _employees;
        }
        public static async Task<bool> AddEmploye(employeesModel employees)
        {
            using (SqlCommand cmd = new SqlCommand("insert into employes (FullName,StuList,ArchList)values(@Name,@stu,@arch)", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@Name", employees.Name);
                cmd.Parameters.AddWithValue("@stu", false);
                cmd.Parameters.AddWithValue("@arch", false);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
        public static async Task<bool> UpdateEmployees(employeesModel employees)
        {
            using (SqlCommand cmd = new SqlCommand("update employes set FullName = @Name where id =@id", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@Name", employees.Name);
                cmd.Parameters.AddWithValue("@id", employees.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
        public static async Task<bool> EmployeeStuAuth(employeesModel employees)
        {
            using (SqlCommand cmd = new SqlCommand("update employes set StuList = @Stu where id =@id", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@Stu", !employees.StuList);
                cmd.Parameters.AddWithValue("@id", employees.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
        public static async Task<bool> EmployeeArchAuth(employeesModel employees)
        {
            using (SqlCommand cmd = new SqlCommand("update employes set  ArchList = @Arch where id =@id", DbConnectionHelper.con))
            {
                cmd.Parameters.AddWithValue("@Arch", !employees.ArchList);
                cmd.Parameters.AddWithValue("@id", employees.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                return rf > 0;
            }
        }
    }
}
