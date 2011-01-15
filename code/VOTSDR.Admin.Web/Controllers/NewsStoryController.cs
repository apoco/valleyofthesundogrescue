using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VOTSDR.Admin.Web.Controllers
{
    public class NewsStoryController : Controller
    {
        //
        // GET: /NewsStory/

        public ActionResult Index()
        {
            var db = new VOTSDR.Data.DataEntities();

            var newsStories = db.NewsStories;
            
            return View(newsStories);
        }

        //
        // GET: /NewsStory/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /NewsStory/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /NewsStory/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /NewsStory/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /NewsStory/Edit/5

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
        // GET: /NewsStory/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /NewsStory/Delete/5

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
