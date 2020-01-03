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
        public КорректирующийКоэффицентБазовогоНорматива (int id_bnz, decimal values_kk, int id_form)
        {
            this.id_bnz = id_bnz;
            this.value = values_kk;
            this.id_form_education = id_form;
        }
        public void SetCorrectCoef (КорректирующиеКоэффиценты k)
        {
            correct_coef.Add(k);
        }
        public List<КорректирующиеКоэффиценты> GetCorrectCoef () => correct_coef;

        string КалендарныйГод { get; set; }
        public int id_bnz { get; set; }
        public decimal value { get; set; }
        public int id_form_education { get; set; }
    }
}
