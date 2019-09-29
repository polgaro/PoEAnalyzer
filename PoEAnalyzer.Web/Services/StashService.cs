using Newtonsoft.Json;
using PoEAnalyzer.Web.DTOs;
using PoEAnalyzer.Web.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEAnalyzer.Web.Services
{
    public static class StashService
    {
        public static Stash GetStashItems()
        {
            Stash ret = new Stash();

            //get tabs
            ret.Tabs = GetTabs();

            //get items in the tab
            foreach(var t in ret.Tabs)
            {
                t.Items = GetItems(t.Id);
            }

            return ret;
        }

        private static List<Tab> GetTabs()
        {
            //https://www.pathofexile.com/character-window/get-stash-items?league={league}&tabs=1&accountName={account}
            IRestResponse response = Execute("get-stash-items", new Dictionary<string, string> { { "tabs", "1" } });

            //read
            dynamic tabInfo = JsonConvert.DeserializeObject(response.Content);
            List<Tab> tabs = new List<Tab>();
            foreach(var x in tabInfo.tabs)
            {
                tabs.Add(new Tab { 
                    Id = x.i,
                    Name = x.n,
                    Type = x.type
                });
            }

            return tabs;

        }

        private static List<Item> GetItems(int tab)
        {
            //https://www.pathofexile.com/character-window/get-stash-items?league=Blight&tabs=1&tabIndex=1&accountName=diego_garber
            IRestResponse response = Execute("get-stash-items", new Dictionary<string, string> { 
                { "tabs", "0" } ,
                { "tabIndex", tab.ToString()}
            });

            //read
            dynamic tabInfo = JsonConvert.DeserializeObject(response.Content);
            List<Item> items = new List<Item>();
            foreach (var x in tabInfo.items)
            {
                items.Add(new Item
                {
                    Id = x.id,
                    Name = x.typeLine ?? x.name,
                    MaxStackSize = x.maxStackSize,
                    Quantity = x.stackSize ?? 1
                });
            }

            return items;
        }


        private static IRestResponse Execute(string resource, Dictionary<string, string> extraParams = null)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://www.pathofexile.com/character-window");
            RestRequest request = new RestRequest();
            request.Resource = resource;

            //add querystring parameters
            request.AddQueryParameter("league", LoginHelper.LoggedContext.League);
            request.AddQueryParameter("accountName", LoginHelper.LoggedContext.Account);

            if (extraParams != null)
            {
                foreach(KeyValuePair<string, string> x in extraParams)
                {
                    request.AddQueryParameter(x.Key, x.Value);
                }
            }

            //add cookie
            request.AddCookie("POESESSID", LoginHelper.LoggedContext.POESessid);

            //execute request
            IRestResponse response = client.Execute(request);
            return response;
        }
        
    }
}
