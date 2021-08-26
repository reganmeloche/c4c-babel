using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using RestSharp;

using esdc_rules_classes.MaternityBenefits;
using esdc_rules_classes.AverageIncome;
using esdc_rules_classes.BestWeeks;

using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits;
using esdc_rules_api.OpenFisca;
using esdc_rules_api.BestWeeks;
using esdc_rules_api.AverageIncome;


namespace esdc_rules_api
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EI Rules API",
                    Description = "https://laws-lois.justice.gc.ca/eng/acts/e-5.6/",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            InjectOpenFisca(services);

            // Handlers
            services.AddScoped<IHandleRequests<AverageIncomeRequest, AverageIncomeResponse>, AverageIncomeRequestHandler>();
            services.AddScoped<IHandleRequests<BestWeeksRequest, BestWeeksResponse>, BestWeeksRequestHandler>();
            services.AddScoped<IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse>, MaternityBenefitsRequestHandler>();
            services.AddScoped<IHandleBulkRequests, MaternityBenefitsBulkRequestHandler>();

            // Validators
            services.AddScoped<IValidateRequests<AverageIncomeRequest>, AverageIncomeRequestValidator>();

            // Main Calculators
            services.AddScoped<ICalculateAverageIncome, AverageIncomeCalculator>();
            services.AddScoped<ICalculateBestWeeks, BestWeeksCalculator>();
            //services.AddScoped<ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson>, MaternityBenefitsOpenFiscaCalculator>();
            services.AddScoped<ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson>, MaternityBenefitsDefaultCalculator>();
        
            // Average Income Calculator Helpers
            services.AddScoped<ICreateFullRoes, FullRoeCreator>();
            services.AddScoped<IGetIncomeForOneWeek, WeeklyIncomeGetter>();
            services.AddScoped<IGetIncomeFromOneRoe, IncomeRoeGetter>();
            services.AddScoped<ICreateFullPayPeriods, FullPayPeriodCreator>();
            services.AddScoped<IGetMainStartDate, MainStartDateGetter>();
            services.AddScoped<IGetStartDateFromEndDate, StartDateFromEndDateGetter>();
            services.AddScoped<IGetIncomeList, IncomeListGetter>();

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/spec.json", "EI Rules Spec");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InjectOpenFisca(IServiceCollection services) {
            // OpenFisca
            services.AddScoped<IOpenFisca, OpenFiscaLib>();
            services.AddScoped<IRestClient, RestSharp.RestClient>();

            // OpenFisca options
            var openFiscaUrl = Configuration.GetValue<string>("OpenFiscaOptions:Url") ?? 
                Environment.GetEnvironmentVariable("OPEN_FISCA_URL");
                
            var openFiscaOptions = new OpenFiscaOptions() {
                Url = openFiscaUrl
            };
            services.AddSingleton<IOptions<OpenFiscaOptions>>(x => Options.Create(openFiscaOptions));
        }
    }
}
