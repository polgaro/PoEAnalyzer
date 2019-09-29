using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.Models
{
    public class ContextModel
    {
        public string POESessid { get; set; }
        public string Account { get; set; }
        public string League { get; set; }
        public string Character { get; set; }
    }
}
