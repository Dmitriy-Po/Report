using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    //Базовый Норматив Затрат Стоимостной Группы
    class BaseCostGroupCostRatio
    {

        public BaseCostGroupCostRatio ()
        {
            Bacalavriat = default(decimal);
            Magistr     = default(decimal);
            Aspirant    = default(decimal);
            SPO         = default(decimal);
            Year        = default(string);
        }
        decimal Bacalavriat { get; set; }
        decimal Magistr { get; set; }
        decimal Aspirant { get; set; }
        decimal SPO { get; set; }
        string Year { get; set; }
        //добавить ассоциацию с классом СтоимостнаяГруппаКалендарногоГода
    }
}
