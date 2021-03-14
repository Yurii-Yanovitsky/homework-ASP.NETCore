using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleApp.Models;

namespace SimpleApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<ProductReader>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting(); ;

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                        name: "Default1",
                        pattern: "{controller}/{action}/{id:int}"
                    );

                endpoints.MapControllerRoute(
                        name: "Default2",
                        pattern: "{controller=Home}/{action=Index}/{category?}"
                    );


            });
        }
    }
}
