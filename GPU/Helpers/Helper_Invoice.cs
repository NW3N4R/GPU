using GPU.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace GPU.Helpers
{
    public class Helper_Invoice
    {
        public static ObservableCollection<InvoiceInfo> _Invoices = new ObservableCollection<InvoiceInfo>();
        public static async Task GetSupports()
        {
            using (SqlCommand cmd = new SqlCommand("select * from invoiceInfo", DbConnectionHelper.con))
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
                            Amount = rd.GetString(3),
                            SID = rd.GetInt32(4)
                        };
                        _Invoices.Add(model);
                    }
                }
            }
        }
    }
}
