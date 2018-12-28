using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ColoPay.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // 使api返回为json 
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
        /// <summary>
        /// 是否开启自动连接
        /// </summary>
        public static bool IsAutoConn => YSWL.Common.ConfigHelper.GetConfigBool("AutoConnection");

    }
}
