using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.DTOs
{
    public class StashItem
    {
        public int Quantity { get; set; }
        public Item Item { get; set; }
        public Price Price { get; set; }
    }
}
