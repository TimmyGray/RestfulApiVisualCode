using System;

namespace RestfulApiVisualCode.Models
{
    public class WarningEvent
    {
        public int Id{get;set;}
       public DateTime DateOfEvent{get;set;}
       public string NameOfDevice{get;set;}
       public string EventDescribe{get;set;}
       public string NameOfASB{get;set;}
       public string DiscribeOfFix{get;set;}
       public bool IsSerios{get;set;}

    }
}