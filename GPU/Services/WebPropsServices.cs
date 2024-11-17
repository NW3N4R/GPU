using GPU.Helpers;
using GPU.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Net.Http;
namespace GPU.Services
{
    public class WebPropsServices
    {
        public static List<WebProperties> MergedProps = new List<WebProperties>();
        public static async Task LoadProps(int id = 0, string Table = "-", bool isUpdate = false)
        {
            if (!isUpdate)
            {
                using (SqlCommand cmd = new SqlCommand("SelectWebprops", DbConnectionHelper.con))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("table", Table);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        MergedProps.Clear();
                        while (await reader.ReadAsync())
                        {
                            var model = new WebProperties
                            {
                                id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                isActive = reader.GetBoolean(2),
                                propType = reader.GetString(3),
                            };
                            MergedProps.Add(model);
                        }
                    }

                }
            }
            else
            {
                await Update(id,Table);
            }
        }
        public static async Task Update(int id,string tableName)
        {
            DeleteProp(id, tableName);
            await LoadProps(id, tableName);
        }
        public static void DeleteProp(int id, string propType)
        {
            var proptoRemove = MergedProps.First(x => x.id == id && x.propType.ToLower() == propType.ToLower());
            if (proptoRemove != null)
            {
                MergedProps.Remove(proptoRemove);
            }
        }
    }
}
