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
using RestfulApiVisualCode.Security;

namespace RestfulApiVisualCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController:Controller
    {
        EventsContext db;
        public UsersController(EventsContext context)
        {
            db = context;
        }


        [Route("index")]
        [HttpGet]
      //  [Authorize(Roles ="Engeneer,Admin")]
        public IActionResult Index()
        {
            return Content(User.Identity?.Name ?? string.Empty);
        }

        //[Route("authorize")]
        //[HttpGet]
        //public IActionResult Authoirize()
        //{

        //    return Redirect("~/authorize.html");
        //}

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest log)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            User? user = await db.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == log.Login);
            if (user != null && (PasswordHasher.Verify(log.Password, user.Password) || user.Password == log.Password))
            {
                if (user.Password == log.Password)
                {
                    user.Password = PasswordHasher.Hash(log.Password);
                    await db.SaveChangesAsync();
                }

                await Authenticate(user);

                return Ok(new
                {
                    user.UserId,
                    user.Login,
                    Role = user.Role?.RoleName
                });

            }

            return BadRequest("Неправильный логин или пароль");
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest reg)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            User? user = await db.Users.FirstOrDefaultAsync(u => u.Login == reg.Login);
            if (user == null)
            {
                user = new User { Login = reg.Login, Password = PasswordHasher.Hash(reg.Password) };
                Role? role = await db.Roles.FirstOrDefaultAsync(r => r.RoleName == "Engeneer");
                if (role != null)
                {
                    user.Role = role;
                }
                db.Users.Add(user);
                await db.SaveChangesAsync();

                await Authenticate(user);

                return Ok(new
                {
                    user.UserId,
                    user.Login,
                    Role = user.Role?.RoleName
                });
            }
            return BadRequest("Такой пользователь уже существует");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.RoleName ?? string.Empty)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
          await  HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
          return Ok("logout");

        }
    }
}
