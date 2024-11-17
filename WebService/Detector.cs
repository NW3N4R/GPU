using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class Detector
    {
        [SqlFunction]
        public static void SendMessage(SqlString message)
        {
            SqlContext.Pipe.Send("Message received: " + message.ToString());
        }
    }
}
