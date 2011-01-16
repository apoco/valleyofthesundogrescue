using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VOTSDR.Data;
using VOTSDR.Utils;
using VOTSDR.Admin.Web.Models;

namespace VOTSDR.Admin.Web.Controllers
{
    [Authorize]
    public class SpecialNeedsStoriesController : Controller
    {
        //
        // GET: /SpecialNeedsStories/

        public ActionResult Index()
        {
            var _db = new DataEntities();
            var stories = _db.SpecialNeedsStories;
            return View(stories);
        }

        //
        // GET: /SpecialNeedsStories/Details/5

        public ActionResult Details(Guid id)
        {
            var _db = new DataEntities();
            var story = _db.SpecialNeedsStories.FirstOrDefault(n => n.SpecialNeedsStoryId == id);
            return View(story);
        }

        //
        // GET: /SpecialNeedsStories/Create

        public ActionResult Create()
        {
            ViewBag.FormType = "Create";
            return View();
        } 

        //
        // POST: /SpecialNeedsStories/Create

        [HttpPost]
        public ActionResult Create(SpecialNeedsStory story)
        {
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                // we need to gen the primary key
                story.SpecialNeedsStoryId = Guid.NewGuid();


                if (Request.Files != null & Request.Files.Count > 0)
                {
                    HttpPostedFileBase storyImageFile = Request.Files["image"];
                    if (storyImageFile != null)
                        story.Image = ImageUtils.GetBytes(storyImageFile.InputStream);
                }

                // Add and Save
                var _db = new DataEntities();
                _db.SpecialNeedsStories.AddObject(story);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.exMsg = ex.Message;
                return View();
            }
        }
        
        //
        // GET: /SpecialNeedsStories/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            ViewBag.FormType = "Edit";
            var _db = new DataEntities();
            var sns = _db.SpecialNeedsStories.FirstOrDefault(n => n.SpecialNeedsStoryId == id);
            return View(sns);
        }

        //
        // POST: /SpecialNeedsStories/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            ViewBag.FormType = "Edit";
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                var _db = new DataEntities();
                var story = _db.SpecialNeedsStories.FirstOrDefault(n => n.SpecialNeedsStoryId == id);
                
                UpdateModel(story, collection);
                
                if (Request.Files != null & Request.Files.Count > 0)
                {
                    HttpPostedFileBase storyImageFile = Request.Files["dogImage"];
                    if (storyImageFile != null && storyImageFile.ContentLength > 0)
                        story.Image = ImageUtils.GetBytes(storyImageFile.InputStream);
                }


                _db.SaveChanges();
 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.exMsg = ex.Message;
                return View();
            }
        }

        //
        // GET: /SpecialNeedsStories/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            var _db = new DataEntities();
            var story = _db.SpecialNeedsStories.FirstOrDefault(n => n.SpecialNeedsStoryId == id);
            return View(story);
        }

        //
        // POST: /SpecialNeedsStories/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var _db = new DataEntities();
                var story = _db.SpecialNeedsStories.FirstOrDefault(n => n.SpecialNeedsStoryId == id);
                _db.DeleteObject(story);
                _db.SaveChanges();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Image(Guid id)
        {

            var _db = new DataEntities();
            var story = _db.SpecialNeedsStories.FirstOrDefault(n => n.SpecialNeedsStoryId == id);
            if (story == null || story.Image == null)
            {
                return null;
            }
            else
            {
                return File(story.Image, "image/jpeg");
            }
        }


    }
}
