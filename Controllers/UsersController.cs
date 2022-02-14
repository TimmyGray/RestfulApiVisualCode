using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RestfulApiVisualCode.DataBaseContext;
using RestfulApiVisualCode.Models;

namespace RestfulApiVisualCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        EventsContext db;
        public UsersController(EventsContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new User { Login = "Timmy", Password = "1234" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }
    }
}
