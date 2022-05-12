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
        public IActionResult Create(IFormFileCollection imageFiles)
        {
           
            if (imageFiles.Count != 0)
            {
                foreach (IFormFile imageFile in imageFiles)
                {
                    byte[] imagedata = null;
                    using (var binaryreader = new BinaryReader(imageFile.OpenReadStream()))
                    {
                        imagedata = binaryreader.ReadBytes((int)imageFile.Length);
                    }
                    Image img = new Image { Name=imageFile.Name};
                    img.ImageByte = imagedata;
                    db.Images.Add(img);
                }
                
                db.SaveChangesAsync();
                return Ok("Файлы залиты!");
            }
            return NoContent();
            
           
        }

    
    }
}
