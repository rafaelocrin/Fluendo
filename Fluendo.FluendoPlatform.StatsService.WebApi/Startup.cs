using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Autofac;
using Autofac.Configuration;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Fluendo.FluendoPlatform.StatsService.WebApi
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
            services.AddLocalization(o => { o.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                CultureInfo[] supportedCultures = new[]
                {
                    new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpClient();



            services.Configure<ApplicationOptions>(Configuration.GetSection("ApplicationOptions"));

            //services.AddLocalization(o => o.ResourcesPath = "Resources");
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new[]{ new CultureInfo("en-US")};

            //    //options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
            //    options.DefaultRequestCulture = new RequestCulture("en-US");

            //    options.SupportedCultures = supportedCultures;

            //    options.SupportedUICultures = supportedCultures;
            //});

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Fluendo StatsService WebApi",
                    Description = "Fluendo Stats Updater Service WebApi"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(options.Value);

            app.UseRequestLocalization(); //before app.UseMvc()

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fluendo StatsService WebApi V1");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ConfigurationModule(Configuration));
        }
    }
}
