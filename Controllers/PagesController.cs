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
    public class PagesController : ControllerBase
    {
        EventsContext db;

        public PagesController(EventsContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetAll()
        {
            if (!db.Pages.Any())
            {
                return NotFound();
            }
            return await db.Pages.ToListAsync();
        }

        [HttpGet("{subheader}")]
        public async Task<ActionResult<Page>> GetOne(string subheader)
        {
            Page page = await db.Pages.FirstOrDefaultAsync(p => p.Subheader == subheader);
            if (page == null)
            {
                return NotFound();
            }
            return new ObjectResult(page);
        }

        [HttpPost]
        public async Task<ActionResult<Page>> AddPage(Page page)
        {
            if (page == null)
            {
                return BadRequest("Ничего не передалось");
            }
            Page testpage = await db.Pages.FirstOrDefaultAsync(p => p.PageId == page.PageId);
            if (testpage == null)
            {
                db.Pages.Add(page);
                await db.SaveChangesAsync();
                return Ok(page);
            }
            else
            {
                return BadRequest("Такое уже есть");
            }

        }


        [HttpPut]
        public async Task<ActionResult<Page>> UpdatePage(Page page)
        {
            if (page == null)
            {
                return BadRequest("Такой статьи нет");
            }
            if (!db.Pages.Any(p => p.PageId == page.PageId))
            {
                return NotFound();
            }
            db.Update(page);
            await db.SaveChangesAsync();
            return Ok(page);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Page>> DeletePage(int id)
        {
            Page delpage = await db.Pages.FirstOrDefaultAsync(p=>p.PageId==id);
            if(delpage == null)
            {
                return NotFound();
            }
            db.Pages.Remove(delpage);
            await db.SaveChangesAsync();
            return Ok(delpage);
        }

    }
}
