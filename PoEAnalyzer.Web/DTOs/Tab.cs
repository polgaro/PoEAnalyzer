using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.DTOs
{
    public class Tab
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public List<StashItem> Items { get; set; }
    }
}
