using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    //КорректирующийКоэффицентБазовогоНорматива
    class BaseRatioCorrectionFactor
    {
        public BaseRatioCorrectionFactor ()
        {
            Year = default(string);
        }
         string Year { get; set; }
    }
}
