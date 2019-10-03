using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class БазовыеНормативыЗатрат
    {        
        БазовыйНормативЗатратСтоимостнойГруппы Группа;
        КорректирующийКоэффицентБазовогоНорматива Корр_коэфф_год;

        public БазовыеНормативыЗатрат ()
        { 
            Группа = new БазовыйНормативЗатратСтоимостнойГруппы(
                new decimal[] {0.0m, 0.0m, 0.0m, 0.0m});
            Корр_коэфф_год = new КорректирующийКоэффицентБазовогоНорматива();  
        }
          
        public БазовыеНормативыЗатрат(string group_name, string year, decimal[] values)
        {
            //добавить аргумент - коэфицент
            Группа = new БазовыйНормативЗатратСтоимостнойГруппы(values);
            Группа.СтоимостнаяГруппа.Наименование = group_name;
            Группа.СтоимостнаяГруппа.КалендарныйГод = year;

            Корр_коэфф_год = new КорректирующийКоэффицентБазовогоНорматива(year);                       
        }
        public string Desc { get; set; }
        public string FullDesc { get; set; }
        public string Comment { get; set; }        
    }
}
