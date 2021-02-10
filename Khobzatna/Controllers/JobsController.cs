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
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Beneficiary).Include(j => j.Donation).Include(j => j.Donor).Include(j => j.Operation).Include(j => j.Request).Include(j => j.ToDoTask).Include(j => j.Volunteer);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name");
            ViewBag.DonationId = new SelectList(db.Donations, "DonationId", "Note");
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name");
            ViewBag.OperationId = new SelectList(db.Operations, "OperationId", "OperationId");
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Note");
            ViewBag.ToDoTaskId = new SelectList(db.ToDoTasks, "ToDoTaskId", "ToDoTaskId");
            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "Name");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,Status,Date,Note,DonorId,VolunteerId,BeneficiaryId,ToDoTaskId,DonationId,RequestId,OperationId")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name", job.BeneficiaryId);
            ViewBag.DonationId = new SelectList(db.Donations, "DonationId", "Note", job.DonationId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name", job.DonorId);
            ViewBag.OperationId = new SelectList(db.Operations, "OperationId", "OperationId", job.OperationId);
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Note", job.RequestId);
            ViewBag.ToDoTaskId = new SelectList(db.ToDoTasks, "ToDoTaskId", "ToDoTaskId", job.ToDoTaskId);
            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "Name", job.VolunteerId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name", job.BeneficiaryId);
            ViewBag.DonationId = new SelectList(db.Donations, "DonationId", "Note", job.DonationId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name", job.DonorId);
            ViewBag.OperationId = new SelectList(db.Operations, "OperationId", "OperationId", job.OperationId);
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Note", job.RequestId);
            ViewBag.ToDoTaskId = new SelectList(db.ToDoTasks, "ToDoTaskId", "ToDoTaskId", job.ToDoTaskId);
            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "Name", job.VolunteerId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,Status,Date,Note,DonorId,VolunteerId,BeneficiaryId,ToDoTaskId,DonationId,RequestId,OperationId")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name", job.BeneficiaryId);
            ViewBag.DonationId = new SelectList(db.Donations, "DonationId", "Note", job.DonationId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name", job.DonorId);
            ViewBag.OperationId = new SelectList(db.Operations, "OperationId", "OperationId", job.OperationId);
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Note", job.RequestId);
            ViewBag.ToDoTaskId = new SelectList(db.ToDoTasks, "ToDoTaskId", "ToDoTaskId", job.ToDoTaskId);
            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "Name", job.VolunteerId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
