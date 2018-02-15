using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FirstWebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/home", home =>
            {
                
                home.Map("/index", Index);
                home.Map("/about", About);
            });
           
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Page Not Found");
            });
        }
        private void Index(IApplicationBuilder app)
        {
            app.MapWhen(context => {

                return context.Request.Query.ContainsKey("id") &&
                       context.Request.Query["id"] == "5";
            }, HandleId);
            app.Run(async context=> await  context.Response.WriteAsync("<h1>Index</h1>")
            );
        }
        //http://localhost:49246/home/index/product?id=5&name=phone
        private static void HandleId(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("id is equal to 5");
            });
        }
        private void About(IApplicationBuilder app)
        {
            app.Run(async context=> await  context.Response.WriteAsync("<h1>About</h1>")
            );
        }
    }
}
