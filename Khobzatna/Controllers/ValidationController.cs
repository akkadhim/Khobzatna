using Khobzatna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsDealer.Controllers
{
    [Authorize]
    public class ValidationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult IsVolunteerNameValid(string Name, int VolunteerId = 0)
        {
            return Json(!db.Volunteers.Any(x => x.Name == Name.Trim() && x.VolunteerId != VolunteerId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDonorNameValid(string Name, int DonorId = 0)
        {
            return Json(!db.Donors.Any(x => x.Name == Name.Trim() && x.DonorId != DonorId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBeneficiaryNameValid(string Name, int BeneficiaryId = 0)
        {
            return Json(!db.Beneficiaries.Any(x => x.Name == Name.Trim() && x.BeneficiaryId != BeneficiaryId), JsonRequestBehavior.AllowGet);
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