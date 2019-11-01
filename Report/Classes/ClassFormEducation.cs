using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Classes
{
    public class FormEducation
    {
        List<ЗначениеКоэффицента> коэффицент { get; set; }
        public FormEducation(string description)
        {            
            Desc = description;
        }
        public void SetCorrectCoef(ЗначениеКоэффицента val)
        {
            коэффицент.Add(val);
        }
        public List<ЗначениеКоэффицента> GetCoef () => коэффицент;

        public int id { get; set; }
        public string Desc { get; set; }
        public string FullDesc { get; set; }
        public static void Fill(List<FormEducation> List)
        {
            SQliteDB db = new SQliteDB();
            SQLiteDataReader reader = db.Select("ФормаОбучения");

            while (reader.Read())
            {
                List.Add(new FormEducation(reader[1].ToString()));
            }
        }
    }
}
