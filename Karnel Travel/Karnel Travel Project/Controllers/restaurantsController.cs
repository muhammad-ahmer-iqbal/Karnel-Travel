using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Karnel_Travel_Project;

namespace Karnel_Travel_Project.Controllers
{
    
    public class restaurantsController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: restaurants
        public ActionResult Index()
        {
            ViewBag.Title = "Restauarant";
            return View(db.restaurant.ToList());
        }

        // GET: restaurants/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Restauarant";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant restaurant = db.restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: restaurants/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Restauarant";
            return View();
        }

        // POST: restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rest_id,rest_name,rest_country,rest_charges,rest_rating,rest_description,rest_img,imageFile")] restaurant restaurant)
        {
            var count = db.restaurant.Count();
            try
            {
                if (ModelState.IsValid)
                {
                    string filename = Path.GetFileNameWithoutExtension(restaurant.imageFile.FileName),
                            extension = Path.GetExtension(restaurant.imageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    restaurant.rest_img = "~/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    restaurant.imageFile.SaveAs(filename);

                    count++;
                    restaurant.rest_id = "rest_" + count;

                    db.restaurant.Add(restaurant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            

            return View(restaurant);
        }

        // GET: restaurants/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Restauarant";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant restaurant = db.restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rest_id,rest_name,rest_country,rest_charges,rest_rating,rest_description,rest_img")] restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: restaurants/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Restauarant";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurant restaurant = db.restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            restaurant restaurant = db.restaurant.Find(id);
            db.restaurant.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
