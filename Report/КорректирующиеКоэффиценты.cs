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
        public КорректирующиеКоэффиценты(decimal значениекоэффицента, string формаобучения, string год)
        {
            Значение_к = new ЗначениеКоэффицента(значениекоэффицента, формаобучения, год);
        }
        //return
        public string Наименование { get; set; }
        public string ПолноеНаименование { get; set; }
        public string Уточнение { get; set; }
        public string Комментарий { get; set; }
        public bool СтудентИнвалид { get; set; }
    }
}
