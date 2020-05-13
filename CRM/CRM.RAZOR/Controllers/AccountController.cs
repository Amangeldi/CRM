using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM.BLL.Interfaces;
using CRM.BLL.Services;
using CRM.DAL.Entities;
using CRM.RAZOR.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.RAZOR.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<User> _userManager;
        readonly TempService _tempService;
        SignInManager<User> _signInManager;
        IUserRegistrationService _userRegistrationService;
        public AccountController(UserManager<User> userManager, TempService tempService, SignInManager<User> signInManager, IUserRegistrationService userRegistrationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tempService = tempService;
            _userRegistrationService = userRegistrationService;
        }
        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
             await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {
                    //_tempService.CurrentUser = await _userRegistrationService.GetCurrent(model.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _tempService.CurrentUser = null;
            return RedirectToAction("index", "home");
        }
    }
}