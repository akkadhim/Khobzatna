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
    public class BeneficiariesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Beneficiaries
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Beneficiaries.ToList());
        }

        // GET: Beneficiaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // GET: Beneficiaries/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var hasAccount = db.Beneficiaries.Any(x => x.UserId == userId);
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

        // POST: Beneficiaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BeneficiaryId,Name,Address,GPS,FamilyNo,age,Note")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var hasAccount = db.Beneficiaries.Any(x => x.UserId == userId);
                if (hasAccount)
                {
                    return RedirectToAction("Index", "Home", null);
                }
                beneficiary.UserId = User.Identity.GetUserId();
                db.Beneficiaries.Add(beneficiary);
                db.SaveChanges();

                MessageHelper message = new MessageHelper()
                {
                    Message = "تم انشاء الحساب يمكنك الان انشاء طلب",
                    MessageLink = "/Requests/Index",
                    MessageLinkText = "أضغط هنا",
                    MessageType = MessageType.success
                };
                TempData["MessageHelper"] = message;
                return RedirectToAction("Index","Home",null);
            }

            return View(beneficiary);
        }

        // GET: Beneficiaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // POST: Beneficiaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BeneficiaryId,Name,Address,GPS,FamilyNo,age,Note")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                var ben = db.Beneficiaries.Find(beneficiary.BeneficiaryId);
                ben.Name = beneficiary.Name;
                ben.Note = beneficiary.Note;
                ben.Address = beneficiary.Address;
                ben.GPS = beneficiary.GPS;
                ben.age = beneficiary.age;
                ben.FamilyNo = beneficiary.FamilyNo;
                db.Entry(ben).State = EntityState.Modified;
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
            return View(beneficiary);
        }

        // GET: Beneficiaries/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // POST: Beneficiaries/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            db.Beneficiaries.Remove(beneficiary);
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
