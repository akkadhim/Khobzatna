using System;
using System.IO;
using System.Linq;
using Khobzatna.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Khobzatna.Startup))]
namespace Khobzatna
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                PrepareRoles(db);
                //PrepareAccounts(db);
                //PrepareOperationType(db);
            }
        }

        private static void PrepareRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // creating Creating Manager role     
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "admin@admin.com";
                user.EmailConfirmed = true;
                string userPWD = "@Aa123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }
        }

        private void PrepareAccounts(ApplicationDbContext db)
        {
            if (db.Accounts.Count() < 1)
            {
                var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"AccountsAll_New.sql");
                var sql = File.ReadAllText(sqlFile);
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();

                var accounts = db.Accounts.ToList();
                foreach (var acnt in accounts)
                {
                    if (acnt.ParentId == null)
                    {
                        string no = acnt.AccountNo;
                        if (no.Length > 0)
                        {
                            if (no.Length == 1)
                            {
                                //root
                                acnt.Level = 1;
                                acnt.FriendlyName = acnt.Name;
                                db.SaveChanges();
                            }
                            else if (IsDigitsOnly(no))
                            {
                                string parentID = no.Substring(0, no.Length - 1);
                                acnt.ParentId = accounts.Where(x => x.AccountNo == parentID).FirstOrDefault().AccountId;
                                acnt.Level = no.Length;
                                acnt.FriendlyName = acnt.Name;
                                db.SaveChanges();
                            }
                        }
                    }
                }

                if (accounts.Where(x => x.Name == "المتبرعين").Count() < 1)
                {
                    Account customer = new Account()
                    {
                        AccountNo = "178",
                        Name = "المتبرعين",
                        FriendlyName = "المتبرعين",
                        Level = 3,
                        ParentId = accounts.Where(x => x.AccountNo == "17").FirstOrDefault().AccountId,
                    };
                    db.Accounts.Add(customer);
                }

                if (accounts.Where(x => x.Name == "الموردون").Count() < 1)
                {
                    Account supp = new Account()
                    {
                        AccountNo = "281",
                        Name = "الموردون",
                        FriendlyName = "الموردون",
                        Level = 3,
                        ParentId = accounts.Where(x => x.AccountNo == "28").FirstOrDefault().AccountId,
                    };
                    db.Accounts.Add(supp);
                }

                if (accounts.Where(x => x.Name == "الحوالات").Count() < 1)
                {
                    Account trns = new Account()
                    {
                        AccountNo = "289",
                        Name = "الحوالات",
                        FriendlyName = "الحوالات",
                        Level = 3,
                        ParentId = accounts.Where(x => x.AccountNo == "28").FirstOrDefault().AccountId,
                    };
                    db.Accounts.Add(trns);
                }
                db.SaveChanges();

            }
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;
        }

        private void PrepareOperationType(ApplicationDbContext db)
        {
            if (!db.OperationTypes.Any(x=>x.Name == "تبرع"))
            {
                OperationType op = new OperationType()
                {
                    Name = "",
                    IsInMenu = true,
                };
                db.OperationTypes.Add(op);
                db.SaveChanges();

                OperationAction opActnIn = new OperationAction()
                {
                    Account = db.Accounts.Where(x=>x.Name == "المتبرعين").FirstOrDefault(),
                    ActionType = ActionType.Credit,
                    Direction = Direction.Input,
                    IsFinalNode = false,
                    IsVisible = true,
                    OperationType = op,
                };
                db.OperationActions.Add(opActnIn);
                db.SaveChanges();

                 OperationAction opActnOut = new OperationAction()
                {
                    Account = db.Accounts.Where(x => x.Name == "نقدية بالصندوق").FirstOrDefault(),
                    ActionType = ActionType.Debit,
                    Direction = Direction.Output,
                    IsFinalNode = true,
                    IsVisible = true,
                    OperationType = op,
                };
                db.OperationActions.Add(opActnOut);
                db.SaveChanges();
            }
        }
    }
}
