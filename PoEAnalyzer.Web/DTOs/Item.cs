using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.DTOs
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? MaxStackSize { get; set; }
        public string Type { get; set; }
    }
}
