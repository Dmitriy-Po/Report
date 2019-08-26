using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Report
{
    public class TableSKill
    {
        public int id { get; set; }
        public string desc { get; set; }
        public string full_desc { get; set; }
        public static List<TableSKill> Fill(List<TableSKill> mylist)
        {
            SQliteDB sql = new SQliteDB();
            SQLiteDataReader r = sql.Select("Квалификации");

            while (r.Read())
            {
                mylist.Add(new TableSKill()
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
