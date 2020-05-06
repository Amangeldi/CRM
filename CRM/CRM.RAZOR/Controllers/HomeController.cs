using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRM.RAZOR.Models;
using CRM.BLL.Interfaces;

namespace CRM.RAZOR.Controllers
{
    public class HomeController : Controller
    {
        ICompanyService companyServ;
        public HomeController(ICompanyService companyService)
        {
            companyServ = companyService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Титуль";
            ViewData["footer"] = "Футер";
            ViewData["sidebar"] = "";
            ViewData["header"] = "Хедр";
            //ViewData["companies"] = await companyServ.GetCompanies();
            return View();
        }

    }
}
