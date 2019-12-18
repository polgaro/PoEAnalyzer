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
            Parallel.ForEach(ret.Tabs, (t) =>
            {
                t.Items = GetItems(t.Id);
            });

            return ret;
        }


        private static List<Tab> GetTabs()
        {
            //https://www.pathofexile.com/character-window/get-stash-items?league={league}&tabs=1&accountName={account}
            IRestResponse response = Execute("get-stash-items", new Dictionary<string, string> { { "tabs", "1" } });

            //read
            dynamic tabInfo = JsonConvert.DeserializeObject(response.Content);
            List<Tab> tabs = new List<Tab>();
            foreach(dynamic x in tabInfo.tabs)
            {
                tabs.Add(new Tab { 
                    Id = x.i,
                    Name = x.n,
                    Type = x.type
                });
            }

            return tabs;

        }

        private static List<StashItem> GetItems(int tab)
        {
            //to get a list of all the leagues:
            //https://www.pathofexile.com/character-window/get-characters?accountName=diego_garber
            //maybe i should do this to get the latest league? :P there's one that says "lastActive=true"

            //https://www.pathofexile.com/character-window/get-stash-items?league=Metamorph&tabs=1&tabIndex=1&accountName=diego_garber
            IRestResponse response = Execute("get-stash-items", new Dictionary<string, string> { 
                { "tabs", "0" } ,
                { "tabIndex", tab.ToString()}
            });

            //read
            dynamic tabInfo = JsonConvert.DeserializeObject(response.Content);
            List<StashItem> items = new List<StashItem>();
            foreach (dynamic x in tabInfo.items)
            {
                Item item = new Item
                {
                    Id = x.id,
                    Name = x.typeLine ?? x.name,
                    MaxStackSize = x.maxStackSize,
                    Type = GetType(x)
                };

                items.Add(
                    new StashItem
                    {
                        Quantity = x.stackSize ?? 1,
                        Item = item,
                        Price = PriceService.GetPrice(item)
                    });
            }

            return items;
        }

        private static string GetType(dynamic x)
        {
            string icon = x.icon;

            if (!string.IsNullOrEmpty(icon) && icon.Contains("Divination"))
                return "card";
            return "";
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
