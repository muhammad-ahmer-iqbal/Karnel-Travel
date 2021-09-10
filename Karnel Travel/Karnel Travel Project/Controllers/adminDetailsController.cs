using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Karnel_Travel_Project;

namespace Karnel_Travel_Project.Content
{
    public class adminDetailsController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: adminDetails
        public ActionResult Index()
        {
            return View(db.adminDetail.ToList());
        }

        // GET: adminDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adminDetail adminDetail = db.adminDetail.Find(id);
            if (adminDetail == null)
            {
                return HttpNotFound();
            }
            return View(adminDetail);
        }

        // GET: adminDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adminDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ad_id,ad_password")] adminDetail adminDetail)
        {
            if (ModelState.IsValid)
            {
                db.adminDetail.Add(adminDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminDetail);
        }

        // GET: adminDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adminDetail adminDetail = db.adminDetail.Find(id);
            if (adminDetail == null)
            {
                return HttpNotFound();
            }
            return View(adminDetail);
        }

        // POST: adminDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ad_id,ad_password")] adminDetail adminDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminDetail);
        }

        // GET: adminDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adminDetail adminDetail = db.adminDetail.Find(id);
            if (adminDetail == null)
            {
                return HttpNotFound();
            }
            return View(adminDetail);
        }

        // POST: adminDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            adminDetail adminDetail = db.adminDetail.Find(id);
            db.adminDetail.Remove(adminDetail);
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
