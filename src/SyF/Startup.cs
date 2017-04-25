using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SyF.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using SyF.ViewModels;
using SyF.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SyF
{
    public class Startup
    {
        private IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath) //contentroot is the root of our project, where our config file is
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

               _config = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

          

            services.AddDbContext<SyFContext>();

           
           
            services.AddLogging();

            services.AddScoped<ISyFRepository, SyFRepository>();

            services.AddTransient<EdamamService>();

            services.AddTransient<SyFSeedData>();

            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddIdentity<SyFUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 8;
                config.User.RequireUniqueEmail = true;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api/") && 
                        ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401; // if we get an OK status and we are triying to redirect on login we will redirect to api
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        
                        }

                        await Task.Yield();//complete the task

                    }

                };
            })
            .AddEntityFrameworkStores<SyFContext>(); //we will store Identities in our dbcontext SyFContext


        }
        public void Configure(IApplicationBuilder app,
       IHostingEnvironment env,
       ILoggerFactory factory,
       SyFSeedData seeder)
        {

            app.UseIdentity();//we turn on Identity before anything else


            Mapper.Initialize(config =>

            {
                config.CreateMap<RecipeViewModel, Recipe>().ReverseMap();//map from viewmodel to entity and reverse it so we have bidirectional mapping
                config.CreateMap<IngredientViewModel, Ingredient>().ReverseMap();

            }

            );

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                factory.AddDebug(LogLevel.Information);
            }
            else
            {
                factory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();

        

            app.UseMvc(config =>
            {
                config.MapRoute(
                  name: "Default",
                  template: "{controller}/{action}/{id?}",
                  defaults: new { controller = "App", action = "Index" }
                  );
            });

            seeder.SeedData().Wait(); //we can't make an async call hence we are using wait instead



        }
    }
}
