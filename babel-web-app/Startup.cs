using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using RestSharp;

using babel_web_app.Lib;

namespace babel_web_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Localization
            services.AddLocalization(opt => {opt.ResourcesPath = "Resources";});
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(opt => {
                var supportedCultures = new List<CultureInfo>() {
                    new CultureInfo("en"),
                    new CultureInfo("fr")
                };
                opt.DefaultRequestCulture = new RequestCulture("en");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
            });
            
            
            services.AddControllersWithViews();

            services.AddScoped<IHandleSimulationRequests, SimulationRequestHandler>();

            services.AddScoped<IRestClient, RestSharp.RestClient>();
            services.AddScoped<SimulationApi>();

            // Simulation Engine Options
            var simulationUrl = Configuration["SimulationEngineOptions:Url"] ?? 
                Environment.GetEnvironmentVariable("SIMULATION_ENGINE_URL");
            var simOptions = new SimulationEngineOptions() {
                Url = simulationUrl
            };
            services.AddSingleton<IOptions<SimulationEngineOptions>>(x => Options.Create(simOptions));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Localization
            app.UseRequestLocalization(
                app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
