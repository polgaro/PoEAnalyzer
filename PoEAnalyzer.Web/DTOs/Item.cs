using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.DTOs
{
    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Id { get; set; }
        public int? MaxStackSize { get; set; }
    }
}
