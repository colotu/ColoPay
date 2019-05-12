using System.Web.Http;
using ColoPay.WebApi.Filter;

namespace ColoPay.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //注册全局异常处理
           // config.Filters.Add(new WebApiErrorHandlerAttribute());
            //跨域处理
           // config.MessageHandlers.Add(new CrossDomainHandler());
        }
    }
}
