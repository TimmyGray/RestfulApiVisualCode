using RestfulApiVisualCode.Models;
using System.Collections.Generic;
using System.Linq;
using RestfulApiVisualCode.Delegates;
using System;
namespace RestfulApiVisualCode.Services
{
    public static class ViewModelService
    {
        static  List<WarningEvent> Warningevents{get;}
        static WriteLineDelegate write;
        static int currentid = 1;
        static ViewModelService()
        {
            Warningevents = new List<WarningEvent>
            {
                new WarningEvent{Id = 1,IsSerios = false,NameOfDevice = "ssl"}
        
            };
        }
        static public List<WarningEvent> GetAll()=>Warningevents;
        static public WarningEvent GetById(int Id)=>Warningevents.FirstOrDefault(p => p.Id==Id);
        static public void Add(WarningEvent warningevent)
        {
            /* if(currentid!=0)
            currentid++; */
            warningevent.Id=currentid++;
            Warningevents.Add(warningevent);
            
        }
        static public void Delete(int id)
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
        static public void Update(WarningEvent warningevent)
        {
          try
          {
             var id = Warningevents.FindIndex(p => p.Id==warningevent.Id);

             Warningevents[id] = warningevent;
          } 
          catch(Exception e)
          {
            write?.Invoke(e.Message);
          }
        }
        static public void DeleteAll()
        {
            
            Warningevents.Clear();
        }
        
    



    }
}
