using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    //Базовый Норматив Затрат Стоимостной Группы
    class БазовыйНормативЗатратСтоимостнойГруппы
    {        
        public СтоимостнаяГруппаКалендарногоГода СтоимостнаяГруппа { get; set; }

        public БазовыйНормативЗатратСтоимостнойГруппы() { }

        public БазовыйНормативЗатратСтоимостнойГруппы (decimal[] values)
        {
            Бакалавриат_Специалитет = values[0];
            Магистратура            = values[1];
            Аспирантура             = values[2];
            SPO                     = values[3];
            //Year                    = СтоимостнаяГруппа.КалендарныйГод;         
            
        }
        decimal Бакалавриат_Специалитет { get; set; }
        decimal Магистратура { get; set; }
        decimal Аспирантура { get; set; }
        decimal SPO { get; set; }
        string Year { get; set; }
        
    }
}
