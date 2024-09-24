﻿using GPU.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace GPU.Helpers
{
    public class Helper_StudentParentInfo
    {
        public static ObservableCollection<StudentParentInfo> _Parent = new ObservableCollection<StudentParentInfo>();
        public static ObservableCollection<StudentParentInfo> ar_Parent = new ObservableCollection<StudentParentInfo>();

        public static async Task GetParent()
        {
            using (SqlCommand cmd = new SqlCommand("select * from StudentParentInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _Parent.Clear();
                    while (await rd.ReadAsync())
                    {
                        var parent = new StudentParentInfo
                        {
                            Id = rd.GetInt32(0),
                            Name = rd.GetString(1),
                            Profession = rd.GetString(2),
                            Phone = rd.GetString(3),
                            Email = rd.GetString(4),
                            SID = rd.GetInt32(5),
                            CardInfoNo = rd.GetString(6),
                            CardInfoIssuePlace = rd.GetString(7)
                        };
                        _Parent.Add(parent);

                    }
                }
            }
        }
        public static async Task ar_GetParent()
        {
            using (SqlCommand cmd = new SqlCommand("select * from ar_StudentParentInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    ar_Parent.Clear();
                    while (await rd.ReadAsync())
                    {
                        var parent = new StudentParentInfo
                        {
                            Id = rd.GetInt32(0),
                            Name = rd.GetString(1),
                            Profession = rd.GetString(2),
                            Phone = rd.GetString(3),
                            Email = rd.GetString(4),
                            SID = rd.GetInt32(5),
                            CardInfoNo = rd.GetString(6),
                            CardInfoIssuePlace = rd.GetString(7)
                        };
                        ar_Parent.Add(parent);
                    }
                }
            }
        }
    }
}
