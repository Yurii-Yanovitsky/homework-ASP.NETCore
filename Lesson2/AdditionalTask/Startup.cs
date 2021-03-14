using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdditionalTask.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;

namespace AdditionalTask
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<Calculator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Calculator",
                    pattern: "{controller}/{action}/{x}/{y}",
                    constraints: new
                    {
                        x = new DoubleRouteConstraint(),
                        y = new DoubleRouteConstraint()
                    });
            });
        }
    }
}
