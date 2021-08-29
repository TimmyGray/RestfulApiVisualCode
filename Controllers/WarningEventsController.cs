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
       public async Task<ActionResult<List<WarningEvent>>> GetAll()=> await db.warningevents.ToListAsync();
       
       [HttpGet("{id}")]
        public async Task<ActionResult<WarningEvent>> GetById(int id)
       {
           var warningevent =await db.warningevents.FirstOrDefaultAsync(p=>p.Id==id);
           if(warningevent==null)
           return NotFound();
           return warningevent;
       }

       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteById(int id)
       {
           var warningevent = await db.warningevents.FirstOrDefaultAsync(p=>p.Id==id);
           if(warningevent==null)
           return NotFound();
           db.warningevents.Remove(warningevent);
           await db.SaveChangesAsync();
           return Ok(warningevent);
       }
       [HttpDelete]
       public async Task<IActionResult> DeleteAll()
       {
           var warningevents =await db.warningevents.ToListAsync();
           if(warningevents==null)
           return NoContent();
           for(int i=0;i<warningevents.Capacity-1;i++)
           {
               db.warningevents.Remove(warningevents[i]);
               await db.SaveChangesAsync();
           }
           return NoContent();
       } 
       [HttpPost]
       public async Task<IActionResult> Create(WarningEvent warningevent)
       {
           if(warningevent==null)
           return BadRequest();
            db.warningevents.Add(warningevent);
           await db.SaveChangesAsync();
           return CreatedAtAction(nameof(Create),new {id=warningevent.Id},warningevent);
       }

       [HttpPut("{id}")]
       public async Task<IActionResult> Update(int id, WarningEvent warningivent)
       {
           if(id!=warningivent.Id)
            return BadRequest();
           var warningevent = await db.warningevents.FirstOrDefaultAsync(p=>p.Id==id);
           if(warningevent==null)
           return NotFound();
           db.warningevents.Update(warningivent);
           await db.SaveChangesAsync();
           return Ok(warningevent);
           
       }


       
        

    }

}