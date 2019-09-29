using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PoEAnalyzer.Web.Models;
using PoEAnalyzer.Web.Services;

namespace PoEAnalyzer.Web.Controllers
{
    public class TabsController : Controller
    {
        public IActionResult Index()
        {
            var stash = StashService.GetStashItems();
            return View(
                    stash.Tabs.Select(x => new TabModel
                    {
                        Idx = x.Id,
                        Name = x.Name,
                        Items = x.Items.Select(i => new ItemModel{ 
                            Name = i.Name,
                            Qty = i.Quantity
                        })
                    })
                ); ;
        }
    }
}