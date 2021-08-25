using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestfulApiVisualCode.Services;
using RestfulApiVisualCode.Models;
using RestfulApiVisualCode.Delegates;

namespace RestfulApiVisualCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarningEventsController:ControllerBase
    {
        public WarningEventsController()
        {

        }

       [HttpGet]
       public ActionResult<List<WarningEvent>> GetAll()=> ViewModelService.GetAll();
       
       [HttpGet("{id}")]
       public ActionResult<WarningEvent> GetById(int id)
       {
           var warningevent = ViewModelService.GetById(id);
           if(warningevent==null)
           NotFound();
           return warningevent;
       }

       [HttpDelete("{id}")]
       public IActionResult DeleteById(int id)
       {
           var warningevent = ViewModelService.GetById(id);
           if(warningevent==null)
           return NotFound();
           ViewModelService.Delete(id);
           return NoContent();
       }
       [HttpDelete]
       public IActionResult DeleteAll()
       {
           ViewModelService.DeleteAll();
           return NoContent();
       }
       [HttpPost]
       public IActionResult Create(WarningEvent warningevent)
       {
           ViewModelService.Add(warningevent);
           return CreatedAtAction(nameof(Create),new {id=warningevent.Id},warningevent);
       }

       [HttpPut("{id}")]
       public IActionResult Update(int id, WarningEvent warningivent)
       {
           if(id!=warningivent.Id)
           BadRequest();
           var warningevent = ViewModelService.GetById(id);
           if(warningevent==null)
           NotFound();
           ViewModelService.Update(warningivent);
           return NoContent();
           
       }


       
        

    }

}