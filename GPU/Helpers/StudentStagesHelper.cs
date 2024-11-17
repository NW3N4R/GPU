using GPU.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Security.Claims;
namespace GPU.Helpers
{
    public class StudentStagesHelper
    {
        public static async Task AddNewStage(GPU.Models.StudentStages model, int ind = 0)
        {
            bool isSuccess = false;
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = ind == 0 ? "StageInsert" : "ArStageInsert";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Stage", model.Stage);
                cmd.Parameters.AddWithValue("@Year", model.Year);
                cmd.Parameters.AddWithValue("@Status", model.Status);
                cmd.Parameters.AddWithValue("@Sid", model.Sid);

                int rf = await cmd.ExecuteNonQueryAsync();
                isSuccess = rf > 0;

            }
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var info = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(info);
        }
        public static async Task EditStage(GPU.Models.StudentStages model, int ind = 0)
        {
            bool isSuccess = false;
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = ind == 0 ? "StageUpdate" : "ArStageUpdate";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Stage", model.Stage);
                cmd.Parameters.AddWithValue("@Year", model.Year);
                cmd.Parameters.AddWithValue("@Status", model.Status);
                cmd.Parameters.AddWithValue("@sid", model.Sid);
                cmd.Parameters.AddWithValue("@id", model.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                isSuccess = rf > 0;

            }
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var info = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(info);
        }
    }
}
