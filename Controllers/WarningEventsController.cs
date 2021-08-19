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
       public ActionResult<List<WarningEvents>> GetAll() => WarningEvents.

    }

}