
using RestfulApiVisualCode.Models;
using Microsoft.EntityFrameworkCore;

 
namespace RestfulApiVisualCode.DataBaseContext
{
    public class EventsContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        {   
            Database.EnsureCreated();
            
        }
    }
}