using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class БазовыеНормативыЗатрат
    {        
        БазовыйНормативЗатратСтоимостнойГруппы Норматив;
        КорректирующийКоэффицентБазовогоНорматива Корр_коэффицент;

        public БазовыеНормативыЗатрат ()
        { 
            Норматив = new БазовыйНормативЗатратСтоимостнойГруппы(
                new decimal[] {0.0m, 0.0m, 0.0m, 0.0m});
            Корр_коэффицент = new КорректирующийКоэффицентБазовогоНорматива();  
        }
          
        public БазовыеНормативыЗатрат(string group_name, string year, decimal[] values)
        {
            //добавить аргумент - коэфицент
            Норматив = new БазовыйНормативЗатратСтоимостнойГруппы(values);
            Норматив.СтоимостнаяГруппа.Наименование = group_name;
            Норматив.СтоимостнаяГруппа.КалендарныйГод = year;

            Корр_коэффицент = new КорректирующийКоэффицентБазовогоНорматива(year);                       
        }
        public string Desc { get; set; }
        public string FullDesc { get; set; }
        public string Comment { get; set; }        
    }
}
