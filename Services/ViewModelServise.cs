using RestfulApiVisualCode.Models;
using System.Collections.Generic;
using System.Linq;
using RestfulApiVisualCode.Delegates;
using System;
namespace RestfulApiVisualCode.Services
{
    static class ViewModelService
    {
        static  List<WarningEvents> Warningevents{get;}
        static WriteLineDelegate write;
        static int currentid = 0;
        static ViewModelService()
        {
            Warningevents = new List<WarningEvents>();
        }
        static internal List<WarningEvents> GetAll()=>Warningevents;
        static internal WarningEvents GetById(int Id)=>Warningevents.FirstOrDefault(p => p.Id==Id);
        static internal void AddWarningEvent(WarningEvents warningevent)
        {
            if(currentid!=0)
            currentid++;
            warningevent.Id=currentid;
            Warningevents.Add(warningevent);
            
        }
        static void Delete(int id)
        {
            try
           {
               Warningevents.Remove(GetById(id));
            }
            catch (Exception e)
            {
                write?.Invoke(e.Message);
            }
           
        }
        static void Update(WarningEvents warningevents)
        {
          try
          {
             var id = Warningevents.FindIndex(p => p.Id==warningevents.Id);

             Warningevents[id] = warningevents;
          } 
          catch(Exception e)
          {
            write?.Invoke(e.Message);
          }
        }
        static void DeleteAll()
        {
            
            Warningevents.Clear();
        }
        
    



    }
}
