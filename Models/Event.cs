using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace RestfulApiVisualCode.Models
{
    public class Event
    {
        public int EventId { get; set; }    
        [Required]
        public string Dateofevent { get; set; } = string.Empty;
        public int Nameofasb { get; set; }
        [Required]
        public string Nameofdevice { get; set; } = string.Empty;
        [Required]
        public string Isserios { get; set; } = string.Empty;
        [Required]
        public string Discribeevent { get; set; } = string.Empty;
        public string Fixevent { get; set; } = "";
        [Required]
        public string EventCreator { get; set; } = string.Empty;
        public List<Image> EventImages { get; set; } = new List<Image>();
        [JsonProperty("tags")]
        public string Tags { get; set; } = string.Empty;
    }
}
