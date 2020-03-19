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
                    League = "Delirium",
                    POESessid = "1b85fcbd9cd513e17ce4e36600be97be"
                };
            }
        }
    }
}
