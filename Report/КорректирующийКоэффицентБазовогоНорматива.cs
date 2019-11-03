using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class КорректирующийКоэффицентБазовогоНорматива
    {
        List<КорректирующиеКоэффиценты> correct_coef = new List<КорректирующиеКоэффиценты>();
        public КорректирующийКоэффицентБазовогоНорматива(string year)
        {
            КалендарныйГод = year; 
        }        
        
        public КорректирующийКоэффицентБазовогоНорматива ()
        {            
            КалендарныйГод = default(string);
        }
        public void SetCorrectCoef (КорректирующиеКоэффиценты k)
        {
            correct_coef.Add(k);
        }
        public List<КорректирующиеКоэффиценты> GetCorrectCoef () => correct_coef;

        string КалендарныйГод { get; set; }
    }
}
