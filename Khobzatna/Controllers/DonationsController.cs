using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Khobzatna.Helper;
using Khobzatna.Models;
using Microsoft.AspNet.Identity;

namespace Khobzatna.Controllers
{
    [Authorize]
    [HasAccount(HasAccount.ClientType.Donor)]
    public class DonationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donations
        public ActionResult Index()
        {
            List<Donation> donations = new List<Donation>();
            if (User.IsInRole("Admin"))
            {
                donations = db.Donations.Include(d => d.DonationType).Include(d => d.Donor).ToList();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var donId = db.Donors.Where(x => x.UserId == userId).FirstOrDefault().DonorId;
                donations = db.Donations.Where(x => x.DonorId == donId).Include(d => d.DonationType).Include(d => d.Donor).ToList();
            }
            return View(donations.ToList());
        }

        // GET: Donations/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Donations/Create
        public ActionResult Create(long? JobId, bool? IsIdentity)
        {
            if (JobId != null)
            {
                var job = db.Jobs.Find(JobId);
                if (job == null)
                {
                    return HttpNotFound();
                }
                ViewBag.JobId = JobId;
            }

            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "Name");
            if (IsIdentity != null && (bool)IsIdentity)
            {
                var userId = User.Identity.GetUserId();
                ViewBag.DonorId = new SelectList(db.Donors.Where(x => x.UserId == userId), "DonorId", "Name");
            }
            else
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name");
                }
                else
                {
                    var userId = User.Identity.GetUserId();
                    ViewBag.DonorId = new SelectList(db.Donors.Where(x=>x.UserId == userId), "DonorId", "Name");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonationId,Note,Date,Status,DonorId,DonationTypeId,Payment")] Donation donation,long? JobId)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(donation);
                db.SaveChanges();

                if (JobId != null)
                {
                    var job = db.Jobs.Find(JobId);
                    if (job == null)
                    {
                        return HttpNotFound();
                    }
                    job.Donation = donation;
                    job.Donor = donation.Donor;
                    db.Entry(job).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //var userId = User.Identity.GetUserId();
                //Account acnt = db.Accounts.Where(x => x.UserId == userId).FirstOrDefault();
                
                return RedirectToAction("Index");
            }

            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "Name");
            if (User.IsInRole("Admin"))
            {
                ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name");
            }
            else
            {
                var userId = User.Identity.GetUserId();
                ViewBag.DonorId = new SelectList(db.Donors.Where(x => x.UserId == userId), "DonorId", "Name");
            }
            return View(donation);
        }

        // GET: Donations/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "Name", donation.DonationTypeId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name", donation.DonorId);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationId,Note,Date,Status,DonorId,DonationTypeId,Payment")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "Name", donation.DonationTypeId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name", donation.DonorId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
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
