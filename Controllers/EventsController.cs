using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RestfulApiVisualCode.Models;
using System.Threading.Tasks;
using RestfulApiVisualCode.DataBaseContext;
using System;
using Microsoft.AspNetCore.Authorization;

namespace RestfulApiVisualCode.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        readonly EventsContext db;
        public EventsController(EventsContext context)
        {
            db = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            return await db.Events.ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            Event evnt = await db.Events.FirstOrDefaultAsync(x => x.EventId == id);
            if (evnt == null)
                return NotFound();
            return new ObjectResult(evnt);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Post(Event evnt)
        {
            if (evnt == null)
            {
                return BadRequest();
            }
            if (Convert.ToDateTime(evnt.Dateofevent)>DateTime.Now)
            {
                ModelState.AddModelError("Dateofevent", "Дата не может быть позднее сегодняшнего числа");
            }
            if (!ModelState.IsValid)
                return BadRequest();
 
            db.Events.Add(evnt);
            await db.SaveChangesAsync();
            return Ok(evnt);
            
        }

        [HttpPut]
        public async Task<ActionResult<Event>> Put(Event evnt)
        {
            if (evnt == null)
            {
                return BadRequest();
            }
            if (!db.Events.Any(x => x.EventId == evnt.EventId))
            {
                return NotFound();
            }
 
            db.Update(evnt);
            await db.SaveChangesAsync();
            return Ok(evnt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> Delete(int id)
        {
            Event evnt = db.Events.FirstOrDefault(x => x.EventId == id);
            if (evnt == null)
            {
                return NotFound();
            }
            db.Events.Remove(evnt);
            await db.SaveChangesAsync();
            return Ok(evnt);
        }
    }
}