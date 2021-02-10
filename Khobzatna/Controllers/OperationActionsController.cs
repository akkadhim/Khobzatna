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
    public class OperationActionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OperationActions
        public ActionResult Index()
        {
            var operationActions = db.OperationActions.Include(o => o.Account).Include(o => o.OperationType);
            return View(operationActions.ToList());
        }

        // GET: OperationActions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationAction operationAction = db.OperationActions.Find(id);
            if (operationAction == null)
            {
                return HttpNotFound();
            }
            return View(operationAction);
        }

        // GET: OperationActions/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "AccountNo");
            ViewBag.OperationTypeId = new SelectList(db.OperationTypes, "OperationTypeId", "Name");
            return View();
        }

        // POST: OperationActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OperationActionId,OperationTypeId,AccountId,Direction,ActionType,IsFinalNode,IsVisible,HasVehicle,HasContainer")] OperationAction operationAction)
        {
            if (ModelState.IsValid)
            {
                db.OperationActions.Add(operationAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "AccountNo", operationAction.AccountId);
            ViewBag.OperationTypeId = new SelectList(db.OperationTypes, "OperationTypeId", "Name", operationAction.OperationTypeId);
            return View(operationAction);
        }

        // GET: OperationActions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationAction operationAction = db.OperationActions.Find(id);
            if (operationAction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "AccountNo", operationAction.AccountId);
            ViewBag.OperationTypeId = new SelectList(db.OperationTypes, "OperationTypeId", "Name", operationAction.OperationTypeId);
            return View(operationAction);
        }

        // POST: OperationActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OperationActionId,OperationTypeId,AccountId,Direction,ActionType,IsFinalNode,IsVisible,HasVehicle,HasContainer")] OperationAction operationAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operationAction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "AccountNo", operationAction.AccountId);
            ViewBag.OperationTypeId = new SelectList(db.OperationTypes, "OperationTypeId", "Name", operationAction.OperationTypeId);
            return View(operationAction);
        }

        // GET: OperationActions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationAction operationAction = db.OperationActions.Find(id);
            if (operationAction == null)
            {
                return HttpNotFound();
            }
            return View(operationAction);
        }

        // POST: OperationActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OperationAction operationAction = db.OperationActions.Find(id);
            db.OperationActions.Remove(operationAction);
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
