using Report.Classes;

namespace Report
{
    public class ЗначениеКоэффицента
    {
        public ЗначениеКоэффицента(decimal c, string y/*, int id*/)
        {
            Коэффицент = c;
            КалендарныйГод = y;
            //this.id = id;            
        }
        
        public int id { get; set; }
        public decimal Коэффицент { get; set; }
        public string КалендарныйГод { get; set; }
        
    }
}
