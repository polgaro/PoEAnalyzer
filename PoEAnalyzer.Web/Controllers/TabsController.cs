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
        private readonly StashService stashService;

        public TabsController(StashService stashService)
        {
            this.stashService = stashService;
        }

        public IActionResult Index()
        {
            DTOs.Stash stash = stashService.GetStashItems();

            IEnumerable<TabModel> tabs = stash.Tabs.Select(x => new TabModel
            {
                Idx = x.Id,
                Name = x.Name,
                Items = x.Items.Select(i => new ItemModel
                {
                    Name = i.Item.Name,
                    Qty = i.Quantity,
                    ChaosValue = i.Price?.Value
                })
                .OrderByDescending(o => o.TotalValue)
                .ThenBy(o => o.Name)
            });

            return View(new TabPageModel { Tabs = tabs }); 
        }
    }
}