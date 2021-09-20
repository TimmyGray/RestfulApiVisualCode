using Microsoft.EntityFrameworkCore;
using RestfulApiVisualCode.Models;
namespace RestfulApiVisualCode.DataBaseContext
{
     public class WarningEventContext : DbContext
    {
        public DbSet<WarningEvent> warningevents { get; set; }
        public WarningEventContext(DbContextOptions<WarningEventContext> options)
            : base(options)
        { 
            
            Database.EnsureCreated();
        }
    }
}