using Khobzatna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khobzatna.Helper
{
    public class UsersRolesManager
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string GetRoleName(string roleId)
        {
            string name = db.Roles.Find(roleId).Name;
            return name;
        }
    }
}