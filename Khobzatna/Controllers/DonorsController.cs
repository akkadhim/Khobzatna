using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Khobzatna.Models;
using Microsoft.AspNet.Identity;
using Khobzatna.Helper;

namespace Khobzatna.Controllers
{
    [Authorize]
    public class DonorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donors
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Donors.ToList());
        }

        // GET: Donors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // GET: Donors/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var hasAccount = db.Donors.Any(x => x.UserId == userId);
            if (hasAccount)
            {
                MessageHelper message = new MessageHelper() {
                    Message = "انت تملك حساب بالفعل للمزيد من التفاصيل",
                    MessageLink = "/Manage/Index",
                    MessageLinkText = "أضغط هنا",
                    MessageType = MessageType.success
                };
                TempData["MessageHelper"] = message;
                return RedirectToAction("Index", "Home", null);
            }
            return View();
        }

        // POST: Donors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonorId,Name,Address,DonationQty,Private,Job,Note")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var hasAccount = db.Donors.Any(x => x.UserId == userId);
                if (hasAccount)
                {
                    return RedirectToAction("Index", "Home", null);
                }
                donor.UserId = userId;
                db.Donors.Add(donor);
                db.SaveChanges();

                MessageHelper message = new MessageHelper()
                {
                    Message = "تم انشاء الحساب يمكنك الان التبرع",
                    MessageLink = "/Donations/Index",
                    MessageLinkText = "أضغط هنا",
                    MessageType = MessageType.success
                };
                TempData["MessageHelper"] = message;
                return RedirectToAction("Index", "Home", null);
            }

            return View(donor);
        }

        // GET: Donors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Donor donor)
        {
            if (ModelState.IsValid)
            {
                var don = db.Donors.Find(donor.DonorId);
                don.Name = donor.Name;
                don.Note = donor.Note;
                don.Private = donor.Private;
                don.Address = donor.Address;
                don.Job = donor.Job;
                db.Entry(don).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index","Home",null);
                }
            }
            return View(donor);
        }

        // GET: Donors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: Donors/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donor donor = db.Donors.Find(id);
            db.Donors.Remove(donor);
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
