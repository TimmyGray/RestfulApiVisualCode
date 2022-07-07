 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulApiVisualCode.DataBaseContext;
using RestfulApiVisualCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            await db.Images.Where(i=>i.EventId== evnt.EventId).LoadAsync();
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
            { return BadRequest(ModelState); }

            evnt.tags = $"{evnt.Dateofevent},{evnt.Discribeevent},{evnt.EventCreator},{evnt.Fixevent},{evnt.Isserios},{evnt.Nameofasb},{evnt.Nameofdevice}";
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

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> Delete(int id)
        {

            Event evnt =await db.Events.FirstOrDefaultAsync(x => x.EventId == id);
            await db.Images.Where(i => i.EventId == evnt.EventId).LoadAsync();

            if (evnt == null)
            {

                return NotFound();
            }

            if (evnt.EventImages!=null)
            {  

                db.Images.RemoveRange(evnt.EventImages);

            }
            
            db.Events.Remove(evnt);
            await db.SaveChangesAsync();
            return Ok(evnt.EventId);
        }
    }
}