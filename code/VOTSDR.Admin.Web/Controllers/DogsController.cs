using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using VOTSDR.Data;

namespace VOTSDR.Admin.Web.Controllers
{
    [Authorize]
    public class DogsController : Controller
    {
        //
        // GET: /Dogs/
        private const int pageSize = 10;

        public ActionResult Index(string viewType = "Available", int pageNumber = 1)
        {
            Data.DataEntities de = new DataEntities();

            int totalDogs;

            IEnumerable<Dog> dogs = null;
            switch (viewType)
            {
                case "Adopted":
                    totalDogs = de.Dogs
                        .Where(w=>w.AdoptedDate != null)
                        .Count();
                    break;
                case "Available":
                default:
                    totalDogs = de.Dogs
                        .Where(w => w.AdoptedDate == null)
                        .Count();
                    break;
            }

            int totalPages = (int)Math.Ceiling((double)totalDogs / (double)pageSize);
            ViewBag.totalPages = totalPages;
            ViewBag.currentPage = pageNumber;
            ViewBag.viewType = viewType;

            switch (viewType)
            {
                case "Adopted":
                    dogs = de.Dogs
                        .OrderBy(o => o.Name)
                        .Where(w => w.AdoptedDate != null)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case "Available":
                default:
                    dogs = de.Dogs
                        .OrderBy(o => o.Name)
                        .Where(w => w.AdoptedDate == null)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
            }

            return View(dogs);
        }

        //
        // GET: /Dogs/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Dogs/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                DataEntities de = new DataEntities();
                Dog dog = Dog.CreateDog(Guid.NewGuid());
                dog.Name = collection["Name"];
                dog.Profile = collection["Profile"];
                dog.Gender = collection["Gender"];
                dog.Breed = collection["Breed"];

                DateTime birthday = new DateTime();
                if (DateTime.TryParse(collection["Birthday"], out birthday))
                    dog.Birthday = birthday;

                if (Request.Files != null & Request.Files.Count > 0)
                {
                    HttpPostedFileBase dogImageFile = Request.Files["dogImage"];
                    if (dogImageFile != null && dogImageFile.ContentLength > 0)
                        dog.Image = GetBytes(dogImageFile.InputStream);

                    HttpPostedFileBase dogThumbnailFile = Request.Files["dogThumbnail"];
                    if (dogThumbnailFile != null && dogImageFile.ContentLength > 0)
                        dog.Thumbnail = GetBytes(dogThumbnailFile.InputStream);
                }

                de.Dogs.AddObject(dog);
                de.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private byte[] GetBytes(Stream stream)
        {
            long fileLength = stream.Length;
            Byte[] bits = new byte[fileLength];
            stream.Read(bits, 0, (int)fileLength);
            return bits;
        }

        public ActionResult Image(Guid id)
        {
            var dog = new DataEntities()
                .Dogs
                .FirstOrDefault(d => d.DogId == id);
            if (dog == null || dog.Image == null)
            {
                return null;
            }
            else
            {
                return File(dog.Image, "image/jpeg");
            }
        }

        public ActionResult Thumbnail(Guid id)
        {
            var dog = new DataEntities()
                .Dogs
                .FirstOrDefault(d => d.DogId == id);
            if (dog == null || dog.Thumbnail == null)
            {
                return null;
            }
            else
            {
                return File(dog.Thumbnail, "image/jpeg");
            }
        }

        //
        // GET: /Dogs/Edit/5

        public ActionResult Edit(Guid id)
        {
            DataEntities de = new DataEntities();
            Dog dog = de.Dogs.FirstOrDefault<Dog>(d => d.DogId == id);

            return View(dog);
        }

        //
        // POST: /Dogs/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                DataEntities de = new DataEntities();
                Dog dog = (Dog)de.Dogs.FirstOrDefault<Dog>(d => d.DogId == id);

                dog.Name = collection["Name"];
                dog.Profile = collection["Profile"];
                dog.Gender = collection["Gender"];
                dog.Breed = collection["Breed"];
                dog.AdoptionStory = collection["AdoptionStory"];

                DateTime birthday = new DateTime();
                if (DateTime.TryParse(collection["Birthday"], out birthday))
                    dog.Birthday = birthday;

                DateTime adoptedDate = new DateTime();
                if (DateTime.TryParse(collection["AdoptedDate"], out adoptedDate))
                    dog.AdoptedDate = adoptedDate;

                if (Request.Files != null & Request.Files.Count > 0)
                {
                    HttpPostedFileBase dogImageFile = Request.Files["dogImage"];
                    if (dogImageFile != null && dogImageFile.ContentLength > 0)
                        dog.Image = GetBytes(dogImageFile.InputStream);

                    HttpPostedFileBase dogThumbnailFile = Request.Files["dogThumbnail"];
                    if (dogThumbnailFile != null && dogThumbnailFile.ContentLength > 0)
                        dog.Thumbnail = GetBytes(dogThumbnailFile.InputStream);
                }

                UpdateModel<Dog>(dog);
                de.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dogs/Delete/5

        public ActionResult Delete(Guid id)
        {
            DataEntities de = new DataEntities();
            Dog dog = de.Dogs.FirstOrDefault<Dog>(d => d.DogId == id);


            return View(dog);
        }

        //
        // POST: /Dogs/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                DataEntities de = new DataEntities();
                Dog dog = de.Dogs.FirstOrDefault<Dog>(d => d.DogId == id);
                if (dog != null)
                    de.Dogs.DeleteObject(dog);
                de.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
