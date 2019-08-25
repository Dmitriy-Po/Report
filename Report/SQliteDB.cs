using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class SQliteDB
    {
        public SQLiteDataReader Select (string name_table)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=ReportDB.db; Version=3;");
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM "+name_table, conn);            

            conn.Open();
            SQLiteDataReader r = cmd.ExecuteReader();
            return r;
            //try
            //{                
            //    return r;
            //}
            //catch (SQLiteException ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message);
            //}
            //conn.Close();                      
        }
    }
}
