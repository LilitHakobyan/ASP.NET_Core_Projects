using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FirstWebApplication
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private string _pattern;

        public TokenMiddleware(RequestDelegate next,string pattern)
        {
           this._next = next;
           this._pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token != _pattern+"456")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
            {
               await _next.Invoke(context);
            }
        }
    }
}
