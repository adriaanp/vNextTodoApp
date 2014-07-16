using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Routing;

namespace vNextTodoApp
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
            app.UseServices(services =>
            {
                services.AddMvc();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{id?}"
                );
            });
        }
    }
}
