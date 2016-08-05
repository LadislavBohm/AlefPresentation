using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Net.Http.Formatting;
using System.Web.Http;
using AlefPresentation.Api.ActionFilters;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Newtonsoft.Json.Serialization;

namespace AlefPresentation.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //globalni nastaveni CORS
            //config.EnableCors(new EnableCorsAttribute("*","*","*"));

            //globalni nastaveni komprese
            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            ConfigFormatters(config);

            ConfigActionFilters(config);

            ConfigRoutes(config);
        }
        
        private static void ConfigFormatters(HttpConfiguration config)
        {
            //vlastni konfigurace formatteru

            config.Formatters.Clear();

            var jsonSerializer = new JsonMediaTypeFormatter
            {
                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            };

            config.Formatters.Add(jsonSerializer);

            //XML formatter

            //config.Formatters.Add(new XmlMediaTypeFormatter());
        }

        private static void ConfigActionFilters(HttpConfiguration config)
        {
            config.Filters.Add(new NLogActionFilter());
        }

        private static void ConfigRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}
