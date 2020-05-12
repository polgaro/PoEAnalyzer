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
                    POESessid = "3c2d6d6fb9be3a6151f8bb10c626b106"
                };
            }
        }
    }
}
