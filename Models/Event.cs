using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace RestfulApiVisualCode.Models
{
    public class Event
    {
        public int EventId { get; set; }    
        public string Dateofevent{get;set;}
        public int Nameofasb { get; set; }
        public string Nameofdevice { get; set; }
        public string Isserios{get;set;}
        public string Discribeevent{get;set;}
        public string Fixevent { get; set; } = "";
        public string EventCreator { get; set; }
        public List<Image> EventImages { get; set; }
        public string tags { get; set; }
    }
}