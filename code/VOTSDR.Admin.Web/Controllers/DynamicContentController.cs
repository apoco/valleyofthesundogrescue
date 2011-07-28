using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VOTSDR.Data;

namespace VOTSDR.Admin.Web.Controllers
{
    public class DynamicContentController : Controller
    {
        //
        // GET: /Content/
        [Authorize]
        public ActionResult Index()
        {
            var _db = new DataEntities();
            var contents = _db.Contents;
            return View(contents);
        }

        //
        // GET: /Content/Details/5

        [Authorize]
        public ActionResult Details(Guid id)
        {
            var _db = new DataEntities();
            var content = _db.Contents.FirstOrDefault(e => e.ContentId == id);

            return View(content);
        }


        //
        // GET: /Content/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.FormType = "Create";
            return View();
        } 

        //
        // POST: /Content/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Content content)
        {
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                // we need to gen the primary key
                content.ContentId = Guid.NewGuid();

                // Add and Save
                var _db = new DataEntities();
                _db.Contents.AddObject(content);
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
        // GET: /Content/Edit/5
        [Authorize]
        public ActionResult Edit(Guid id)
        {
            ViewBag.FormType = "Edit";

            var _db = new DataEntities();
            var content = _db.Contents.FirstOrDefault(e => e.ContentId == id);

            return View(content);
        }

        //
        // POST: /Content/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            ViewBag.FormType = "Edit";
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                var _db = new DataEntities();
                var content = _db.Contents.FirstOrDefault(e => e.ContentId == id);

                UpdateModel(content, collection);
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
        // GET: /Content/Delete/5
        [Authorize]
        public ActionResult Delete(Guid id)
        {

            var _db = new DataEntities();
            var content = _db.Contents.FirstOrDefault(e => e.ContentId == id);

            return View(content);
        }

        //
        // POST: /Content/Delete/5

        [HttpPost]
        [Authorize]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var _db = new DataEntities();
                var content = _db.Contents.FirstOrDefault(e => e.ContentId == id);

                _db.DeleteObject(content);
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
