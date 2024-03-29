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
using RestfulApiVisualCode.EmailClass;
using System.Text;

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
            if (evnt.Isserios == "Серьезное происшествие")
            {
                try
                {
                    await SendEmailMessage(evnt);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Email not send. exception - {e.Message}");
                    
                }
            }


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

        public async Task SendEmailMessage(Event evnt)
        {
            StringBuilder emailtext = new StringBuilder();
            emailtext.AppendLine($"Сегодня в {DateTime.Now.ToShortTimeString()}, было зафиксировано серьзное происшествие:");
            emailtext.AppendLine($"Номер асб - {evnt.Nameofasb}");
            emailtext.AppendLine($"Что сломалось - {evnt.Nameofdevice}");
            emailtext.AppendLine($"Характер поломки, причины произошедшего:");
            emailtext.AppendLine(evnt.Discribeevent);
            if (evnt.Fixevent!=null)
            {
                emailtext.AppendLine("Поломку устранили следущим образом:");
                emailtext.AppendLine(evnt.Fixevent);

            }
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync("timmygray@yandex.ru", "Серьезное происшествие", emailtext.ToString());
        }
    }

}