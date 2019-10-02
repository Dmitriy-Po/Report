using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    class ЗначениеКоэффицента
    {
        public ЗначениеКоэффицента(decimal c)
        {
            Коэффицент = c;
        }
        public decimal Коэффицент { get; set; }
        public string КалендарныйГод { get; set; }
    }
}
