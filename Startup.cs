using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using RestfulApiVisualCode.DataBaseContext;
using RestfulApiVisualCode.Models;


namespace RestfulApiVisualCode
{
    public class Startup
    {
     
        public void ConfigureServices(IServiceCollection services)
        {

             string con = "Server=(localdb)\\mssqllocaldb;Database=eventsdbstore;Trusted_Connection=True;";
            services.AddDbContext<EventsContext>(options => options.UseSqlServer(con));
            
            services.AddControllers(); 
        }

        public void Configure(IApplicationBuilder app)
        {
            var loggerfactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerfactory.CreateLogger<Startup>();
            //app.Run(async (context) =>
            //{
            //    logger.LogInformation("Logg: {0}", context.Request.Path);
            //});
            app.UseDeveloperExceptionPage();
 
            app.UseDefaultFiles();
            app.UseStaticFiles();
 
            app.UseRouting();
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
