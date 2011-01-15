using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Data;
using VOTSDR.Web.Models;

namespace VOTSDR.Web.Controllers
{
    public class DogController : Controller
    {
        public ActionResult Image(Guid id)
        {
            var dog = new DataEntities()
                .Dogs
                .FirstOrDefault(d => d.DogId == id);
            if (dog == null)
            {
                throw new HttpException(404, "Not Found");
            }

            return File(dog.Image, "image/jpeg");
        }

        public ActionResult Thumbnail(Guid id)
        {
            var dog = new DataEntities()
                .Dogs
                .FirstOrDefault(d => d.DogId == id);
            if (dog == null)
            {
                throw new HttpException(404, "Not Found");
            }

            return File(dog.Thumbnail, "image/jpeg");
        }

        public ActionResult Available()
        {
            var dogs = new List<Dog>(
                from dog in new DataEntities().Dogs
                where !dog.AdoptedDate.HasValue
                orderby dog.DateFeatured
                select dog);
            return View(
                from dog in dogs 
                select new AdoptableDogViewModel
                {
                    Id = dog.DogId,
                    Profile = dog.Profile,
                    ImageUrl = Url.Action("Thumbnail", new { id = dog.DogId }),
                    Name = dog.Name
                }
            );
        }

        public ActionResult SuccessStories()
        {
            var dogs =
                from dog in new DataEntities().Dogs
                where dog.AdoptedDate.HasValue
                orderby dog.AdoptedDate descending
                select new SuccessStoryViewModel { 
                    //ImageUrl = 
                    Story = dog.AdoptionStory,
                };
            return View(

            );
        }
    }
}
