using Khobzatna.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Khobzatna.Helper
{
    public class HasAccount: ActionFilterAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ClientType clientType;
        private string userId;

        public HasAccount(ClientType clientType)
        {
            this.clientType = clientType;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            userId = filterContext.HttpContext.User.Identity.GetUserId();

            try
            {
                var user = db.Users.Find(userId);
                bool isClient = false;
                switch (clientType)
                {
                    case ClientType.Donor:
                        isClient = db.Donors.Any(x => x.UserId == userId);
                        break;
                    case ClientType.Beneficiary:
                        isClient = db.Beneficiaries.Any(x => x.UserId == userId);
                        break;
                    case ClientType.Volunteer:
                        isClient = db.Volunteers.Any(x => x.UserId == userId);
                        break;
                    default:
                        isClient = false;
                        break;
                }
                if (isClient)
                {
                    // do nothing
                }
                else
                {
                    switch (clientType)
                    {
                        case ClientType.Donor:
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Donors" }, { "action", "Create" } });
                            break;
                        case ClientType.Beneficiary:
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Beneficiaries" }, { "action", "Create" } });
                            break;
                        case ClientType.Volunteer:
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Volunteers" }, { "action", "Create" } });
                            break;
                        default:
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                            break;
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }

        }

        public enum ClientType
        {
            Donor,
            Beneficiary,
            Volunteer
        }
    }
}