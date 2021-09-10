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
    
    public class bookingHotsController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: bookingHots
        public ActionResult Index()
        {
            ViewBag.Title = "Hotel Booking";
            var bookingHot = db.bookingHot.Include(b => b.userDetail).Include(b => b.hotel);
            return View(bookingHot.ToList());
        }

        // GET: bookingHots/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Hotel Booking";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingHot bookingHot = db.bookingHot.Find(id);
            if (bookingHot == null)
            {
                return HttpNotFound();
            }
            return View(bookingHot);
        }

        // GET: bookingHots/Create
        public ActionResult Create()
        {
            ViewBag.bhot_cust_id = new SelectList(db.customer, "cust_id", "cust_name");
            ViewBag.bhot_name = new SelectList(db.hotel, "hot_id", "hot_name");
            return View();
        }

        // POST: bookingHots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bhot_id,bhot_cust_id,bhot_name,bhot_departure,bhot_arrival,bhot_guests,bhot_rooms,bhot_contactNo")] bookingHot bookingHot)
        {
            if (ModelState.IsValid)
            {
                db.bookingHot.Add(bookingHot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bhot_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingHot.bhot_cust_id);
            ViewBag.bhot_name = new SelectList(db.hotel, "hot_id", "hot_name", bookingHot.bhot_name);
            return View(bookingHot);
        }

        // GET: bookingHots/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingHot bookingHot = db.bookingHot.Find(id);
            if (bookingHot == null)
            {
                return HttpNotFound();
            }
            ViewBag.bhot_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingHot.bhot_cust_id);
            ViewBag.bhot_name = new SelectList(db.hotel, "hot_id", "hot_name", bookingHot.bhot_name);
            return View(bookingHot);
        }

        // POST: bookingHots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bhot_id,bhot_cust_id,bhot_name,bhot_departure,bhot_arrival,bhot_guests,bhot_rooms,bhot_contactNo")] bookingHot bookingHot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingHot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bhot_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingHot.bhot_cust_id);
            ViewBag.bhot_name = new SelectList(db.hotel, "hot_id", "hot_name", bookingHot.bhot_name);
            return View(bookingHot);
        }

        // GET: bookingHots/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Hotel Booking";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingHot bookingHot = db.bookingHot.Find(id);
            if (bookingHot == null)
            {
                return HttpNotFound();
            }
            return View(bookingHot);
        }

        // POST: bookingHots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            bookingHot bookingHot = db.bookingHot.Find(id);
            db.bookingHot.Remove(bookingHot);
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
