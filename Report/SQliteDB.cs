using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class SQliteDB

    {
        //метод для переноса данных их БД в колелкцию
        public void DataBaseToList ()
        {            
            
        }
        public SQLiteConnection conn = new SQLiteConnection("Data Source=ReportDB.db; Version=3;");
        public SQLiteDataReader Select (string name_table)
        {            
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM "+name_table, conn);            

            conn.Open();
            SQLiteDataReader r = cmd.ExecuteReader();
            
            return r;                               
        }
        public SQLiteDataReader Insert (string name_table, List<TableFilial> list)
        {
            string s = "INSERT INTO "+name_table+ "(стуктурное_подразделение_ВК, специальность_ВК, квалификация_ВК, очное, очно_заочное, заочное, год, студент_инвалид)"
                +" VALUES ("+ list[0].full_desc +")";
                        

            SQLiteCommand comand = new SQLiteCommand(s, conn);           

            conn.Open();
            SQLiteDataReader reader = comand.ExecuteReader();
            return reader;
        }
    }
}
