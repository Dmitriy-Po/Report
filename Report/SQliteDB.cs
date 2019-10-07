using System.Data.SQLite;
using System.Linq;
using System.Text;


namespace Report
{
    public class SQliteDB

    {        
        
        public SQLiteConnection ConnectionDB = new SQLiteConnection("Data Source=ReportDB.db; Version=3;");
        public SQLiteDataReader Select (string name_table)
        {            
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM "+name_table, ConnectionDB);            
            /*добавить ограничение на выборку элементов из бд, равной 10.*/
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
            FormListCountStudent form = new FormListCountStudent();
            var id_skill    = form.ListSkill.Where(x => x.desc == table.Skill).Select(x => x).ToList();
            var id_special  = form.ListSpecial.Where(x => x.desc == table.Special).Select(x => x).ToList();
            var id_filial   = form.ListFilial.Where(x => x.full_desc == table.Filial).Select(x => x).ToList();

            SQLiteCommand command = new SQLiteCommand
                ("UPDATE ЧисленностьОбучающихся SET " +
                $"заочное = {table.zaochnoe}, " +
                $"очно_заочное = {table.ochno_zaocjnoe}, " +
                $"очное = {table.ochnoe}, " +
                $"студент_инвалид = {table.student_inv}, " +
                $"год = {table.year}, " +
                $"квалификация_ВК = {id_skill[0].id}, " +
                $"специальность_ВК = {id_special[0].id}, " +
                $"стуктурное_подразделение_ВК = {id_filial[0].id} " +
                $"WHERE код = {table.id}", ConnectionDB);

            ConnectionDB.Open();
            command.ExecuteNonQuery();
            ConnectionDB.Close();
        }
        public string GetConnection () => ConnectionDB.ConnectionString;
        
    }
}
