using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRM.RAZOR.Models;
using CRM.BLL.Interfaces;
using CRM.DAL.Entities;
using CRM.BLL.Services;

namespace CRM.RAZOR.Controllers
{
    public class HomeController : Controller
    {
        TempService tempServ;
        IUserRegistrationService userRegistrationServ;
        public HomeController(TempService tempService, IUserRegistrationService userRegistrationService)
        {
            tempServ = tempService;
            userRegistrationServ = userRegistrationService;
        }
        
        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                tempServ.CurrentUser = await userRegistrationServ.GetCurrent(User.Identity.Name);
            }
            ViewData["Title"] = "Титуль";
            ViewData["footer"] = "Футер";
            ViewData["sidebar"] = "";
            ViewData["header"] = "Хедр";
            //ViewData["companies"] = await companyServ.GetCompanies();
            return View();
        }
    }
}
