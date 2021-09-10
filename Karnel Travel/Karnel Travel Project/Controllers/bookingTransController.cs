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
    
    public class bookingTransController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: bookingTrans
        public ActionResult Index()
        {
            ViewBag.Title = "Transport Booking";
            var bookingTran = db.bookingTran.Include(b => b.userDetail).Include(b => b.tranport);
            return View(bookingTran.ToList());
        }

        // GET: bookingTrans/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Transport Booking";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingTran bookingTran = db.bookingTran.Find(id);
            if (bookingTran == null)
            {
                return HttpNotFound();
            }
            return View(bookingTran);
        }

        // GET: bookingTrans/Create
        public ActionResult Create()
        {
            ViewBag.btran_cust_id = new SelectList(db.customer, "cust_id", "cust_name");
            ViewBag.btran_name = new SelectList(db.tranport, "tran_id", "tran_name");
            return View();
        }

        // POST: bookingTrans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "btran_id,btran_cust_id,btran_name,btran_departure,btran_arrival,btran_guests,btran_seats,btran_contactNo")] bookingTran bookingTran)
        {
            if (ModelState.IsValid)
            {
                db.bookingTran.Add(bookingTran);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.btran_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingTran.btran_cust_id);
            ViewBag.btran_name = new SelectList(db.tranport, "tran_id", "tran_name", bookingTran.btran_name);
            return View(bookingTran);
        }

        // GET: bookingTrans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingTran bookingTran = db.bookingTran.Find(id);
            if (bookingTran == null)
            {
                return HttpNotFound();
            }
            ViewBag.btran_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingTran.btran_cust_id);
            ViewBag.btran_name = new SelectList(db.tranport, "tran_id", "tran_name", bookingTran.btran_name);
            return View(bookingTran);
        }

        // POST: bookingTrans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "btran_id,btran_cust_id,btran_name,btran_departure,btran_arrival,btran_guests,btran_seats,btran_contactNo")] bookingTran bookingTran)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingTran).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.btran_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingTran.btran_cust_id);
            ViewBag.btran_name = new SelectList(db.tranport, "tran_id", "tran_name", bookingTran.btran_name);
            return View(bookingTran);
        }

        // GET: bookingTrans/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Transport Booking";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookingTran bookingTran = db.bookingTran.Find(id);
            if (bookingTran == null)
            {
                return HttpNotFound();
            }
            return View(bookingTran);
        }

        // POST: bookingTrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            bookingTran bookingTran = db.bookingTran.Find(id);
            db.bookingTran.Remove(bookingTran);
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
