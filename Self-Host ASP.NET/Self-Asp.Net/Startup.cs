using System.Web.Http;
using Owin;
[assembly: Microsoft.Owin.OwinStartup(typeof(Self_Asp.Net.Startup))]
namespace Self_Asp.Net
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            SwaggerConfig.Register(config);
            appBuilder.UseWebApi(config);
        }
    }
}
