using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class TableFilial
    {
        public int id { get; set; }
        public string desc { get; set; }
        public string full_desc { get; set; }
        public static List<TableFilial> Fill(List<TableFilial> mylist)
        {
            SQliteDB sql = new SQliteDB();
            SQLiteDataReader r = sql.Select("Filial");

            while (r.Read())
            {
                mylist.Add(new TableFilial()
                {
                    id = Convert.ToInt32(r[0]),
                    desc = Convert.ToString(r[1]),
                    full_desc = Convert.ToString(r[2])
                });
            }
            r.Close();
            return mylist;            
        }

    }      
}
