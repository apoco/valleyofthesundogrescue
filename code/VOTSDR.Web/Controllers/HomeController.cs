using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Web.Models;

namespace VOTSDR.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(
                new HomeIndexViewModel
                {
                    FeaturedDogDescription = "Featured dog description",
                    FeaturedDogName = "Bingo",
                    FeaturedDogProfile = "Featured dog profile",
                    FeaturedDogThumbnailUrl = "http://www.dogbreedinfo.com/images18/ChihuahuaViansBigMacAttackMac3.JPG",

                    SpecialNeedThumbnailUrl = "http://veterinarianlisting.net/files/2010/03/vet_5.jpg",
                    SpecialNeedDescription = "This is our special needs",

                    NewsAndEvents = new List<NewsOrEventSummaryViewModel>
                    {
                        new NewsOrEventSummaryViewModel { 
                            Date = new DateTime(2011, 02, 15),
                            Title = "Arizona Pitbulls Something",
                            Summary = "Bunch of news",
                        },
                        new NewsOrEventSummaryViewModel {
                            Date = new DateTime(2011, 01, 01),
                            Title = "So and so blah blah",
                            Summary = "Here's what's going on...",
                        },
                    },
                }
            );
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
