using Microsoft.AspNetCore.Http;
using PoEAnalyzer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.Helpers
{
    public class LoginService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ContextModel LoggedContext
        {
            get 
            {
                return new ContextModel
                {
                    Account = GetQueryString("Account") ?? "diego_garber",
                    Character = "",
                    League = GetQueryString("League") ?? "Harvest",
                    POESessid = GetQueryString("POESessid") ?? "50d1972b8a2f9c734a85df5c5de7cac8"
                };
            }
        }

        private string GetQueryString(string s)
        {
            return httpContextAccessor.HttpContext.Request.Query[s].FirstOrDefault();
        }
    }
}
