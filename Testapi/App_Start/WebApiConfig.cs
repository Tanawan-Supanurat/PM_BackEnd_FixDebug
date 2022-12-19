using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Testapi
{
    public static class WebApiConfig
    {
        
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute(origins: "*",
                                              headers: "*",
                                              methods: "*");

            config.EnableCors(cors);
            //Cors　の設定を有効にします。
            // Web API の設定およびサービス

            // Web API ルート
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

        }
    }
}
