using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.Models
{
    public class TabModel
    {
        public int Idx { get; set; }
        public string Name { get; set; }
        public double ChaosValue 
        {
            get
            {
                return Items.Sum(x => x.TotalValue);
            }
        }
        public IEnumerable<ItemModel> Items {get; set;}
    }

    public class ItemModel
    {
        public string Name { get; set; }
        public int Qty { get; set; }
        public double? ChaosValue { get; set; }
        public double TotalValue
        {
            get
            {
                return (Qty * ChaosValue) ?? 0;
            }
        }
    }
}
