using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Khobzatna.Models;

namespace Khobzatna.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class DonationTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DonationTypes
        public ActionResult Index()
        {
            return View(db.DonationTypes.ToList());
        }

        // GET: DonationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationType donationType = db.DonationTypes.Find(id);
            if (donationType == null)
            {
                return HttpNotFound();
            }
            return View(donationType);
        }

        // GET: DonationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonationTypeId,Name,Note,IsMoney")] DonationType donationType)
        {
            if (ModelState.IsValid)
            {
                db.DonationTypes.Add(donationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donationType);
        }

        // GET: DonationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationType donationType = db.DonationTypes.Find(id);
            if (donationType == null)
            {
                return HttpNotFound();
            }
            return View(donationType);
        }

        // POST: DonationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationTypeId,Name,Note,IsMoney")] DonationType donationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donationType);
        }

        // GET: DonationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationType donationType = db.DonationTypes.Find(id);
            if (donationType == null)
            {
                return HttpNotFound();
            }
            return View(donationType);
        }

        // POST: DonationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonationType donationType = db.DonationTypes.Find(id);
            db.DonationTypes.Remove(donationType);
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
