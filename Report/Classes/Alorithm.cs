using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Classes
{
    class Algorithm
    {
        SQLiteDataAdapter Adapter;
        DataSet Dataset;
        SQLiteConnection connection;


        public Algorithm () { }
        public void Calculate (int year)
        {
            SQliteDB DB = new SQliteDB();
            using (connection = new SQLiteConnection(DB.ConnectionDB))
            {
                connection.Open();

                string query = 
                                // Table 0.
                                "SELECT Filial.id as 'Филиал_код', " +
                                "Filial.full_desc as 'Филиал', " +
                                "Специальности.наименование as 'Специальность', " +
                                "Квалификации.наименование as 'Квалификация',  " +
                                "ЧисленностьОбучающихся.очное as 'Очное', " +
                                "ЧисленностьОбучающихся.очно_заочное as 'Очно-заочное', " +
                                "ЧисленностьОбучающихся.заочное as 'Заочное', " +
                                "SUBSTR(ЧисленностьОбучающихся.год, 1, 4) as 'год', " +
                                "ЧисленностьОбучающихся.студент_инвалид " +
                                "FROM ЧисленностьОбучающихся " +
                                "JOIN Filial ON ЧисленностьОбучающихся.стуктурное_подразделение_ВК = Filial.id " +
                                "JOIN Специальности ON ЧисленностьОбучающихся.специальность_ВК = Специальности.код " +
                                "JOIN Квалификации  ON ЧисленностьОбучающихся.квалификация_ВК = Квалификации.код " +
                                $"WHERE ЧисленностьОбучающихся.год LIKE '{year}%'; " +
                                
                                // Table 1.
                                "SELECT ЗначениеКоэффицента.Значение FROM ЗначениеКоэффицента "+
                                "JOIN КорректирующиеКоэффиценты ON КорректирующиеКоэффиценты.код = ЗначениеКоэффицента.Корректирующие_ВК "+
                                "JOIN КоррКоэффицентБазовогоНорматива ON КоррКоэффицентБазовогоНорматива.Корр_коэфф_ВК = ЗначениеКоэффицента.Корректирующие_ВК "+
                                $"WHERE КоррКоэффицентБазовогоНорматива.Базовый_норматив_ВК = 75; ";

                Adapter = new SQLiteDataAdapter(query, connection);
                Adapter.Fill(Dataset);
                
                
                // Заполнение колекции Численности обучающихся.
                List<TableCountStudent> ListStudent = new List<TableCountStudent>();
                foreach (DataRow item in Dataset.Tables[0].AsEnumerable())
                {
                    ListStudent.Add(new TableCountStudent(
                                        Convert.ToInt32(item[0]),
                                        Convert.ToString(item[1]),
                                        Convert.ToString(item[2]),
                                        Convert.ToString(item[3]),

                                        Convert.ToInt32(item[4]),
                                        Convert.ToInt32(item[5]),
                                        Convert.ToInt32(item[6]),
                                        Convert.ToInt32(item[7]), 
                                        Convert.ToBoolean(item[8])
                                   ));
                }
                // Конец заполнения коллекции Численности обучающихся.
                
                


            }
        }
    }
}
