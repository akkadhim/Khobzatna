using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khobzatna.Models
{
    public class Donor
    {
        public Donor()
        {
            Donations = new HashSet<Donation>();
            Jobs = new HashSet<Job>();
        }

        public int DonorId { get; set; }
        [Required]
        [Remote("IsDonorNameValid", "Validation", ErrorMessage = "هذا الاسم مستعمل سابقا!", AdditionalFields = "DonorId")]
        [Display(Name = "اسم المتبرع")]
        public string Name { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "كمية التبرع")]
        public int DonationQty { get; set; }
        [Display(Name = "جعل هويتي مخفية")]
        public bool Private { get; set; }
        [Display(Name = "الوظيفة")]
        public string Job { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}