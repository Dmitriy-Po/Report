using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    //Стоимостная Группа Календарного Года
    public class СтоимостнаяГруппаКалендарногоГода
    {
        List<БазовыйНормативЗатратСтоимостнойГруппы> норматив = new List<БазовыйНормативЗатратСтоимостнойГруппы>();


        public СтоимостнаяГруппаКалендарногоГода(string name, string year)
        {
            Наименование = name;
            КалендарныйГод = year;
        }
        
        public СтоимостнаяГруппаКалендарногоГода(int id, string desc)
        {
            this.id_group = id;
            this.Наименование = desc;
        }
        public void SetNormal (БазовыйНормативЗатратСтоимостнойГруппы new_normal)
        {
            норматив.Add(new_normal);
        }
        public List<БазовыйНормативЗатратСтоимостнойГруппы> GetNormal () => норматив;

        public int id_group { get; set; }
        public string Наименование { get; set; }
        string FullDesc { get; set; }
        string Comment { get; set; }
        public string КалендарныйГод { get; set; }
    }
}
