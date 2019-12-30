using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Classes
{
    class СпециальностьСтоимостнойГруппы
    {
        public int id_group { get; set; }
        public int id_spec { get; set; }


        public СпециальностьСтоимостнойГруппы () { }
        public СпециальностьСтоимостнойГруппы (int id_group, int id_spec)
        {
            this.id_group = id_group;
            this.id_spec = id_spec;
        }
    }
}
