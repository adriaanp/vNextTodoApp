using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.StaticFiles;
using Microsoft.AspNet.FileSystems;
using System.IO;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Newtonsoft.Json;
using Microsoft.AspNet.Mvc;

namespace vNextTodoApp
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

            var config = new Configuration();
            config.AddJsonFile("config.json");
            config.AddEnvironmentVariables();

            //SetupNHibernate();


            app.UseFileServer(new FileServerOptions()
            {
                EnableDirectoryBrowsing = false,
                FileSystem = new PhysicalFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app"))
            });

            app.UseServices(services =>
            {
               services.AddEntityFramework()
                .AddSqlServer();

                services.SetupOptions<DbContextOptions>(options =>
                {
                    options.UseSqlServer(config.Get("Data:DefaultConnection:ConnectionString"));
                });

                services.AddMvc();

                //services.SetupOptions<MvcOptions>(options =>
                //{
                //    var t = options.OutputFormatters.Find(d => d.OutputFormatterType == typeof(JsonOutputFormatter));
                //    if (t != null)
                //    {
                //        (t.OutputFormatter as JsonOutputFormatter).
                //    }
                //});
                services.AddScoped<TaskDbContext>();
                
                //NHibernate

            });


            //Json
            //JsonConvert.DefaultSettings().ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "mvc",
                    template: "{controller}/{action}/{id?}"
                    );

                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{id?}"
                );
            });
        }
    }
}
