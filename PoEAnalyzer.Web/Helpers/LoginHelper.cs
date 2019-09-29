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
                    League = "Blight",
                    POESessid = "1cf6896a50f1e3e8f3bcf9dece51e5f6"
                };
            }
        }
    }
}
