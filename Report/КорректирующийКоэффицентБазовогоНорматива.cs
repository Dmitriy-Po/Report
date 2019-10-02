using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    class КорректирующийКоэффицентБазовогоНорматива
    {
        public КорректирующиеКоэффиценты КорректирующиеКоэффицент { get; set; }

        public КорректирующийКоэффицентБазовогоНорматива(string year)
        {
            КалендарныйГод = year;
            //КорректирующиеКоэффиценты = new КорректирующиеКоэффиценты(koef);
        }

        public КорректирующийКоэффицентБазовогоНорматива ()
        {            
            КалендарныйГод = default(string);
        }
        string КалендарныйГод { get; set; }
    }
}
