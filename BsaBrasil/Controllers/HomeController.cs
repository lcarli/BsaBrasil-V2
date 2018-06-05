using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BsaBrasil.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace BsaBrasil.Controllers
{
    public class HomeController : Controller
    {
        public IStringLocalizer<HomeController> Localizer { get; }

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            Localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
