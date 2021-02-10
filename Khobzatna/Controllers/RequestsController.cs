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
    [HasAccount(HasAccount.ClientType.Beneficiary)]
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        public ActionResult Index()
        {
            List<Request> requests = new List<Request>();
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                requests = db.Requests.Include(r => r.Beneficiary).Include(r => r.RequestType).ToList();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var benId = db.Beneficiaries.Where(x => x.UserId == userId).FirstOrDefault().BeneficiaryId;
                requests = db.Requests.Where(x=>x.BeneficiaryId == benId).Include(r => r.Beneficiary).Include(r => r.RequestType).ToList();
            }
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.RequestTypeId = new SelectList(db.RequestTypes, "RequestTypeId", "Name");
            if (User.IsInRole("Admin"))
            {
                ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name");
            }
            else
            {
                var userId = User.Identity.GetUserId();
                ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries.Where(x => x.UserId == userId), "BeneficiaryId", "Name");
            }
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestId,Note,Date,Status,BeneficiaryId,RequestTypeId")] Request request)
        {
            if (ModelState.IsValid)
            {
                request.Status = RequestStatus.Waiting;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (User.IsInRole("Admin"))
            {
                ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name");
            }
            else
            {
                var userId = User.Identity.GetUserId();
                ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries.Where(x => x.UserId == userId), "BeneficiaryId", "Name");
            }
            ViewBag.RequestTypeId = new SelectList(db.RequestTypes, "RequestTypeId", "Name", request.RequestTypeId);
            return View(request);
        }

        // GET: Requests/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name", request.BeneficiaryId);
            ViewBag.RequestTypeId = new SelectList(db.RequestTypes, "RequestTypeId", "Name", request.RequestTypeId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,Note,Date,Status,BeneficiaryId,RequestTypeId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name", request.BeneficiaryId);
            ViewBag.RequestTypeId = new SelectList(db.RequestTypes, "RequestTypeId", "Name", request.RequestTypeId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
