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
        private static readonly byte[] JpegHeader = [0xFF, 0xD8, 0xFF];
        private static readonly byte[] PngHeader = [0x89, 0x50, 0x4E, 0x47];
        private static readonly byte[] Gif87aHeader = [0x47, 0x49, 0x46, 0x38, 0x37, 0x61];
        private static readonly byte[] Gif89aHeader = [0x47, 0x49, 0x46, 0x38, 0x39, 0x61];
        private static readonly byte[] WebPHeader = [0x57, 0x45, 0x42, 0x50];

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
                using var inputStream = imageFile.OpenReadStream();
                using MemoryStream memoryStream = new MemoryStream();
                await inputStream.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                if (!HasValidImageSignature(imageBytes))
                {
                    return BadRequest("Файл не является корректным изображением");
                }

                img.ImageByte = imageBytes;
                db.Images.Add(img);
                count++;
            }
            db.Events.Update(evnt);
            await db.SaveChangesAsync();
            return Ok(count);

        }

        private static bool HasValidImageSignature(byte[] fileBytes)
        {
            byte[] header = new byte[12];
            int bytesRead = Math.Min(fileBytes.Length, header.Length);
            Array.Copy(fileBytes, header, bytesRead);
            if (bytesRead < 3)
            {
                return false;
            }

            if (StartsWith(header, bytesRead, JpegHeader) || StartsWith(header, bytesRead, PngHeader))
            {
                return true;
            }

            if (StartsWith(header, bytesRead, Gif87aHeader) || StartsWith(header, bytesRead, Gif89aHeader))
            {
                return true;
            }

            return bytesRead >= 12
                   && header[0] == 0x52
                   && header[1] == 0x49
                   && header[2] == 0x46
                   && header[3] == 0x46
                   && header[8] == WebPHeader[0]
                   && header[9] == WebPHeader[1]
                   && header[10] == WebPHeader[2]
                   && header[11] == WebPHeader[3];
        }

        private static bool StartsWith(byte[] source, int sourceLength, byte[] prefix)
        {
            if (sourceLength < prefix.Length)
            {
                return false;
            }

            for (int i = 0; i < prefix.Length; i++)
            {
                if (source[i] != prefix[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
