using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Karnel_Travel_Project.Controllers
{
    
    public class hotelsController : Controller
    {
        private karnelEntities1 db = new karnelEntities1();

        // GET: hotels
        public ActionResult Index()
        {
            ViewBag.Title = "Hotel";
            return View(db.hotel.ToList());
        }

        // GET: hotels/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Hotel";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel hotel = db.hotel.Find(id);

            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: hotels/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Hotel";
            return View();
        }

        // POST: hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hot_id,hot_name,hot_country,hot_charges,hot_roomAvailable,hot_rating,hot_description,hot_img,imageFile")] hotel hotel)
        {
            var count = db.hotel.Count();
            try
            {
                if (ModelState.IsValid)
                {
                    string filename = Path.GetFileNameWithoutExtension(hotel.imageFile.FileName),
                            extension = Path.GetExtension(hotel.imageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    hotel.hot_img = "~/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    hotel.imageFile.SaveAs(filename);

                    count++;
                    hotel.hot_id = "hot_" + count;

                    db.hotel.Add(hotel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }            

            return View(hotel);
        }

        // GET: hotels/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Hotel";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel hotel = db.hotel.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hot_id,hot_name,hot_country,hot_charges,hot_roomAvailable,hot_rating,hot_description,hot_img,imageFile")] hotel hotel)
        {
            if (ModelState.IsValid)
            {
                string  filename = Path.GetFileNameWithoutExtension(hotel.imageFile.FileName),
                        extension = Path.GetExtension(hotel.imageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                hotel.hot_img = "~/Image/" + filename;
                filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                hotel.imageFile.SaveAs(filename);

                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: hotels/Delete/5
        public ActionResult Delete(string id)
        {
            ViewBag.Title = "Hotel";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotel hotel = db.hotel.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            hotel hotel = db.hotel.Find(id);
            db.hotel.Remove(hotel);
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
