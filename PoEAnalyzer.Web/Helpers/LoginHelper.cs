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
                    League = "Harvest",
                    POESessid = "50d1972b8a2f9c734a85df5c5de7cac8"
                };
            }
        }
    }
}
