using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    //Базовые нормативы затрат
    public class BasicCostStandards
    {
        BaseRatioCorrectionFactor CorrFactor;
        BaseCostGroupCostRatio CostBaseGroup;
        public BasicCostStandards ()
        {
            CorrFactor = new BaseRatioCorrectionFactor();
            CostBaseGroup = new BaseCostGroupCostRatio();                           
        }
        string Desc { get; set; }
        string FullDesc { get; set; }
        string Comment { get; set; }
        public void SetValues ()
        {
            CorrFactor.
        }
    }
}
