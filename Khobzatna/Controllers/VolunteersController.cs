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
    public class VolunteersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Volunteers
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Volunteers.ToList());
        }

        // GET: Volunteers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // GET: Volunteers/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var hasAccount = db.Volunteers.Any(x => x.UserId == userId);
            if (hasAccount)
            {
                MessageHelper message = new MessageHelper()
                {
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

        // POST: Volunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VolunteerId,Name,Address,Job,FreeTime,Note")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var hasAccount = db.Volunteers.Any(x => x.UserId == userId);
                if (hasAccount)
                {
                    return RedirectToAction("Index", "Home", null);
                }
                volunteer.UserId = userId;
                db.Volunteers.Add(volunteer);
                db.SaveChanges();

                MessageHelper message = new MessageHelper()
                {
                    Message = "تم انشاء الحساب يمكنك الان البدء بالاعمال التطوعية",
                    MessageLink = "/ToDoTasks/Index",
                    MessageLinkText = "أضغط هنا",
                    MessageType = MessageType.success
                };
                TempData["MessageHelper"] = message;
                return RedirectToAction("Index", "Home", null);
            }

            return View(volunteer);
        }

        // GET: Volunteers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                var vol = db.Volunteers.Find(volunteer.VolunteerId);
                vol.Address = volunteer.Address;
                vol.FreeTime = volunteer.FreeTime;
                vol.Job = volunteer.Job;
                vol.Name = volunteer.Name;
                db.Entry(vol).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home", null);
                }
            }
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);
            db.Volunteers.Remove(volunteer);
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
