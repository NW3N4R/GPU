using GPU.Models;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace GPU.Helpers
{
    public class Helper_Invoice
    {
        public static ObservableCollection<InvoiceInfo> _Invoices = new ObservableCollection<InvoiceInfo>();
        public static ObservableCollection<InvoiceInfo> ar_Invoices = new ObservableCollection<InvoiceInfo>();
        public static async Task GetInvoices()
        {
            using (SqlCommand cmd = new SqlCommand("select * from InvoiceInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    _Invoices.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new InvoiceInfo
                        {
                            id = rd.GetInt32(0),
                            InvoiceId = rd.GetString(1),
                            InvoiceDate = rd.GetString(2),
                            Amount = decimal.Parse(rd.GetString(3)).ToString("N0"),
                            SID = rd.GetInt32(4),
                        };
                        _Invoices.Add(model);
                    }
                }
            }
        }
        public static async Task ar_GetInvoices()
        {
            using (SqlCommand cmd = new SqlCommand("select * from ar_InvoiceInfo", DbConnectionHelper.con))
            {
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    ar_Invoices.Clear();
                    while (await rd.ReadAsync())
                    {
                        var model = new InvoiceInfo
                        {
                            id = rd.GetInt32(0),
                            InvoiceId = rd.GetString(1),
                            InvoiceDate = rd.GetString(2),
                            Amount = decimal.Parse(rd.GetString(3)).ToString("N0"),
                            SID = rd.GetInt32(4),
                        };
                        ar_Invoices.Add(model);
                    }
                }
            }
        }
        public static async Task NewInvoice( InvoiceInfo info)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"insert into invoiceinfo (invoiceID,InvoiceDate,Amount,Sid)values(@InvoiceID,@Date,@Amount,@sid)";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@sid", info.SID);


                await cmd.ExecuteNonQueryAsync();

                await GetInvoices();
            }
        }
        public static async Task UpdateInvoice(InvoiceInfo info)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"update invoiceinfo set invoiceID=@InvoiceID,InvoiceDate=@Date,Amount=@Amount where id =@id";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@id", info.id);

                await cmd.ExecuteNonQueryAsync();
                await GetInvoices();
            }
        }

        public static async Task arNewInvoice(InvoiceInfo info)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"insert into ar_invoiceinfo (invoiceID,InvoiceDate,Amount,Sid)values(@InvoiceID,@Date,@Amount,@sid)";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@sid", info.SID);


                await cmd.ExecuteNonQueryAsync();

                await ar_GetInvoices();
            }
        }
        public static async Task arUpdateInvoice(InvoiceInfo info)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = $"update ar_invoiceinfo set invoiceID=@InvoiceID,InvoiceDate=@Date,Amount=@Amount where id =@id";

                cmd.Parameters.AddWithValue("@InvoiceID", info.InvoiceId);
                cmd.Parameters.AddWithValue("@Date", info.InvoiceDate);
                cmd.Parameters.AddWithValue("@Amount", info.Amount);
                cmd.Parameters.AddWithValue("@id", info.id);

                await cmd.ExecuteNonQueryAsync();
                await ar_GetInvoices();
            }
        }
    }
}
