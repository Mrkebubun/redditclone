using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;



namespace RedditClone
{
    public class GlobalApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add(new Route("Users/{id}", new MvcRouteHandler())
                {
                    Defaults = new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "UserInformation",
                    id = ""}),
                });
            routes.Add(new Route("submit", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new { controller = "Item", action = "SubmitNew" }),
            });
            routes.Add(new Route("Register", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new { controller = "Account", action = "Register" }),
            });
            routes.Add(new Route("Login", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new { controller = "Account", action = "Login" }),
            });
            routes.Add(new Route("Main", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new { controller = "Item", action = "Main" }),
            });
            routes.Add(new Route("", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new { controller = "Item", action = "Main" }),
            });
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Item", action = "Main", id = "" }  // Parameter defaults
            );
            
            // Note: Change the URL to "{controller}.mvc/{action}/{id}" to enable
            //       automatic support on IIS6 and IIS7 classic mode

            //routes.Add(new Route("Login", new MvcRouteHandler())
            //{
            //    Defaults = new RouteValueDictionary(new { controller = "UserInfo", action = "LoginPage" }),
            //});
            //routes.Add(new Route("Register", new MvcRouteHandler())
            //{
            //    Defaults = new RouteValueDictionary(new { controller = "UserInfo", action = "RegisterPage" }),
            //});
            //routes.Add(new Route("Main", new MvcRouteHandler())
            //{
            //    Defaults = new RouteValueDictionary(new { controller = "Item", action = "Main" }),
            //});
            //routes.Add(new Route("{controller}/{action}/{id}", new MvcRouteHandler())
            //{
            //    Defaults = new RouteValueDictionary(new { action = "Index", id = "" }),
            //});

            //routes.Add(new Route("Default.aspx", new MvcRouteHandler())
            //{
            //    Defaults = new RouteValueDictionary(new { controller = "Home", action = "Index", id = "" }),
            //});
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}