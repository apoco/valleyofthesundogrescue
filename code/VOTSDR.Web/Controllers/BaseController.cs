using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Data;
using VOTSDR.Web.Models;

namespace VOTSDR.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnResultExecuting(
            ResultExecutingContext filterContext)
        {
            // Add "Dog of the Week" info to the view bag
            var dog = new DataEntities()
                .Dogs
                .OrderByDescending(d => d.DateFeatured)
                .Select(d => new
                {
                    Id = d.DogId,
                    Name = d.Name,
                    Birthday = d.Birthday, Breed = d.Breed,
                })
                .FirstOrDefault();

            ViewBag.DogOfTheWeek = new DogOfTheWeekSummaryViewModel {
                Id = dog.Id,
                Name = dog.Name,
                ThumbnailUrl = Url.Action(
                    "Image", "Dog", new { id = dog.Id }),
                Age = GetDogAge(dog.Birthday ?? DateTime.Now),
                Breed = dog.Breed
            };

            base.OnResultExecuting(filterContext);
        }

        protected string GetDogAge(DateTime Birthday)
        {
            TimeSpan span = DateTime.Today.Subtract(Birthday);

            string age = (span.Days <= 30) ? span.Days.ToString() + " days" :
                (span.Days <= 365) ? (span.Days / 30).ToString() + " months" :
                (span.Days >= 365) ? (span.Days / 365).ToString() + " years " +
                    ((span.Days % 365) > 30 ? ((span.Days % 365) / 30).ToString() + " months" : "") :
                "";

            return age;
        }
    }
}