using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KittenJournal.Models;
using Microsoft.AspNetCore.Authorization;

namespace KittenJournal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Fosters");
            } else if (User.IsInRole("Foster"))
            {
                return RedirectToAction("Index", "Kittens");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
