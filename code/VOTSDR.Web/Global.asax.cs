using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VOTSDR.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "DogImage", "dogs/{id}/image.jpg",
                new { controller = "Dog", action = "Image" });

            routes.MapRoute(
                "DogThumbnail", "dogs/{id}/thumbnail.jpg",
                new { controller = "Dog", action = "Thumbnail" });

            routes.MapRoute(
                "AvailableDogs", "dogs/",
                new { controller = "Dog", action = "Available" });

            routes.MapRoute(
                "SuccessStories", "dogs/adopted",
                new { controller = "Dog", action = "SuccessStories" });

            routes.MapRoute(
                "HowToHelp", "help",
                new { controller = "Home", action = "HowToHelp" });

            routes.MapRoute(
                "Education", "education",
                new { controller = "Home", action = "Education" });

            routes.MapRoute(
                "SponsorsAndLinks", "sponsors-and-links",
                new { controller = "Home", action = "SponsorsAndLinks" });

            routes.MapRoute(
                "Shop", "shop",
                new { controller = "Home", action = "Shop" });

            routes.MapRoute(
                "ContactUs", "contact",
                new { controller = "Home", action = "ContactUs" });

            routes.MapRoute(
                "ContactUsSuccess", "contact",
                new { controller = "Home", action = "ContactUsSuccess" });

            routes.MapRoute(
                "NewsAndEvents", "news",
                new { controller = "News", action = "Index" });

            routes.MapRoute(
                "SpecialNeedsImage", "news/special-needs/{id}.jpg",
                new { controller = "News", action = "SpecialNeedsImage" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}