using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FirstWebApplication
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddDebug();
            var logger = loggerFactory.CreateLogger("RequestInfoLogger");
            app.UseStaticFiles();
            //app.UseMiddleware<TokenMiddleware>(); we can add extansions and write other form
            string pattern = "123";
            app.UseToken(pattern);//and it will be work correctly
            //http://localhost:49246/home/?token=123456
            app.Map("/home", home =>
            {
                home.Map("/index", Index);
                home.Map("/about", About);
                home.Map("", Home);
            });
           
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Page Not Found");
                logger.LogInformation("Processing request {0}", context.Request.Path);
            });
        }
        private void Index(IApplicationBuilder app)
        {
            app.MapWhen(context => context.Request.Query.ContainsKey("id") &&
                                   context.Request.Query["id"] == "5", HandleId);
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
        private void Home(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("<h1>Home Page</h1>")
            );
        }
    }
}
