using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VOTSDR.Admin.Web.Controllers
{
    public class DogsController : Controller
    {
        //
        // GET: /Dogs/

        public ActionResult Index()
        {
            Data.DataEntities de = new Data.DataEntities();
            

            return View(de.Dogs);
        }

        //
        // GET: /Dogs/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
                Data.DataEntities de = new Data.DataEntities();
                Data.Dog dog = Data.Dog.CreateDog(Guid.NewGuid());
                dog.Name = collection["Name"];
                dog.Profile = collection["Profile"];
                dog.Gender = collection["Gender"];
                dog.Breed = collection["Breed"];

                DateTime birthday = new DateTime();
                if (DateTime.TryParse(collection["Birthday"], out birthday))
                    dog.Birthday = birthday;

                de.Dogs.AddObject(dog);
                de.SaveChanges();                
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        
        //
        // GET: /Dogs/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Dogs/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dogs/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Dogs/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
