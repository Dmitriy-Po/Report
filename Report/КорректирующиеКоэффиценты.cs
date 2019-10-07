using System.Collections.Generic;
using System.Data.SQLite;

namespace Report
{
    class КорректирующиеКоэффиценты
    {
        ЗначениеКоэффицента Значение_к;
        public КорректирующиеКоэффиценты(decimal значениекоэффицента, Classes.FormEducation формаобучения, string год)
        {            
            Значение_к = new ЗначениеКоэффицента(значениекоэффицента, год);
            Значение_к.ФормаОбучения = формаобучения;
        }
        public void Fill(List<КорректирующиеКоэффиценты> list)
        {
            SQliteDB db = new SQliteDB();
            SQLiteDataReader reader = db.Select("КорректирующиеКоэффиценты");

            while (reader.Read())
            {
                list.Add(new КорректирующиеКоэффиценты())                
            }
        }

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
