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
    
    public class tranportsController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: tranports
        public ActionResult Index()
        {
            ViewBag.Title = "Tranport";
            return View(db.tranport.ToList());
        }

        // GET: tranports/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Tranport";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranport tranport = db.tranport.Find(id);
            if (tranport == null)
            {
                return HttpNotFound();
            }
            return View(tranport);
        }

        // GET: tranports/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Tranport";
            return View();
        }

        // POST: tranports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tran_id,tran_name,tran_country,tran_charges,tran_seatAvailable,tran_description,tran_img,imageFile")] tranport tranport)
        {
            var count = db.tranport.Count();
            try
            {
                if (ModelState.IsValid)
                {
                    string filename = Path.GetFileNameWithoutExtension(tranport.imageFile.FileName),
                            extension = Path.GetExtension(tranport.imageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    tranport.tran_img = "~/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    tranport.imageFile.SaveAs(filename);

                    count++;
                    tranport.tran_id = "tran_" + count;

                    db.tranport.Add(tranport);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            

            return View(tranport);
        }

        // GET: tranports/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Tranport";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranport tranport = db.tranport.Find(id);
            if (tranport == null)
            {
                return HttpNotFound();
            }
            return View(tranport);
        }

        // POST: tranports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tran_id,tran_name,tran_country,tran_charges,tran_seatAvailable,tran_description,tran_img,imageFile")] tranport tranport)
        {
            if (ModelState.IsValid)
            {
                string  filename = Path.GetFileNameWithoutExtension(tranport.imageFile.FileName),
                        extension = Path.GetExtension(tranport.imageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                tranport.tran_img = "~/Image/" + filename;
                filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                tranport.imageFile.SaveAs(filename);

                db.Entry(tranport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tranport);
        }

        // GET: tranports/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Tranport";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tranport tranport = db.tranport.Find(id);
            if (tranport == null)
            {
                return HttpNotFound();
            }
            return View(tranport);
        }

        // POST: tranports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tranport tranport = db.tranport.Find(id);
            db.tranport.Remove(tranport);
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
