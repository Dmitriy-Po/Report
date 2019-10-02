using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    class КорректирующиеКоэффиценты
    {
        ЗначениеКоэффицента Значение_к;
        public КорректирующиеКоэффиценты(decimal koef)
        {
            Значение_к = new ЗначениеКоэффицента(koef);
        }
        public decimal GetCoeff() => Значение_к.Коэффицент;
        string Наименование { get; set; }
        string ПолноеНаименование { get; set; }
        string Уточнение { get; set; }
        string Комментарий { get; set; }
        bool СтудентИнвалид { get; set; }
    }
}
