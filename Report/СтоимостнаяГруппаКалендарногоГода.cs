using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    //Стоимостная Группа Календарного Года
    class СтоимостнаяГруппаКалендарногоГода
    {
        public СтоимостнаяГруппаКалендарногоГода(string name, string year)
        {
            Наименование = name;
            КалендарныйГод = year;
        }
        public string Наименование { get; set; }
        string FullDesc { get; set; }
        string Comment { get; set; }
        public string КалендарныйГод { get; set; }
    }
}
