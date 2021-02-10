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
using Newtonsoft.Json.Linq;

namespace Khobzatna.Controllers
{
    [Authorize]
    [HasAccount(HasAccount.ClientType.Volunteer)]
    public class ToDoTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoTasks
        public ActionResult Index()
        {
            return View(db.ToDoTasks.ToList());
        }

        public ActionResult JobAssignment(long? TaskId)
        {
            if (TaskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask toDoTask = db.ToDoTasks.Include(x => x.Jobs).Where(x => x.ToDoTaskId == TaskId).FirstOrDefault();
            if (toDoTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.ToDoTask = toDoTask;
            ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "BeneficiaryId", "Name");
            ViewBag.DonationId = new SelectList(db.Donations, "DonationId", "Note");
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "Name");
            ViewBag.OperationId = new SelectList(db.Operations, "OperationId", "OperationId");
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Note");
            ViewBag.ToDoTaskId = new SelectList(db.ToDoTasks, "ToDoTaskId", "ToDoTaskId");
            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult JobAssignment(long? ToDoTaskId, Job job)
        {
            if (ToDoTaskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask toDoTask = db.ToDoTasks.Include(x => x.Jobs).Where(x => x.ToDoTaskId == ToDoTaskId).FirstOrDefault();
            if (toDoTask == null)
            {
                return HttpNotFound();
            }
            if (job.Date != null && !string.IsNullOrEmpty(job.Note))
            {
                job.ToDoTask = toDoTask;
                db.Jobs.Add(job);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = ToDoTaskId });
        }

        [HttpPost]
        public ActionResult JobRemove(long? jobId)
        {
            if (jobId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Where(x => x.JobId == jobId).FirstOrDefault();
            if (jobId == null)
            {
                return HttpNotFound();
            }
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = job.ToDoTaskId });
        }

        [HasAccount(HasAccount.ClientType.Volunteer)]
        public ActionResult VolunteerAssignment(long? JobId, bool IsIdentity)
        {
            if (JobId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Where(x => x.JobId == JobId).FirstOrDefault();
            if (job == null)
            {
                return HttpNotFound();
            }
            if (IsIdentity != true)
            {
                ViewBag.JobId = JobId;
                ViewBag.VolunteerId = new SelectList(db.Volunteers.ToList(), "VolunteerId", "Name");
                return View();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var vol = db.Volunteers.Where(x => x.UserId == userId).FirstOrDefault();
                if (vol == null)
                {
                    return HttpNotFound();
                }
                job.Volunteer = vol;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = job.ToDoTaskId });
        }

        [HasAccount(HasAccount.ClientType.Volunteer)]
        [HttpPost]
        public ActionResult VolunteerAssignment(long? JobId, int? VolunteerId)
        {
            if (JobId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Where(x => x.JobId == JobId).FirstOrDefault();
            if (job == null)
            {
                return HttpNotFound();
            }
            var vol = db.Volunteers.Find(VolunteerId);
            if (vol == null)
            {
                return HttpNotFound();
            }
            job.Volunteer = vol;
            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = job.ToDoTaskId });
        }

            // GET: ToDoTasks/Details/5
            public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask toDoTask = db.ToDoTasks.Include(x => x.Jobs).Where(x=>x.ToDoTaskId == id).FirstOrDefault();
            if (toDoTask == null)
            {
                return HttpNotFound();
            }
            List<MapInfo> mapInfo = new List<MapInfo>();
            foreach (var item in toDoTask.Jobs)
            {
                if (item.Beneficiary != null)
                {
                    if (!string.IsNullOrEmpty(item.Beneficiary.GPS))
                    {
                        string[] Postions = item.Beneficiary.GPS.Split(',');
                        var point = new MapInfo()
                        {
                            Lat = Convert.ToDouble(Postions[0]),
                            Lng = Convert.ToDouble(Postions[1]),
                            Color = "red",
                            Title = item.Beneficiary.Name,
                        };
                        mapInfo.Add(point);
                    }
                }
                if (item.Donor != null)
                {
                    if (!string.IsNullOrEmpty(item.Donor.Address))
                    {
                        string[] Postions = item.Donor.Address.Split(',');
                        var point = new MapInfo()
                        {
                            Lat = Convert.ToDouble(Postions[0]),
                            Lng = Convert.ToDouble(Postions[1]),
                            Color = "green",
                            Title = item.Donor.Name,
                        };
                        mapInfo.Add(point);
                    }
                }
            }
            ViewBag.MapInfo = mapInfo;
            return View(toDoTask);
        }

        // GET: ToDoTasks/Create
        public ActionResult Create(int? RequestId)
        {
            if (RequestId != null)
            {
                var request = db.Requests.Find(RequestId);
                if (request == null)
                {
                    return HttpNotFound();
                }
                ToDoTask toDoTask = new ToDoTask()
                {
                    Note = request.Note,
                };
                ViewBag.RequestId = RequestId;
                return View(toDoTask);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToDoTaskId,Note,Status,Date")] ToDoTask toDoTask, int? RequestId)
        {
            if (ModelState.IsValid)
            {
                db.ToDoTasks.Add(toDoTask);
                db.SaveChanges();
                if (RequestId != null)
                {
                    var request = db.Requests.Find(RequestId);
                    if (request != null)
                    {
                        Job job = new Job()
                        {
                            Date = request.Date,
                            Beneficiary = request.Beneficiary,
                            Note = request.Note,
                            Request = request,
                            ToDoTask = toDoTask,
                            Status = JobStatus.Started,
                        };
                        db.Jobs.Add(job);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(toDoTask);
        }

        // GET: ToDoTasks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask toDoTask = db.ToDoTasks.Find(id);
            if (toDoTask == null)
            {
                return HttpNotFound();
            }
            return View(toDoTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToDoTaskId,Note,Status,Date")] ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoTask);
        }

        // GET: ToDoTasks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoTask toDoTask = db.ToDoTasks.Find(id);
            if (toDoTask == null)
            {
                return HttpNotFound();
            }
            return View(toDoTask);
        }

        // POST: ToDoTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ToDoTask toDoTask = db.ToDoTasks.Find(id);
            db.ToDoTasks.Remove(toDoTask);
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
