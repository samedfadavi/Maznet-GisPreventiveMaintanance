using pmServiceu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace pmService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");

            // Handle preflight requests (OPTIONS)
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                // Respond to preflight requests 
                HttpContext.Current.Response.End();
            }
        }
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
