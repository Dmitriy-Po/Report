using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class БазовыеНормативыЗатрат
    {
        List<БазовыйНормативЗатратСтоимостнойГруппы> Норматив;
        List<КорректирующийКоэффицентБазовогоНорматива> КоррктирующийКоэфф;

        public БазовыеНормативыЗатрат ()
        { 
            //Норматив = new БазовыйНормативЗатратСтоимостнойГруппы(                
            //    new decimal[] {0.0m, 0.0m, 0.0m, 0.0m}, "");
            //Норматив.SetGroup("", "");
            //КоррктирующийКоэфф = new КорректирующийКоэффицентБазовогоНорматива();  
        }
          
        public БазовыеНормативыЗатрат(int id, string description)
        {
            this.id = id;
            Desc = description;
            Норматив            = new List<БазовыйНормативЗатратСтоимостнойГруппы>();
            КоррктирующийКоэфф  = new List<КорректирующийКоэффицентБазовогоНорматива>();                      
        }
        public void SetNormativ (БазовыйНормативЗатратСтоимостнойГруппы n)
        {
            Норматив.Add(n);
        }
        public List<БазовыйНормативЗатратСтоимостнойГруппы> GetNormativ () => Норматив;
        
        public void SetCorrectCoef (КорректирующийКоэффицентБазовогоНорматива kk)
        {
            КоррктирующийКоэфф.Add(kk);
        }
        public List<КорректирующийКоэффицентБазовогоНорматива> GetCorrectCoef () => КоррктирующийКоэфф;
        public int id { get; set; }
        public string Desc { get; set; }
        public string FullDesc { get; set; }
        public string Comment { get; set; }        
    }
}
