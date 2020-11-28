using System;
using System.IO;
using System.Reflection;
using AlphaVantageConnector;
using AlphaVantageConnector.Interfaces;
using Common;
using Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
            services.AddSingleton<IAlphaVantageConnector, AlphaVantageConnector.AlphaVantageConnector>();
            services.AddScoped<IAlphaVantageService, AlphaVantageService>();

            services.AddControllers();
            services.AddSwaggerGen(q =>
            {
                q.SwaggerDoc("v1", new OpenApiInfo { Title = "Analytics API V0.01", Version = "0.01" });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                q.IncludeXmlComments(commentsFile);
            });
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
