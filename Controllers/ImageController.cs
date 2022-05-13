using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulApiVisualCode.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulApiVisualCode.DataBaseContext;

namespace RestfulApiVisualCode.Controllers
{
    public class ImageController : Controller
    {

        EventsContext db;
        public ImageController(EventsContext _db)
        {
            db = _db;
        }
        
        [Route("image")]
        [HttpPost]
        public async Task<ActionResult<Image>> Create(IFormFileCollection imageFiles)
        {
           
            if (imageFiles.Count != 0)
            {
                List<Image> images = new List<Image>();
                Event evnt = db.Events.OrderByDescending(e => e.EventId).FirstOrDefault();
                             
                evnt.EventImages = images;
                foreach (IFormFile imageFile in imageFiles)
                {
                    byte[] imagedata = null;
                    using (var binaryreader = new BinaryReader(imageFile.OpenReadStream()))
                    {
                        imagedata = binaryreader.ReadBytes((int)imageFile.Length);
                    }
                    Image img = new Image { Name =imageFile.Headers.ToString() , EventId = Convert.ToInt32(imageFile.FileName) };
                    img.ImageByte = imagedata;
                    images.Add(img);
                    
                    db.Images.Add(img);
                }
                db.Events.Update(evnt);
                await db.SaveChangesAsync();
                return Ok(images.Count);
            }
            return NoContent();
            
           
        }

    
    }
}
