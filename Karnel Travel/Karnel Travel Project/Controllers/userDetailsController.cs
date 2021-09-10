using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Karnel_Travel_Project;

namespace Karnel_Travel_Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class userDetailsController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: userDetails
        public ActionResult Index()
        {
            ViewBag.Title = "User";
            return View(db.userDetail.ToList());
        }

        // GET: userDetails/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "User";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetail userDetail = db.userDetail.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: userDetails/Create
        public ActionResult Create()
        {
            ViewBag.Title = "User";
            return View();
        }

        // POST: userDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "u_id,u_password,u_role")] userDetail userDetail)
        {
            bool matched = db.userDetail.Any(x => x.u_id == userDetail.u_id);
            if (!matched)
            {
                if (ModelState.IsValid)
                {
                    db.userDetail.Add(userDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Use a Different User Id");
                return RedirectToAction("Create");
            }

            return View(userDetail);
        }

        // GET: userDetails/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "User";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetail userDetail = db.userDetail.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: userDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "u_id,u_password,u_role")] userDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        // GET: userDetails/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "User";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetail userDetail = db.userDetail.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: userDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            userDetail userDetail = db.userDetail.Find(id);
            db.userDetail.Remove(userDetail);
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
