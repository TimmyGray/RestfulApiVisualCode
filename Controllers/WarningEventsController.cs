using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestfulApiVisualCode.Services;
using RestfulApiVisualCode.Models;
using RestfulApiVisualCode.Delegates;
using Microsoft.EntityFrameworkCore;
using RestfulApiVisualCode.DataBaseContext;
using System;
using System.Threading.Tasks;

namespace RestfulApiVisualCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarningEventsController:ControllerBase
    {
        WarningEventContext db;
        
        public WarningEventsController(WarningEventContext db)
        {
            this.db = db;
            if(!db.warningevents.Any())
            {
                db.warningevents.Add(new WarningEvent{NameOfASB = "7",NameOfDevice = "SSL",DateOfEvent = DateTime.Now});
                db.SaveChanges();
            }
            
        }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<WarningEvent>>> GetAll()=> await db.warningevents.ToListAsync();
       
       [HttpGet("{id}")]
        public async Task<ActionResult<WarningEvent>> GetById(int id)
       {
           var warningevent =await db.warningevents.FirstOrDefaultAsync(p=>p.Id==id);
           if(warningevent==null)
            return NotFound();
           return new ObjectResult(warningevent);
       }

       [HttpDelete("{id}")]
       public async Task<ActionResult<WarningEvent>> DeleteById(int id)
       {
           var warningevent = await db.warningevents.FirstOrDefaultAsync(p=>p.Id==id);
           if(warningevent==null)
                return NotFound();
           db.warningevents.Remove(warningevent);
           await db.SaveChangesAsync();
           return Ok(warningevent);
       }
       [HttpDelete]
       public async Task<ActionResult> DeleteAll()
       {
           var warningevents =await db.warningevents.ToListAsync();
           if(warningevents==null)
           return NoContent();
           for(int i=1;i<warningevents.Capacity+1;i++)
           {
               db.warningevents.Remove(warningevents[i]);
               await db.SaveChangesAsync();
           }
           return NoContent();
       } 
       [HttpPost]
       public async Task<ActionResult<WarningEvent>> Create(WarningEvent warningevent)
       {
           if(warningevent==null)
                return BadRequest();
            db.warningevents.Add(warningevent);
           await db.SaveChangesAsync();
           return Ok(warningevent);
       }

       [HttpPut]
       public async Task<ActionResult<WarningEvent>> Update(WarningEvent warningevent)
       {
           
           if(warningevent==null)
                return BadRequest();

           if(!db.warningevents.Any(p=>p.Id==warningevent.Id))
                return NotFound();

           db.Update(warningevent);
           await db.SaveChangesAsync();
           return Ok(warningevent);
           
       }


       
        

    }

}