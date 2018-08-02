using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CooperationApp.Data
{
    public class DbSQLite
    {

        public static SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection("Coorporation.db");
        }

    }
}
