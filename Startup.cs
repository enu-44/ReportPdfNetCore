using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace pmacore_api
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
             services
              .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddCors(options => { options.AddPolicy("CorsPolicy", 
                                      builder => builder.AllowAnyOrigin() 
                                                        .AllowAnyMethod() 
                                                        .AllowAnyHeader() 
                                                        .AllowCredentials()); 
                                  }); 
                                  
            services.AddOptions();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

           // services.AddMvc(); 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
             app.UseCors(builder =>
        builder.WithOrigins("*")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .AllowAnyOrigin()
      );
            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseMvcWithDefaultRoute();


            //app.UseHttpsRedirection();

             app.UseStaticFiles();

              // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(env.WebRootPath)
            });

            app.UseStaticFiles(new StaticFileOptions()  
            {  
            FileProvider = new PhysicalFileProvider(  
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/fonts")),  
                RequestPath = new PathString("/wwwroot/fonts") // accessing outside wwwroot folder contents.  
            }); 

            app.UseStaticFiles(new StaticFileOptions()  
            {  
            FileProvider = new PhysicalFileProvider(  
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Reports")),  
                RequestPath = new PathString("/wwwroot/Reports") // accessing outside wwwroot folder contents.  
            }); 

           

            app.Run(async (context) =>   await context.Response.WriteAsync("Api Report!"));

            app.UseMvc();
        }
    }
}
