using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CinemaController
{
    public static class StaticData
    {
        public static int user;
        public static string role;
        public static SqlConnection connection = new SqlConnection("server=DAUN;database=Машанка;trusted_connection=true");
        public static string login;
        public static Frame Frame;
        public static MainWindow CurrentMainWindow;
    }
}
