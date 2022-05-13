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
                return BadRequest(ModelState);
            db.Events.Add(evnt);
            await db.SaveChangesAsync();
            return Ok(evnt);
            
        }

        
        [Route("/imageupload")]
        [HttpPost]
        public async Task<IActionResult> ImageCreate(IFormFileCollection files)
        {
            if (files is null)
            {
                return NoContent();
            }
            foreach (IFormFile file in files)
            {
                await Task.Run(() =>
                {
                    byte[] imagedata = null;
                    using (var binaryreader = new BinaryReader(file.OpenReadStream()))
                    {
                        imagedata = binaryreader.ReadBytes((int)file.Length);
                    }
                    Image image = new Image {Name=file.Name, ImageByte = imagedata };
                    db.Images.Add(image);
                });

            }
            await db.SaveChangesAsync();
            return Ok();
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