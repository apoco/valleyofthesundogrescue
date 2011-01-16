using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Data;

namespace VOTSDR.Admin.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        //
        // GET: /Events/

        public ActionResult Index()
        {
            var _db = new DataEntities();
            var events = _db.Events;

            return View(events);
        }

        //
        // GET: /Events/Details/5

        public ActionResult Details(Guid id)
        {
            var _db = new DataEntities();
            var evt = _db.Events.FirstOrDefault(e => e.EventId == id);

            return View(evt);
        }

        //
        // GET: /Events/Create

        public ActionResult Create()
        {
            ViewBag.FormType = "Create";
            return View();
        } 

        //
        // POST: /Events/Create

        [HttpPost]
        public ActionResult Create(Event evt)
        {
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                // we need to gen the primary key
                evt.EventId = Guid.NewGuid();

                // Add and Save
                var _db = new DataEntities();
                _db.Events.AddObject(evt);
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
        // GET: /Events/Edit/5

        public ActionResult Edit(Guid id)
        {
            ViewBag.FormType = "Edit";
            var _db = new DataEntities();
            var evt = _db.Events.FirstOrDefault(e => e.EventId == id);

            return View(evt);
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            ViewBag.FormType = "Edit";
            ViewBag.exMsg = "";
            try
            {
                if (!ModelState.IsValid) return View();

                var _db = new DataEntities();

                var evt = _db.Events.FirstOrDefault(e => e.EventId == id);
                UpdateModel(evt, collection);
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
        // GET: /Events/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            var _db = new DataEntities();
            var evt = _db.Events.FirstOrDefault(e => e.EventId == id);
            return View(evt);
        }

        //
        // POST: /Events/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var _db = new DataEntities();
                var evt = _db.Events.FirstOrDefault(e => e.EventId == id);
                _db.DeleteObject(evt);
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
