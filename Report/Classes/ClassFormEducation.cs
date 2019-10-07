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
        public FormEducation(int id, string s)
        {
            this.id = id;
            Desc = s;
        }
        public int id { get; set; }
        public string Desc { get; set; }
        public string FullDesc { get; set; }
        public static void Fill(List<FormEducation> List)
        {
            SQliteDB db = new SQliteDB();
            SQLiteDataReader reader = db.Select("ФормаОбучения");

            while (reader.Read())
            {
                List.Add(new FormEducation(Convert.ToInt32(reader[0]), reader[1].ToString()));
            }
        }
    }
}
