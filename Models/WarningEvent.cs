using System;

namespace RestfulApiVisualCode.Models
{
    public class WarningEvent
    {
        public int Id{get;set;}
       public DateTime DateOfEvent{get;set;}
       public string NameOfDevice{get;set;}
       public string EventDiscribe{get;set;}
       public string NameOfASB{get;set;}
       public string DiscribeOfFix{get;set;}
       public string IsSerios{get;set;}

    }
}