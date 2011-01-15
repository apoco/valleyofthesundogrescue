using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Web.Models;
using VOTSDR.Data;
using VOTSDR.Utils;

namespace VOTSDR.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var entities = new DatabaseEntities();

            var news =
                from article in entities
                    .NewsStories
                    .OrderByDescending(s => s.Date)
                    .Take(10)
                    .ToList()
                select new NewsOrEventSummaryViewModel
                {
                    Id = article.NewsStoryId,
                    Date = article.Date,
                    Title = article.Title,
                    Summary = article.Text.Summarize(200),
                };

            var events =
                from evt in entities
                    .Events
                    .OrderByDescending(e => e.Date)
                    .Take(10)
                    .ToList()
                select new NewsOrEventSummaryViewModel
                {
                    Id = evt.EventId,
                    Date = evt.Date,
                    Title = evt.Name,
                    Summary = evt.Description.Summarize(200),
                };

            var viewData = new HomeIndexViewModel
            {
                NewsAndEvents = news
                    .Concat(events)
                    .OrderByDescending(i => i.Date),
            };

            // Load the latest featured dog
            var featuredDog = entities
                .Dogs
                .OrderByDescending(d => d.DateFeatured)
                .Where(d => d.DateFeatured.HasValue)
                .FirstOrDefault();
            if (featuredDog != null)
            {
                viewData.FeaturedDogThumbnailUrl = Url.Action(
                    "Image", "Image", 
                    new { id = featuredDog.ThumbnailUrl } );
                viewData.FeaturedDogName = featuredDog.Name;
                viewData.FeaturedDogProfile = featuredDog.Profile;
            }

            var featuredNeed = entities
                .SpecialNeedsStories
                .Where(s => s.IsFeatured)
                .OrderByDescending(s => s.DateCreated)
                .FirstOrDefault();
            if (featuredNeed != null)
            {
                viewData.SpecialNeedThumbnailUrl =Url.Action(
                    "Image", "Image",
                    new { id = featuredNeed.ImageUrl });
                viewData.SpecialNeedDescription = featuredNeed.Text;
            }

            return View(viewData);
        }

        public ActionResult HowToHelp()
        {
            return View();
        }

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult SponsorsAndLinks()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
    }
}
