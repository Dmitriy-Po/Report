﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Report
{
    class КорректирующиеКоэффиценты
    {
        ЗначениеКоэффицента Значение_к;

        public КорректирующиеКоэффиценты () { }
        public КорректирующиеКоэффиценты(int код, decimal значениекоэффицента, Classes.FormEducation формаобучения, string год)
        {            
            Значение_к = new ЗначениеКоэффицента(значениекоэффицента, год, код);
            Значение_к.ФормаОбучения = формаобучения;
        }
        public static void Fill(List<КорректирующиеКоэффиценты> list)
        {
            string q = "SELECT "+
                "КорректирующиеКоэффиценты.Наименование as '1', "+
                "КорректирующиеКоэффиценты.ПолноеНаименование as '2', "+
                "КорректирующиеКоэффиценты.Уточнение as '3', "+
                "КорректирующиеКоэффиценты.Комментарий as '4', "+
                "КорректирующиеКоэффиценты.СтудентИнвалид as '5', "+
                "ЗначениеКоэффицента.код as id_k, "+                
                "ЗначениеКоэффицента.Значение as '6', "+
                "ЗначениеКоэффицента.КаледндарныйГод as '7',"+                
                "ФормаОбучения.код as '8', "+
                "ФормаОбучения.наименование as 'Форма Обучения', "+
                "КорректирующиеКоэффиценты.код as '9' " +
                
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
                list.Add(new КорректирующиеКоэффиценты(Convert.ToInt32(reader["id_k"]),
                    Convert.ToDecimal(reader["6"]),
                    
                    new Classes.FormEducation(
                        Convert.ToInt32(reader["8"]), reader["Форма Обучения"].ToString()), reader["7"].ToString())
                {
                    id                  = Convert.ToInt32(reader["9"]),
                    Наименование        = reader["1"].ToString(),
                    ПолноеНаименование  = reader["2"].ToString(),
                    Уточнение           = reader["3"].ToString(),
                    Комментарий         = reader["4"].ToString(),
                    СтудентИнвалид      = Convert.ToBoolean(reader["5"])
                });                                
            }
            db.ConnectionDB.Close();
        }

        public int id { get; set; }
        public int GetIdCoeff () => Значение_к.id;
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
