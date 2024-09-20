using GPU.Models;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace GPU.Helpers
{
    public class Helper_Invoice
    {
        public static ObservableCollection<InvoiceInfo> _Invoices = new ObservableCollection<InvoiceInfo>();
        public static async Task GetInvoices(string queary)
        {
            using (SqlCommand cmd = new SqlCommand("", DbConnectionHelper.con))
            {
                cmd.CommandText = queary;
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
                            Amount = decimal.Parse( rd.GetString(3)).ToString("N0"),
                            SID = rd.GetInt32(4)
                        };
                        _Invoices.Add(model);
                    }
                }
            }
            //_Invoices.First().isFirst = true;
        }
    }
}
