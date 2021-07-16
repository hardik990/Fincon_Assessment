using System.Web.Http;
using WebActivatorEx;
using Fincon_Assessment_WebAPI;
using Swashbuckle.Application;
using Microsoft.OpenApi.Models;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Fincon_Assessment_WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {


            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        
                        c.SingleApiVersion("v1", "Fincon Assessment WebAPI");


                        //c.BasicAuth("basic")
                        //    .Description("Basic HTTP Authentication");
                       


                        //c.ApiKey("apiKey")
                        //    .Description("API Key Authentication")
                        //    .Name("apiKey")
                        //    .In("header");

                      
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Fincon Swagger UI");
                        //c.EnableApiKeySupport("apiKey", "header");
                        c.InjectJavaScript(thisAssembly, "Fincon_Assessment_WebAPI.js.basic-authentication.js");
                    });
        }
    }
}
