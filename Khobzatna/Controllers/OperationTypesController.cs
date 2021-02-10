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
    public class OperationTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OperationTypes
        public ActionResult Index()
        {
            return View(db.OperationTypes.ToList());
        }

        // GET: OperationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationType operationType = db.OperationTypes.Find(id);
            if (operationType == null)
            {
                return HttpNotFound();
            }
            return View(operationType);
        }

        // GET: OperationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OperationTypeId,Name,IsInMenu")] OperationType operationType)
        {
            if (ModelState.IsValid)
            {
                db.OperationTypes.Add(operationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(operationType);
        }

        // GET: OperationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationType operationType = db.OperationTypes.Find(id);
            if (operationType == null)
            {
                return HttpNotFound();
            }
            return View(operationType);
        }

        // POST: OperationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OperationTypeId,Name,IsInMenu")] OperationType operationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(operationType);
        }

        // GET: OperationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationType operationType = db.OperationTypes.Find(id);
            if (operationType == null)
            {
                return HttpNotFound();
            }
            return View(operationType);
        }

        // POST: OperationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OperationType operationType = db.OperationTypes.Find(id);
            db.OperationTypes.Remove(operationType);
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
