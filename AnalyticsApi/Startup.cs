using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaVantageConnector;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Validation;
using Common;
using Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AnalyticsApi
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

            var apiHttpClient = HttpClientManager.GetRateLimitClient(@"https://www.alphavantage.co/query", rateLimit: 1000, maxConcurrentRequests:7);
            services.AddSingleton(x => apiHttpClient);

            services.AddSingleton<IApiKeyService, ApiKeyService>();
            services.AddSingleton<IApiCallValidator, ApiCallValidator>();

            services.AddSingleton<IRequestCompositor, RequestCompositor>();

            services.AddSingleton<IAlphaVantageConnector, AlphaVantageConnector.AlphaVantageConnector>();
            services.AddScoped<IAlphaVantageService, AlphaVantageService>();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Analytics API V0.01");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
