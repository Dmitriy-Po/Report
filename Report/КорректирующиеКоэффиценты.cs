using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Report
{
    public class КорректирующиеКоэффиценты
    {
        List<ЗначениеКоэффицента> Значение_к { get; set; }
        List<КорректирующийКоэффицентБазовогоНорматива> kkbn = new List<КорректирующийКоэффицентБазовогоНорматива>();


        public КорректирующиеКоэффиценты () { }
        public КорректирующиеКоэффиценты (int id, string desc)
        {
            this.id = id;
            this.Наименование = desc;
        }
        public КорректирующиеКоэффиценты(int id, string desc, string detail)
        {
            this.id = id;
            this.Наименование = desc;
            this.Уточнение = detail;
        }

        public КорректирующиеКоэффиценты(string desc, string fullDesc, string detail, string comment, bool std_inv)
        {
            Наименование        = desc;
            ПолноеНаименование  = fullDesc;
            Уточнение           = detail;
            Комментарий         = comment;
            СтудентИнвалид      = std_inv;

            Значение_к = new List<ЗначениеКоэффицента>();
            
        }
        public static void Fill(List<КорректирующиеКоэффиценты> list)
        {
            string q = "SELECT КорректирующиеКоэффиценты.код, КорректирующиеКоэффиценты.Наименование FROM КорректирующиеКоэффиценты";

            SQliteDB db = new SQliteDB();
            using (SQLiteConnection connection = new SQLiteConnection(db.ConnectionDB))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(q, connection);
                DataTable table = new DataTable();

                adapter.Fill(table);

                foreach (DataRow row in table.AsEnumerable())
                {
                    list.Add(new КорректирующиеКоэффиценты(Convert.ToInt32(row[0]), row[1].ToString()));
                }
            }


            #region OLD
            //while (reader.Read())
            //{                
            //    list.Add(new КорректирующиеКоэффиценты(/*Convert.ToInt32(reader["id_k"]),*/
            //        Convert.ToDecimal(reader["6"]),

            //        new Classes.FormEducation(
            //            Convert.ToInt32(reader["8"]), reader["Форма Обучения"].ToString()), reader["7"].ToString())
            //    {
            //        id                  = Convert.ToInt32(reader["9"]),
            //        Наименование        = reader["1"].ToString(),
            //        ПолноеНаименование  = reader["2"].ToString(),
            //        Уточнение           = reader["3"].ToString(),
            //        Комментарий         = reader["4"].ToString(),
            //        СтудентИнвалид      = Convert.ToBoolean(reader["5"])
            //    });                                
            //}
            //db.ConnectionDB.Close();
            #endregion
        }
        public void SetValueCoef (ЗначениеКоэффицента val)
        {
            Значение_к.Add(val);
        }
        public List<ЗначениеКоэффицента> GetValueCoef () => Значение_к;

        public void SetCorrectCoef (КорректирующийКоэффицентБазовогоНорматива kk)
        {
            kkbn.Add(kk);
        }
        public List<КорректирующийКоэффицентБазовогоНорматива> GetCorrectCoef () => kkbn;


        public int id { get; set; }
        //public int GetIdCoeff () => Значение_к.id;
        //public decimal GetCoef() => Значение_к.Коэффицент;
        //public string GetYear() => Значение_к.КалендарныйГод;
        //public string GetForm() => Значение_к.ФормаОбучения.Desc;
        public string Наименование { get; set; }
        public string ПолноеНаименование { get; set; }
        public string Уточнение { get; set; }
        public string Комментарий { get; set; }
        public bool СтудентИнвалид { get; set; }
    }
}
