using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Data;

namespace VOTSDR.Admin.Web.Controllers
{
    public class NewsStoriesController : Controller
    {
        //
        // GET: /NewsStory/

        public ActionResult Index()
        {
            var _db = new DataEntities();
            var stories = _db.NewsStories;
            return View(stories);
        }

        //
        // GET: /NewsStory/Details/5

        public ActionResult Details(Guid id)
        {
            var _db = new DataEntities();
            var story = _db.NewsStories.FirstOrDefault(n => n.NewsStoryId == id);
            return View(story);
        }

        //
        // GET: /NewsStory/Create

        public ActionResult Create()
        {
            ViewBag.FormType = "Create";
            return View();
        } 

        //
        // POST: /NewsStory/Create

        [HttpPost]
        public ActionResult Create(NewsStory story)
        {
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                // we need to gen the primary key
                story.NewsStoryId = Guid.NewGuid();

                // Add and Save
                var _db = new DataEntities();
                _db.NewsStories.AddObject(story);
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
            ViewBag.FormType = "Edit";
            var _db = new DataEntities();
            var story = _db.NewsStories.FirstOrDefault(n => n.NewsStoryId == id);

            return View(story);
        }

        //
        // POST: /NewsStory/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            ViewBag.FormType = "Edit";
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                var _db = new DataEntities();
                
                var newsStory = _db.NewsStories.FirstOrDefault(n => n.NewsStoryId == id);
                UpdateModel(newsStory, collection);
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
        // GET: /NewsStory/Delete/5

        public ActionResult Delete(Guid id)
        {
            var _db = new DataEntities();
            var story = _db.NewsStories.FirstOrDefault(n => n.NewsStoryId == id);

            return View(story);
        }

        //
        // POST: /NewsStory/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var _db = new DataEntities();
                var story = _db.NewsStories.FirstOrDefault(n => n.NewsStoryId == id);
                _db.DeleteObject(story);
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
