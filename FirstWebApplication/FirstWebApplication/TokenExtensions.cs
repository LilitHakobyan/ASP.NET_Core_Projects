using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace FirstWebApplication
{
    public static class TokenExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder bulder,string pattern)
        {
           return bulder.UseMiddleware<TokenMiddleware>(pattern);
        }
    }
}
