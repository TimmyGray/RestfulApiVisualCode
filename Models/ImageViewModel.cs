using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestfulApiVisualCode.Models
{
    public class ImageViewModel
    {
        public string Name { get; set; }
        public IFormFile imageFile { get; set; }
    }
}
