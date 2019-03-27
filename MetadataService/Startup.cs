using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DataRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace MetadataService
{
    public class Startup
    {
        private readonly IHostingEnvironment _currentEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this._currentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("InsuranceConnection");
            services.AddDbContext<InsuranceContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IMetadataRepository, MetadataRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddAutoMapper();
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAnyOriginPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            if (_currentEnvironment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration["Cache:AWSRedisEndPoint"];
                });
            }
            services.AddHealthChecks();
            //  services.AddSession(options =>
            // {
            //     // Set a short timeout for easy testing.
            //     options.IdleTimeout = TimeSpan.FromSeconds(60);
            //     options.Cookie.HttpOnly = true;
            // });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Vehicel Insurance Metadata API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Create a logging provider based on the configuration information passed through the appsettings.json
            loggerFactory.AddAWSProvider(this.Configuration.GetAWSLoggingConfigSection());
            app.UseHealthChecks("/health");
            // Enable middleware to serve generated Swagger as a JSON endpoint.
             app.UseSwagger(c =>
            {
                c.RouteTemplate = Constants.APIPath+"/swagger/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/"+Constants.APIPath+"/swagger/v1/swagger.json", "Vehicel Insurance Metadata API V1");
                c.RoutePrefix = Constants.APIPath+"/swagger";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("AllowAnyOriginPolicy");
            app.UseHttpsRedirection();
            app.UseErrorHandlingMiddleware();
            app.UseMvc();
        }
    }
}
