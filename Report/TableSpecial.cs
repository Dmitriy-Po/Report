using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Report
{
    public class TableSpecial
    {
        public int id { get; set; }
        public string desc { get; set; }
        public static List<TableSpecial> Fill(List<TableSpecial> mylist)
        {
            SQliteDB sql = new SQliteDB();
            SQLiteDataReader r = sql.Select("Специальности");

            while (r.Read())
            {
                mylist.Add(new TableSpecial()
                {
                    id = Convert.ToInt32(r[0]),
                    desc = Convert.ToString(r[1])                    
                });
            }
            r.Close();
            return mylist;
        }
    }
}
