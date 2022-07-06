using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poultry.Farm.MIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poultry.Farm.MIS
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //check the environment variable in launchSetting.Json file which store launch config for running project locally and AppSeting.Json is used for otherwise
            if (env.IsDevelopment()) 
            {
                // customize DeveloperExceptionPage using DeveloperExceptionPageOptions
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();

                //Set number of lines of source code to show in exceptions window in browser 
                developerExceptionPageOptions.SourceCodeLineCount = 10; 
                
                app.UseDeveloperExceptionPage(developerExceptionPageOptions); //Enable exceptions window in browser to trace errors and should be registered as early as possible
            }

            
            app.UseRouting();

            //app.UseMvcWithDefaultRoute(); set the default route request processing which is by default home controller and index action method

            //Configuring custome route but following code is exactly identical as default
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{action=index}/{id?}");
            });

            app.UseStaticFiles();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //Create an exception message at run time
            //        throw new Exception("Processing error");

            //        await context.Response.WriteAsync("Hello World!");
            //       // await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName); To check which  process is hosting and running app
            //    });
            //});
        }
    }
}
