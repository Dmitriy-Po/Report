using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    class ЗначениеКоэффицента
    {
        public ЗначениеКоэффицента(decimal c, string y)
        {
            Коэффицент = c;
            КалендарныйГод = y;            
        }
        public decimal Коэффицент { get; set; }
        public string КалендарныйГод { get; set; }
        public Classes.FormEducation ФормаОбучения { get; set; }
    }
}
