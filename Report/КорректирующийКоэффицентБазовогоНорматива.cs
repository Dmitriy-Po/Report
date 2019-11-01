using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class КорректирующийКоэффицентБазовогоНорматива
    {        

        public КорректирующийКоэффицентБазовогоНорматива(string year)
        {
            КалендарныйГод = year; 
        }        
        
        public КорректирующийКоэффицентБазовогоНорматива ()
        {            
            КалендарныйГод = default(string);
        }
        string КалендарныйГод { get; set; }
    }
}
