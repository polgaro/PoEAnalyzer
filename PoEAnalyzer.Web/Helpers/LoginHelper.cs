using PoEAnalyzer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.Helpers
{
    public static class LoginHelper
    {
        public static ContextModel LoggedContext
        {
            get {
                return new ContextModel
                {
                    Account = "diego_garber",
                    Character = "",
                    League = "Metamorph",
                    POESessid = "c8ee94102edb79bee6ce66dc8465dfb9"
                };
            }
        }
    }
}
