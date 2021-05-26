using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasAPI.Infra.Base
{
    public class Configure
    {
        private readonly RequestDelegate _next;

        public Configure(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var communicationJson = (CommunicationJson)httpContext.RequestServices.GetService(typeof(CommunicationJson));
            var configuration = (IConfiguration)httpContext.RequestServices.GetService(typeof(IConfiguration));
            communicationJson.NameFile = configuration.GetSection("FileData:NameFile").Value;
            
            return _next(httpContext);
        }
    }

    public static class ConfigureExtensions
    {
        public static IApplicationBuilder UseConfigure(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Configure>();
        }
    }
}
