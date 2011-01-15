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
                    ImageUrl = Url.Action(
                        "Image", "Image", new { id = dog.ThumbnailUrl } ),
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
