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
        public SQLiteDataReader Insert (string name_table, int[] num)
        {
            //задача  - корректно собрать строку SQL
            // строка создаётся по формату, требуется протестировать запрос sql.
            StringBuilder commnadtext = new StringBuilder();
            commnadtext.AppendFormat("INSERT INTO {0}(стуктурное_подразделение_ВК, специальность_ВК, квалификация_ВК, очное, очно_заочное, заочное, год, студент_инвалид)"+
                "VALUES ({1}, {2}, {3})", name_table, num[0], num[1], num[2]);

            //string s = "INSERT INTO "+name_table+ "(стуктурное_подразделение_ВК, специальность_ВК, квалификация_ВК, очное, очно_заочное, заочное, год, студент_инвалид)"
            //    +" VALUES ("+  + ")";
            

            SQLiteCommand comand = new SQLiteCommand(commnadtext.ToString(), conn);           

            conn.Open();
            SQLiteDataReader reader = comand.ExecuteReader();
            return reader;
        }
    }
}
