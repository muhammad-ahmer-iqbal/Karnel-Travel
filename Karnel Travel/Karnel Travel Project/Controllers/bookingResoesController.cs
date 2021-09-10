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
    
    public class bookingResoesController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: bookingResoes
        public ActionResult Index()
        {
            ViewBag.Title = "Resort Booking";
            var bookingReso = db.bookingReso.Include(b => b.userDetail).Include(b => b.resort);
            return View(bookingReso.ToList());
        }

        // GET: bookingResoes/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Resort Booking";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingReso bookingReso = db.bookingReso.Find(id);
            if (bookingReso == null)
            {
                return HttpNotFound();
            }
            return View(bookingReso);
        }

        // GET: bookingResoes/Create
        public ActionResult Create()
        {
            ViewBag.breso_cust_id = new SelectList(db.customer, "cust_id", "cust_name");
            ViewBag.breso_name = new SelectList(db.resort, "reso_id", "reso_name");
            return View();
        }

        // POST: bookingResoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "breso_id,breso_cust_id,breso_name,breso_departure,breso_arrival,breso_guests,breso_rooms,breso_contactNo")] bookingReso bookingReso)
        {
            if (ModelState.IsValid)
            {
                db.bookingReso.Add(bookingReso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.breso_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingReso.breso_cust_id);
            ViewBag.breso_name = new SelectList(db.resort, "reso_id", "reso_name", bookingReso.breso_name);
            return View(bookingReso);
        }

        // GET: bookingResoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingReso bookingReso = db.bookingReso.Find(id);
            if (bookingReso == null)
            {
                return HttpNotFound();
            }
            ViewBag.breso_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingReso.breso_cust_id);
            ViewBag.breso_name = new SelectList(db.resort, "reso_id", "reso_name", bookingReso.breso_name);
            return View(bookingReso);
        }

        // POST: bookingResoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "breso_id,breso_cust_id,breso_name,breso_departure,breso_arrival,breso_guests,breso_rooms,breso_contactNo")] bookingReso bookingReso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingReso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.breso_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingReso.breso_cust_id);
            ViewBag.breso_name = new SelectList(db.resort, "reso_id", "reso_name", bookingReso.breso_name);
            return View(bookingReso);
        }

        // GET: bookingResoes/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Resort Booking";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingReso bookingReso = db.bookingReso.Find(id);
            if (bookingReso == null)
            {
                return HttpNotFound();
            }
            return View(bookingReso);
        }

        // POST: bookingResoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            bookingReso bookingReso = db.bookingReso.Find(id);
            db.bookingReso.Remove(bookingReso);
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
