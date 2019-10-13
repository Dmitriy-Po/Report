using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Report
{
    class КорректирующиеКоэффиценты
    {
        ЗначениеКоэффицента Значение_к;

        public КорректирующиеКоэффиценты () { }
        public КорректирующиеКоэффиценты(decimal значениекоэффицента, Classes.FormEducation формаобучения, string год)
        {            
            Значение_к = new ЗначениеКоэффицента(значениекоэффицента, год);
            Значение_к.ФормаОбучения = формаобучения;
        }
        public static void Fill(List<КорректирующиеКоэффиценты> list)
        {
            string q = "SELECT "+
                "КорректирующиеКоэффиценты.Наименование, "+
                "КорректирующиеКоэффиценты.ПолноеНаименование, "+
                "КорректирующиеКоэффиценты.Уточнение,"+
                "КорректирующиеКоэффиценты.Комментарий,"+
                "КорректирующиеКоэффиценты.СтудентИнвалид,"+                
                "ЗначениеКоэффицента.Значение, "+
                "ЗначениеКоэффицента.КаледндарныйГод,"+                
                "ФормаОбучения.код, "+
                "ФормаОбучения.наименование as 'Форма Обучения', "+
                "КорректирующиеКоэффиценты.код " +
                
                "FROM ЗначениеКоэффицента "+
                "INNER JOIN КорректирующиеКоэффиценты ON "+
                "ЗначениеКоэффицента.Корректирующие_ВК = КорректирующиеКоэффиценты.код "+
                "INNER JOIN ФормаОбучения ON "+
                "ЗначениеКоэффицента.ФормаОбучения_ВК = ФормаОбучения.код";

            SQliteDB db = new SQliteDB();            

            SQLiteCommand Command = new SQLiteCommand(q, db.ConnectionDB);
            db.ConnectionDB.Open();


            SQLiteDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {                
                list.Add(new КорректирующиеКоэффиценты(Convert.ToDecimal(reader[5]),
                    new Classes.FormEducation(
                        Convert.ToInt32(reader[7]), reader[8].ToString()), reader[6].ToString())
                {
                    id = Convert.ToInt32(reader[9]),
                    Наименование = reader[0].ToString(),
                    ПолноеНаименование = reader[1].ToString(),
                    Уточнение = reader[2].ToString(),
                    Комментарий = reader[3].ToString(),
                    СтудентИнвалид = Convert.ToBoolean(reader[4])
                });                                
            }
            db.ConnectionDB.Close();
        }

        public int id { get; set; }
        public decimal GetCoef() => Значение_к.Коэффицент;
        public string GetYear() => Значение_к.КалендарныйГод;
        public string GetForm() => Значение_к.ФормаОбучения.Desc;
        public string Наименование { get; set; }
        public string ПолноеНаименование { get; set; }
        public string Уточнение { get; set; }
        public string Комментарий { get; set; }
        public bool СтудентИнвалид { get; set; }
    }
}
