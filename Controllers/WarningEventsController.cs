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
       public ActionResult<List<WarningEvents>> GetAll()=> ViewModelService.GetAll();
       [HttpGet("{id}")]
       public ActionResult<WarningEvents> GetById(int id)
       {
           var warningevents = ViewModelService.GetById(id);
           if(warningevents==null)
           NotFound();
           return warningevents;
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
       public IActionResult Create(WarningEvents warningevents)
       {
           ViewModelService.AddWarningEvent(warningevents);
           return CreatedAtAction("Create",warningevents);
       }

       
        

    }

}