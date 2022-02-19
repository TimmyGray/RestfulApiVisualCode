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
    public class PagesController:ControllerBase
    {
        EventsContext db;

        public PagesController(EventsContext context)
        {
            db = context;
            if (!db.Pages.Any())
            {
                db.Pages.Add(new Page { Header = "Компьютеры, Графические станции, Dalet", SubHeader = "Компьютеры", Info="Это тестовая статья, помогающая понять работает или нет" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetAll()
        {
            if(!db.Pages.Any())
            {
                return NotFound();
            }
            return await db.Pages.ToListAsync();
        }

        [HttpGet("{subheader}")]
        public async Task<ActionResult<Page>> GetOne(string subheader)
        {
            Page page = await db.Pages.FirstOrDefaultAsync(p => p.SubHeader == subheader);
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
                return BadRequest();
            }
            db.Pages.Add(page);
            await db.SaveChangesAsync();
            return Ok(page);
        }

        [HttpPut]
        public async Task<ActionResult<Page>> UpdatePage(Page page)
        {
            if (page == null)
            {
                return BadRequest();
            }
            Page updpage = await db.Pages.FirstOrDefaultAsync(p => p.SubHeader == page.SubHeader);
            if (updpage == null)
            {
                return NotFound();
            }
            db.Pages.Update(page);
            await db.SaveChangesAsync();
            return Ok(page);
        }

        [HttpDelete]
        public async Task<ActionResult<Page>> DeletePage(Page page)
        {
            if(page==null)
            {
                return BadRequest();
            }
            Page delpage = await db.Pages.FirstOrDefaultAsync(p => p.SubHeader == page.SubHeader);
            if(delpage == null)
            {
                return NotFound();
            }
            db.Pages.Remove(page);
            await db.SaveChangesAsync();
            return Ok(page);
        }

    }
}
