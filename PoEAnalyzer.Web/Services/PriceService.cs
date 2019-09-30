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
    public static class PriceService
    {
        static Dictionary<string, Price> _prices = new Dictionary<string, Price>();

        public static Price GetPrice(Item item)
        {
            Dictionary<string, Price> prices = GetAllPrices();
            if (prices.ContainsKey(item.Name))
                return prices[item.Name];
            return null;
        }

        private static Dictionary<string, Price> GetAllPrices()
        {
            if (_prices.Count == 0)
            {
                lock (_prices)
                {
                    if (_prices.Count == 0)
                    {
                        string[] categories = new string[]
                        {
                            "card",
                            "currency",
                            "scarab",
                            "flask",
                            //"gem",
                            "map",
                            "prophecy"
                        };

                        //get ALL the categories in parallel
                        Dictionary<string, Dictionary<string, Price>> newPrices = categories.AsParallel().ToDictionary(
                                    x => x,
                                    x => GetPricesByCategory(x)
                                );
                        
                        //now add all the prices to the _prices dictionary
                        foreach(Dictionary<string, Price> pricesByCategory in newPrices.Values)
                        {
                            foreach(KeyValuePair<string, Price> x in pricesByCategory)
                            {
                                //some things are duplicated!
                                if(!_prices.ContainsKey(x.Key))
                                    _prices.Add(x.Key, x.Value);
                            }
                        }

                        //fix chaos value
                        _prices["Chaos Orb"] = new Price { Unit = "chaos", Value = 1 };
                        }//if
                }//lock
            }//if
            return _prices;
        }

        private static Dictionary<string, Price> GetPricesByCategory(string category)
        {
            IRestResponse response = Execute("get", new Dictionary<string, string> {
                            {"league", LoginHelper.LoggedContext.League},
                            { "category",category}
                        });

            Dictionary<string, Price> ret = new Dictionary<string, Price>();
            dynamic prices = JsonConvert.DeserializeObject(response.Content);

            foreach (dynamic price in prices)
            {
                string key = price.name.ToString();
                
                //there are some duplicated prices :S
                if (!ret.ContainsKey(key))
                {
                    ret.Add(key, new Price
                    {
                        Unit = "chaos",
                        Value = price.min
                    });
                }
            }
            return ret;
        }

        private static IRestResponse Execute(string resource, Dictionary<string, string> extraParams = null)
        {
            //https://api.poe.watch/get?league=Blight&category=card
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.poe.watch/");
            RestRequest request = new RestRequest();
            request.Resource = resource;

            //add querystring parameters
            if (extraParams != null)
            {
                foreach (KeyValuePair<string, string> x in extraParams)
                {
                    request.AddQueryParameter(x.Key, x.Value);
                }
            }

            //execute request
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
