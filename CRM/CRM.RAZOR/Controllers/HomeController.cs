using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRM.RAZOR.Models;

namespace CRM.RAZOR.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Титуль";
            ViewData["footer"] = "Футер";
            ViewData["sidebar"] = "Сайдбар";
            ViewData["header"] = "Хедр";
            return View();
        }

    }
}
