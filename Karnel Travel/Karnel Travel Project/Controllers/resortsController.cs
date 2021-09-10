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
    
    public class resortsController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: resorts
        public ActionResult Index()
        {
            ViewBag.Title = "Resort";
            return View(db.resort.ToList());
        }

        // GET: resorts/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Resort";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resort resort = db.resort.Find(id);
            if (resort == null)
            {
                return HttpNotFound();
            }
            return View(resort);
        }

        // GET: resorts/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Resort";
            return View();
        }

        // POST: resorts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reso_id,reso_name,reso_country,reso_charges,reso_roomAvailable,reso_rating,reso_description,reso_img,imageFile")] resort resort)
        {
            var count = db.resort.Count();
            try
            {
                if (ModelState.IsValid)
                {
                    string filename = Path.GetFileNameWithoutExtension(resort.imageFile.FileName),
                            extension = Path.GetExtension(resort.imageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    resort.reso_img = "~/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    resort.imageFile.SaveAs(filename);

                    count++;
                    resort.reso_id = "reso_" + count;

                    db.resort.Add(resort);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            

            return View(resort);
        }

        // GET: resorts/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Resort";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resort resort = db.resort.Find(id);
            if (resort == null)
            {
                return HttpNotFound();
            }
            return View(resort);
        }

        // POST: resorts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reso_id,reso_name,reso_country,reso_charges,reso_roomAvailable,reso_rating,reso_description,reso_img")] resort resort)
        {
            if (ModelState.IsValid)
            {
                string  filename = Path.GetFileNameWithoutExtension(resort.imageFile.FileName),
                        extension = Path.GetExtension(resort.imageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                resort.reso_img = "~/Image/" + filename;
                filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                resort.imageFile.SaveAs(filename);

                db.Entry(resort).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resort);
        }

        // GET: resorts/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Resort";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resort resort = db.resort.Find(id);
            if (resort == null)
            {
                return HttpNotFound();
            }
            return View(resort);
        }

        // POST: resorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            resort resort = db.resort.Find(id);
            db.resort.Remove(resort);
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
