using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RestfulApiVisualCode.Models
{
    public class Page
    {
        public int PageId { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Subheader { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string PageCreator { get; set; } = string.Empty;
        [JsonProperty("tags")]
        public string Tags { get; set; } = string.Empty;


    }
}
