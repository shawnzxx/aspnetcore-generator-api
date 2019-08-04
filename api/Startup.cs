using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace api
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
            services.AddMvc()
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Generate Random Data API",
                        Version = "v1",
                        Description = "Through this API you can access audit functions",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "shawn.zhang@avanade.com",
                            Name = "Shawn Zhang",
                            Url = new Uri("https://www.linkedin.com/in/shawnzxx/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                //var xmlCommentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlCommentsFileFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFileName);

                //setupAction.IncludeXmlComments(xmlCommentsFileFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();

            //we add at back of UseHttpsRedirection, so that all link to swagger website using http will redirect to https site
            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //Enable middleware to serve Swagger UI (HTML, CSS, JS etc.)
            //Specifying the Swagger JSON endpoint
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Generate Random Data API V1");
                setupAction.RoutePrefix = "";
            });

            var redirectRootToSwagger = new RewriteOptions()
                .AddRedirect("^$", "swagger");
            app.UseRewriter(redirectRootToSwagger);
        }
    }
}
