using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Data;
using VOTSDR.Web.Models;
using System.Web.UI;

namespace VOTSDR.Web.Controllers
{
    public class DogController : BaseController
    {
        [OutputCache(Location=OutputCacheLocation.ServerAndClient, Duration=300)]
        public ActionResult Image(Guid id)
        {
            var dog = new DataEntities()
                .Dogs
                .FirstOrDefault(d => d.DogId == id);
            if (dog == null || dog.Image == null)
            {
                return File(Server.MapPath("~/content/images/default-dog-pic.png"), "image/png");
            }
            else
            {
                return File(dog.Image, "image/jpeg");
            }
        }

        [OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration=300)]
        public ActionResult Thumbnail(Guid id)
        {
            var dog = new DataEntities()
                .Dogs
                .FirstOrDefault(d => d.DogId == id);
            if (dog == null || dog.Thumbnail == null)
            {
                return File(Server.MapPath("~/content/images/default-dog-thumb.png"), "image/png");
            }
            else
            {
                return File(dog.Thumbnail, "image/jpeg");
            }
        }

        public ActionResult Available()
        {
            var entities = new DataEntities();
            var latestFeatureDate = entities.Dogs.Max(d => d.DateFeatured);
            var dogs = (
                from dog in entities.Dogs
                where !dog.AdoptedDate.HasValue
                orderby dog.DateFeatured descending 
                select new { 
                    dog.DogId, dog.Profile, dog.Name, dog.Birthday, 
                    dog.DateFeatured, dog.Breed, dog.Gender }
            ).ToList();

            return View(
                from dog in dogs 
                select new AdoptableDogViewModel
                {
                    Id = dog.DogId,
                    Profile = dog.Profile,
                    ImageUrl = Url.Action("Image", new { id = dog.DogId }),
                    ThumbnailUrl = Url.Action(
                        "Thumbnail", new { id = dog.DogId }),
                    Name = dog.Name,
                    Age = GetDogAge(dog.Birthday ?? DateTime.Today),
                    Featured = 
                        dog.DateFeatured.HasValue 
                        && dog.DateFeatured == latestFeatureDate,
                    Breed = dog.Breed,
                    Gender = (dog.Gender ?? "m").ToLower() == "m" ? "Male" : "Female"
                }
            );
        }

        public ActionResult SuccessStories()
        {
            var dogs = (
                from dog in new DataEntities().Dogs
                where dog.AdoptedDate.HasValue
                orderby dog.AdoptedDate descending
                select new { dog.Name, dog.DogId, dog.AdoptionStory }
            ).ToList();

            return View(
                from dog in dogs
                select new SuccessStoryViewModel
                {
                    DogName = dog.Name,
                    ImageUrl = Url.Action("Thumbnail", new { id = dog.DogId }),
                    Story = dog.AdoptionStory,
                }
            );
        }
    }
}
