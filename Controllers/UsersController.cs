using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RestfulApiVisualCode.DataBaseContext;
using RestfulApiVisualCode.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using RestfulApiVisualCode.ViewModels;

namespace RestfulApiVisualCode.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class UsersController:Controller
    {
        EventsContext db;
        public UsersController(EventsContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return Content("Авторизация");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {

                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == loginModel.Login && u.Password == loginModel.Password);
                if (user!=null)
                {
                    await Authenticate(loginModel.Login);
                    return Redirect("~/ClientView.html");

                }
                ModelState.AddModelError("", "Неправильные логин и(или) пароль");

            }
            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AutorizationModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    db.Users.Add(new User { Login = model.Login, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Login); 

                    return Redirect("~/ClientView.html");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }
    }
}
