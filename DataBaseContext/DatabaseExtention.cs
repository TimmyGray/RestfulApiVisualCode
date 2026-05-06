using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RestfulApiVisualCode.DataBaseContext
{
    public static class DataBaseExtention
    {
        public static void CreateDataBase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<EventsContext>();
            if (dbContext.Database.IsRelational())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
