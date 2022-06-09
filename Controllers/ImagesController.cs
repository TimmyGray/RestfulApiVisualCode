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

namespace RestfulApiVisualCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : Controller
    {

        EventsContext db;
        public ImagesController(EventsContext _db)
        {
            db = _db;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImages(int id)
        {
            List<Image> images = await db.Images.Where(i=>i.EventId==id).ToListAsync();
            if (images.Count == 0 || images == null)
            { return BadRequest(); }
            //foreach (Image image in images)
            //{
            //    using (FileStream writer = new FileStream("Resources\\"+image.ImageId+".jpeg", FileMode.CreateNew, FileAccess.Write))
            //    {
            //       await writer.WriteAsync(image.ImageByte, 0, image.ImageByte.Length);
            //    }
            //}
            return new ObjectResult(images);
            
        }



        [Route("imageupload")]
        [HttpPost]
        public async Task<ActionResult<Image>> ImageCreate(IFormFileCollection imageFiles)
        {

            if (imageFiles.Count != 0)
            {
                Event evnt = db.Events.OrderByDescending(e => e.EventId).FirstOrDefault();
                int count = 0;
                foreach (IFormFile imageFile in imageFiles)
                {
                    Image img = new Image { Name = imageFile.Headers.ToString(), EventId = evnt.EventId, EventforImage = evnt };
                    using (var binaryreader = new BinaryReader(imageFile.OpenReadStream()))
                    {
                        img.ImageByte = binaryreader.ReadBytes((int)imageFile.Length);
                        
                    }


                    db.Images.Add(img);
                    count++;
                }
                db.Events.Update(evnt);
                await db.SaveChangesAsync();
                return Ok(count);
            }
            return NoContent();


        }


    }
}
