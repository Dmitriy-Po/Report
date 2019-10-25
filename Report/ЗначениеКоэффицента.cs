using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    class ЗначениеКоэффицента
    {
        public ЗначениеКоэффицента(decimal c, string y, int id)
        {
            Коэффицент = c;
            КалендарныйГод = y;
            this.id = id;            
        }
        public int id { get; set; }
        public decimal Коэффицент { get; set; }
        public string КалендарныйГод { get; set; }
        public Classes.FormEducation ФормаОбучения { get; set; }
    }
}
