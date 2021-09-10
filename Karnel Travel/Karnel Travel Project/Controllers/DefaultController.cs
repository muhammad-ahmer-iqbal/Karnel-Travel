using Karnel_Travel_Project.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


namespace Karnel_Travel_Project.Controllers
{
    public class DefaultController : Controller
    {
        static int count;
        private karnelEntities1 db = new karnelEntities1();
        // GET: Default
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Transport()
        {
            ViewBag.Title = "Transport";
            return View(db.tranport.ToList());
        }
        [AllowAnonymous]
        public ActionResult Restaurant()
        {
            ViewBag.Title = "Restaurant";
            return View(db.restaurant.ToList());
        }
        [AllowAnonymous]
        public ActionResult Resort()
        {
            ViewBag.Title = "Resort";
            return View(db.resort.ToList());
        }
        [AllowAnonymous]
        public ActionResult Hotel()
        {
            ViewBag.Title = "Hotel";
            return View(db.hotel.ToList());
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "cont_id,cont_name,cont_email,cont_formType,cont_dateTime,cont_message")] contact contact)
        {
            try
            {
                count = db.contact.Count();
                if (ModelState.IsValid)
                {
                    count++;
                    contact.cont_id = "cont_" + count;
                    contact.cont_dateTime = DateTime.Now;
                    db.contact.Add(contact);
                    db.SaveChanges();
                    return RedirectToAction("Contact");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(contact);
        }
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "u_id,u_password")] logIn model)
        {
            bool isValid = db.userDetail.Any(x => x.u_id == model.u_id && x.u_password == model.u_password);

            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(model.u_id, false);
                return RedirectToAction("Index", "Default");
            }
            else
            {
                ModelState.AddModelError("", "Invalid User ID and Password");
            }

            return View();
        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            ViewBag.Title = "Sign Up";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "cust_id,cust_u_id,cust_u_password,cust_name,cust_contactNo,cust_email,cust_nic")] customer customer)
        {
            count = db.customer.Count();
            bool matched = db.userDetail.Any(x => x.u_id == customer.cust_u_id);
            if (!matched)
            {
                if (ModelState.IsValid)
                {
                    count++;
                    customer.cust_id = "cust_" + count;
                    db.customer.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("SignIn");
                }
            }
            else
            {
                count++;
                ViewBag.uId = "Sugested: " + "user_" + count;
                ModelState.AddModelError("", "Use a Different User Id");
                return RedirectToAction("SignUp");
            }
            return View(customer);
        }
        [Authorize(Roles = "Customer, Admin")]
        public ActionResult BookingTran()
        {
            ViewBag.Title = "Transport Booking";
            ViewBag.btran_cust_id = new SelectList(db.customer, "cust_id", "cust_name");
            ViewBag.btran_name = new SelectList(db.tranport, "tran_id", "tran_name");
            return View();
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingTran([Bind(Include = "btran_id,btran_cust_id,btran_name,btran_guests,btran_seats,btran_contactNo")] bookingTran bookingTran)
        {
            count = db.bookingTran.Count();
            try
            {
                if (ModelState.IsValid)
                {
                    bookingTran.btran_cust_id = User.Identity.Name;
                    count++;
                    bookingTran.btran_id = "btran_" + count;
                    db.bookingTran.Add(bookingTran);
                    db.SaveChanges();
                    return RedirectToAction("Transport");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            ViewBag.btran_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingTran.btran_cust_id);
            ViewBag.btran_name = new SelectList(db.tranport, "tran_id", "tran_name", bookingTran.btran_name);
            return View(bookingTran);
        }
        [Authorize(Roles = "Customer, Admin")]
        public ActionResult BookingReso()
        {
            ViewBag.Title = "Resort Booking";
            ViewBag.breso_cust_id = new SelectList(db.customer, "cust_id", "cust_name");
            ViewBag.breso_name = new SelectList(db.resort, "reso_id", "reso_name");
            return View();
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingReso([Bind(Include = "breso_id,breso_cust_id,breso_name,breso_departure,breso_arrival,breso_guests,breso_rooms,breso_contactNo")] bookingReso bookingReso)
        {
            count = db.bookingReso.Count();
            try
            {
                if (ModelState.IsValid)
                {
                    bookingReso.breso_cust_id = User.Identity.Name;
                    count++;
                    bookingReso.breso_id = "breso_" + count;
                    db.bookingReso.Add(bookingReso);
                    db.SaveChanges();
                    return RedirectToAction("Resort");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            ViewBag.breso_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingReso.breso_cust_id);
            ViewBag.breso_name = new SelectList(db.resort, "reso_id", "reso_name", bookingReso.breso_name);
            return View(bookingReso);
        }
        [Authorize(Roles = "Customer, Admin")]
        public ActionResult BookingHot()
        {
            ViewBag.Title = "Hotel Booking";
            ViewBag.bhot_cust_id = new SelectList(db.customer, "cust_id", "cust_name");
            ViewBag.bhot_name = new SelectList(db.hotel, "hot_id", "hot_name");
            return View();
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingHot([Bind(Include = "bhot_id,bhot_cust_id,bhot_name,bhot_departure,bhot_arrival,bhot_guests,bhot_rooms,bhot_contactNo")] bookingHot bookingHot)
        {
            count = db.bookingHot.Count();
            try
            {
                if (ModelState.IsValid)
                {
                    bookingHot.bhot_cust_id = User.Identity.Name;
                    count++;
                    bookingHot.bhot_id = "bhot_" + count;
                    db.bookingHot.Add(bookingHot);
                    db.SaveChanges();
                    return RedirectToAction("Hotel");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            ViewBag.bhot_cust_id = new SelectList(db.customer, "cust_id", "cust_name", bookingHot.bhot_cust_id);
            ViewBag.bhot_name = new SelectList(db.hotel, "hot_id", "hot_name", bookingHot.bhot_name);
            return View(bookingHot);
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }
    }
}