using System;

namespace RestfulApiVisualCode.Models
{
    public class WarningEvents
    {
        internal int Id{get;set;}
        DateTime DateOfEvent{get;set;}
        string NameOfDevice{get;set;}
        string EventDescribe{get;set;}
        string NameOfASB{get;set;}
        string DiscribeOfFix{get;set;}
        bool IsSerios{get;set;}

    }
}