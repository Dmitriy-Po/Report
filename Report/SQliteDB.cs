using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report
{
    public class SQliteDB

    {        
        
        public SQLiteConnection ConnectionDB = new SQLiteConnection("Data Source=ReportDB.db; Version=3;");
        public SQLiteDataReader Select (string name_table)
        {            
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM "+name_table, ConnectionDB);            

            ConnectionDB.Open();
            SQLiteDataReader r = cmd.ExecuteReader();
            
            return r;                               
        }
        public void Insert (string name_table, int[] num)
        {
            //задача  - корректно собрать строку SQL
            // строка создаётся по формату, требуется протестировать запрос sql.
            StringBuilder commnadtext = new StringBuilder();
            FormListCountStudent fa = new FormListCountStudent();
            commnadtext.AppendFormat("INSERT INTO {0}(стуктурное_подразделение_ВК, специальность_ВК, квалификация_ВК, очное, очно_заочное, заочное, год, студент_инвалид)"+
                "VALUES ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", 
                name_table, num[0], num[1], num[2], num[3], num[4], num[5], num[6], num[7]);

            //string s = "INSERT INTO "+name_table+ "(стуктурное_подразделение_ВК, специальность_ВК, квалификация_ВК, очное, очно_заочное, заочное, год, студент_инвалид)"
            //    +" VALUES ("+  + ")";
            

            SQLiteCommand comand = new SQLiteCommand(commnadtext.ToString(), ConnectionDB);           

            ConnectionDB.Open();
            SQLiteDataReader reader;
            reader = comand.ExecuteReader();
            //return reader;
            ConnectionDB.Close();
           
        }
        public void Delete(string id)
        {
            SQLiteCommand command = new SQLiteCommand
                ($"DELETE FROM ЧисленностьОбучающихся WHERE ЧисленностьОбучающихся.код in({id})", ConnectionDB);
            ConnectionDB.Open();
            command.ExecuteNonQuery();
            ConnectionDB.Close();
        }
        public void Update(TableCountStudent table)
        {
            SQLiteCommand command = new SQLiteCommand
                ("UPDATE ЧисленностьОбучающихся SET " +
                $"заочное = {table.zaochnoe}," +
                $"очно_заочное = {table.ochno_zaocjnoe}," +
                $"очное = {table.ochnoe}," +
                $"студент_инвалид = {table.ochnoe}," +
                $"год = {table.year}," +
                $"квалификация_ВК = {table.Skill}," +
                $"специальность_ВК = {table.Special}," +
                $"стуктурное_подразделение_ВК = {table.Filial} " +
                $"WHERE код = {table.id}", ConnectionDB);

            ConnectionDB.Open();
            command.ExecuteNonQuery();
            ConnectionDB.Close();
        }
        
    }
}
