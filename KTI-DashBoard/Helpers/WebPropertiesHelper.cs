using KTI_DashBoard.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace KTI_DashBoard.Helpers
{
    public class WebPropertiesHelper
    {
        public static List<WebProperties> _Martial;
        public static List<WebProperties> _Province;
        public static List<WebProperties> _EduAdmini;
        public static List<WebProperties> _Nationality;
        public static List<WebProperties> _Religion;
        public static List<WebProperties> _Department;
        public static async Task GetAllProps()
        {
            _Martial = new List<WebProperties>();
            _Province = new List<WebProperties>();
            _EduAdmini = new List<WebProperties>();
            _Nationality = new List<WebProperties>();
            _Religion = new List<WebProperties>();
            _Department = new List<WebProperties>();
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
                                    if (!_Martial.Contains(model))
                                    {
                                        _Martial.Add(model);
                                    }
                                    break;
                                case 2:
                                    if (!_Province.Contains(model))
                                    {
                                        _Province.Add(model);
                                    }
                                    break;
                                case 3:
                                    if (!_EduAdmini.Contains(model))
                                    {
                                        _EduAdmini.Add(model);
                                    }
                                    break;
                                case 4:
                                    if (!_Nationality.Contains(model))
                                    {
                                        _Nationality.Add(model);
                                    }
                                    break;
                                case 5:
                                    if (!_Religion.Contains(model))
                                    {
                                        _Religion.Add(model);
                                    }
                                    break;
                                case 6:
                                    if (!_Department.Contains(model))
                                    {
                                        _Department.Add(model);
                                    }
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
                cmd.CommandText = $"insert into {tbl} (Name,isActive)values(@name,@isactive); select scope_identity();";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@isactive", true);

                int id = (int)(decimal)await cmd.ExecuteScalarAsync();
                if (id > 0)
                {
                    var web = new WebProperties();
                    web.id = id;
                    web.Name = name;
                    web.isActive = true;
                    if (tbl.ToLower().Contains("prov"))
                    {
                        _Province.Add(web);
                    }
                    else if (tbl.ToLower().Contains("martial"))
                    {
                        _Martial.Add(web);
                    }
                    else if (tbl.ToLower().Contains("educationadmini"))
                    {
                        _EduAdmini.Add(web);
                    }
                    else if (tbl.ToLower().Contains("national"))
                    {
                        _Nationality.Add(web);
                    }
                    else if (tbl.ToLower().Contains("religion"))
                    {
                        _Religion.Add(web);
                    }
                    else if (tbl.ToLower().Contains("depart"))
                    {
                        _Department.Add(web);
                    }
                }
                return id > 0;
            }
        }
        public static async Task<bool> UpdatePropStatus(string tbl, int id, bool status)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"update {tbl} set isActive = @isactive where id =@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@isactive", status);

                int rf = await cmd.ExecuteNonQueryAsync();
                if (rf > 0)
                {
                }
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
