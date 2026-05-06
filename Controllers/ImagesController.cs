using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulApiVisualCode.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulApiVisualCode.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace RestfulApiVisualCode.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : Controller
    {
        private const long MaxImageSizeBytes = 5 * 1024 * 1024;
        private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        EventsContext db;
        public ImagesController(EventsContext _db)
        {
            db = _db;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImages(int id)
        {
            List<Image> images = await db.Images.Where(i => i.EventId == id).ToListAsync();
            if (images.Count == 0)
            {
                return NotFound();
            }
            return new ObjectResult(images);
        }



        [HttpPost]
        public async Task<ActionResult<int>> ImageCreate([FromForm] IFormFileCollection imageFiles, [FromForm] int? eventId)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                return BadRequest("Не переданы изображения");
            }

            Event? evnt = eventId.HasValue
                ? await db.Events.FirstOrDefaultAsync(e => e.EventId == eventId.Value)
                : await db.Events.OrderByDescending(e => e.EventId).FirstOrDefaultAsync();

            if (evnt == null)
            {
                return BadRequest("Событие для изображения не найдено");
            }

            int count = 0;
            foreach (IFormFile imageFile in imageFiles)
            {
                if (imageFile.Length <= 0)
                {
                    return BadRequest("Файл изображения пустой");
                }

                if (imageFile.Length > MaxImageSizeBytes)
                {
                    return BadRequest("Размер изображения превышает допустимый лимит");
                }

                if (!AllowedContentTypes.Contains(imageFile.ContentType))
                {
                    return BadRequest("Неподдерживаемый тип файла");
                }

                Image img = new Image { Name = imageFile.FileName, EventId = evnt.EventId };
                using MemoryStream memoryStream = new MemoryStream();
                await imageFile.CopyToAsync(memoryStream);
                img.ImageByte = memoryStream.ToArray();
                db.Images.Add(img);
                count++;
            }
            db.Events.Update(evnt);
            await db.SaveChangesAsync();
            return Ok(count);

        }


    }
}
