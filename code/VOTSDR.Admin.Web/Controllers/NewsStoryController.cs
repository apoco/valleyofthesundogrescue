using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Data;

namespace VOTSDR.Admin.Web.Controllers
{
    public class NewsStoryController : Controller
    {



        //
        // GET: /NewsStory/

        public ActionResult Index()
        {
            var _db = new DataEntities();
            var newsStories = _db.NewsStories;
            
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
        public ActionResult Create(NewsStory newsStory)
        {
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                // we need to gen the primary key
                newsStory.NewsStoryId = Guid.NewGuid();

                // Add and Save
                var _db = new DataEntities();
                _db.NewsStories.AddObject(newsStory);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {                
                ViewBag.exMsg = ex.Message;
                return View();
            }
        }
        
        //
        // GET: /NewsStory/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var _db = new DataEntities();
            var newsStory = _db.NewsStories.Where(n =>  n.NewsStoryId == id);

            return View(newsStory);
        }

        //
        // POST: /NewsStory/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, NewsStory newsStory)
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

        public ActionResult Delete(Guid id)
        {
            var _db = new DataEntities();
            var newsStory = _db.NewsStories.FirstOrDefault(n => n.NewsStoryId == id);

            return View(newsStory);
        }

        //
        // POST: /NewsStory/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var _db = new DataEntities();
                var newsStory = _db.NewsStories.FirstOrDefault(n => n.NewsStoryId == id);
                _db.DeleteObject(newsStory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
