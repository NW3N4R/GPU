using KTI_DashBoard.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KTI_DashBoard.Helpers
{
    public class WebPropertiesHelper
    {
        public static List<WebProperties> _Martial = new List<WebProperties>();
        public static List<WebProperties> _Province = new List<WebProperties>();
        public static List<WebProperties> _EduAdmini = new List<WebProperties>();
        public static List<WebProperties> _Nationality = new List<WebProperties>();
        public static List<WebProperties> _Religion = new List<WebProperties>();
        public static List<WebProperties> _Department = new List<WebProperties>();

        public static async Task GetAllProps()
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                List<string> tbls = new List<string>();
                tbls.Add("MartialStatus");
                tbls.Add("Province");
                tbls.Add("EducationAdministrator");
                tbls.Add("Nationality");
                tbls.Add("Religion");
                tbls.Add("Department");
                int index = 0;
                _Martial.Clear();
                _Province.Clear();
                _EduAdmini.Clear();
                _Nationality.Clear();
                _Religion.Clear();
                _Department.Clear();
                foreach (var item in tbls)
                {

                  
                    if (index > 6)
                    {
                        break;
                    }
                    index++;
                    cmd.CommandText = $"select * from {item}";
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var model = new WebProperties
                            {
                                id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                isActive = reader.GetBoolean(2),
                            };

                            switch (index)
                            {
                                case 1:
                                    _Martial.Add(model);
                                    break;
                                case 2:
                                    _Province.Add(model);
                                    break;
                                case 3:
                                    _EduAdmini.Add(model);
                                    break;
                                case 4:
                                    _Nationality.Add(model);
                                    break;
                                case 5:
                                    _Religion.Add(model);
                                    break;
                                case 6:
                                    _Department.Add(model);
                                    break;
                                default:
                                    break;

                            }


                        }
                    }
                }
            }
        }

        public static async Task<bool> addProperties(string tbl, string name)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"insert into {tbl} (Name,isActive)values(@name,@isactive)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@isactive", true);

                await cmd.ExecuteNonQueryAsync();
            }
            return true;
        }
        public static async Task<bool> UpdatePropStatus(string tbl,int id, bool status)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"update {tbl} set isActive = @isactive where id =@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@isactive", status);

                await cmd.ExecuteNonQueryAsync();
            }
            return true;
        }

        public static async Task<bool> UpdateProp(string tbl, int id, string name)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"update {tbl} set Name = @Name where id =@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Name", name);

                await cmd.ExecuteNonQueryAsync();
            }
            return true;
        }

    }
}
