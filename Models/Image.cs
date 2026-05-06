using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestfulApiVisualCode.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] ImageByte { get; set; } = Array.Empty<byte>();
        public int? EventId { get; set; }
        [JsonIgnore]
        public Event? EventforImage { get; set; }
    }
}
