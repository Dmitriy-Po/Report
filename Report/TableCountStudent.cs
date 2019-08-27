using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report
{
    public class TableCountStudent
    {
        public int id { get; set; }
        public string year { get; set; }
        public int ochnoe { get; set; }
        public int zaochnoe { get; set; }
        public int ochno_zaocjnoe { get; set; }
        public bool student_inv { get; set; }
        public int Filial { get; set; }
        public int Special { get; set; }
        public int Skill { get; set; }

       

    }
}
