using Khobzatna.Models;
using System;
using System.Data.Entity;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Khobzatna.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersRolesManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserRoles
        public ActionResult Index()
        {
            List<ApplicationUser> users = db.Users.Include(x=>x.Roles).ToList();
            return View(users);
        }

        public ActionResult RolesAssignment(string UserId)
        {
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(UserId);
            if (user == null)
            {
                return HttpNotFound();
            }
            List<string> roles = new List<string>();
            foreach (var role in user.Roles)
            {
                roles.Add(db.Roles.Find(role.RoleId).Name);
            }
            ViewBag.Roles = roles;
            ViewBag.AllRoles = new SelectList(db.Roles.ToList(), "Id", "Name", null);
            return View(user);
        }

        [HttpPost]
        public ActionResult RolesAssignment(string UserId, string AddedRole)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(UserId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var roleName = db.Roles.Find(AddedRole).Name;
            if (!string.IsNullOrEmpty(roleName))
            {
                var result1 = UserManager.AddToRole(user.Id, roleName);
            }

            return RedirectToAction("RolesAssignment", new { UserId = UserId });
        }

        [HttpPost]
        public ActionResult DeleteRole(string UserId, string RoleName)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(UserId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var roleName = db.Roles.Where(x=>x.Name == RoleName).FirstOrDefault().Id;
            if (!string.IsNullOrEmpty(roleName))
            {
                var result1 = UserManager.RemoveFromRole(user.Id, RoleName);
            }

            return RedirectToAction("RolesAssignment", new { UserId = UserId });
        }
    }
}