using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khobzatna.Models
{
    public class Beneficiary
    {
        public Beneficiary()
        {
            Requests = new HashSet<Request>();
            Jobs = new HashSet<Job>();
        }

        public int BeneficiaryId { get; set; }
        [Required]
        [Display(Name = "الاسم")]
        [Remote("IsBeneficiaryNameValid", "Validation", ErrorMessage = "هذا الاسم مستعمل سابقا!", AdditionalFields = "BeneficiaryId")]
        public string Name { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "جي بي اس")]
        public string GPS { get; set; }
        [Range(1, 30, ErrorMessage = "عدد افراد الاسرة المسموح من 1 الى 30")]
        [Display(Name = "عدد افراد الاسرة")]
        public int FamilyNo { get; set; } = 0;
        [Display(Name = "العمر")]
        [Range(10, 120, ErrorMessage = "العمر المسموح من 10 الى 120")]
        public int age { get; set; } = 0;
        [DataType(DataType.MultilineText)]
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}