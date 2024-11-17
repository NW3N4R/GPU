using GPU.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace GPU.Helpers
{
    public class Helper_Invoice
    {
        public static async Task NewInvoice(InvoiceInfo info)
        {
            bool isSuccess = false;

            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"insert into invoiceinfo (invoiceID,InvoiceDate,Amount,Sid)values(@InvoiceID,@Date,@Amount,@sid)";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@sid", info.SID);


                int rf = await cmd.ExecuteNonQueryAsync();
                isSuccess = rf > 0;
            }


            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var model = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(model);
        }
        public static async Task UpdateInvoice(InvoiceInfo info)
        {
            bool isSuccess = false;

            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"update invoiceinfo set invoiceID=@InvoiceID,InvoiceDate=@Date,Amount=@Amount where id =@id";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@id", info.id);

                int rf = await cmd.ExecuteNonQueryAsync();
                isSuccess = rf > 0;
            }
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var model = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(model);
        }
        public static async Task arNewInvoice(InvoiceInfo info)
        {
            bool isSuccess = false;

            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"insert into ar_invoiceinfo (invoiceID,InvoiceDate,Amount,Sid)values(@InvoiceID,@Date,@Amount,@sid)";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@sid", info.SID);


                int rf = await cmd.ExecuteNonQueryAsync();
                isSuccess = rf > 0;
            }
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var model = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(model);
        }
        public static async Task arUpdateInvoice(InvoiceInfo info)
        {
            bool isSuccess = false;

            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"update ar_invoiceinfo set invoiceID=@InvoiceID,InvoiceDate=@Date,Amount=@Amount where id =@id";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@id", info.id);


                int rf = await cmd.ExecuteNonQueryAsync();
                isSuccess = rf > 0;
            }
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var model = new InfoModel
            {
                UserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                isSuccess = isSuccess,
                CountDown = 0
            };
            DbConnectionHelper.Infos.Add(model);
        }
    }
}
