using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.Models
{
    public class TabPageModel
    {
        public double ChaosValue
        {
            get
            {
                return Tabs.Sum(x => x.ChaosValue);
            }
        }
        public IEnumerable<TabModel> Tabs { get; set; }
    }
}
