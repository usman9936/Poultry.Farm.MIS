using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //AddIdentity() method adds the default identity system configuration for the specified user and role types.
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();


            //We can override password default settings in asp.net core identity by, using the Configure() method of the IServiceCollection interface in the ConfigureServices() method of the Startup class like below

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            });

            //or we can also do this while adding Identity services
            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //    options.Password.RequireNonAlphanumeric = false;
            //}).AddEntityFrameworkStores<AppDbContext>();

            //Telling .Net that what db we are going to use and DbConnection is defined in Appsetting
            services.AddDbContextPool<AppDbContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("DbConnection")));

            //services.AddMvc();
            //To apply [Authorize] attribute globally on all controllers and controller actions throughout your application
            //modify the code in ConfigureServices method of the Startup class.
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            //services.AddSingleton<IEmployeeRepository, EmployeeRepository>();//In memory repository of Employees
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();//SQL repository of Employees

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
            else
            {
                //UseStatusCodePagesWithRedirects middleware component intercepts the 404 status code and as the name implies, issues a redirect to the provided error path (in our case "/Error/404")

                //app.UseStatusCodePagesWithRedirects("/Error/{0}");

                //This is a clever piece of middleware. As the name implies it re-executes the pipeline keeping the correct (404) status code. It just returns the custom view (NotFound) HTML
                //As it is just re - executing the pipeline and not issuing a redirect request, we also preserve the original URL(/ foo / bar) in the address bar.It does not change from / foo / bar to / Error / 404.
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }


            app.UseRouting();

            app.UseAuthentication();

            //app.UseMvcWithDefaultRoute(); set the default route request processing which is by default home controller and index action method

            //Configuring custome route but following code is exactly identical as default
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{action=index}/{id?}");
            });

            //or anyother route in our case its login page
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=account}/{action=login}");
            //});

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
