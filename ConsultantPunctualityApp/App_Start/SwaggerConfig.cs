using System.Web.Http;
using WebActivatorEx;
using ConsultantPunctualityApp;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ConsultantPunctualityApp
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
  .EnableSwagger(c => c.SingleApiVersion("v1", "Consultancy App"))
  .EnableSwaggerUi();
        }
    }
}