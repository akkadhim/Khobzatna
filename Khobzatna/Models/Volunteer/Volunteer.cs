using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khobzatna.Models
{
    public class Volunteer
    {
        public Volunteer()
        {
            Jobs = new HashSet<Job>();
        }

        public int VolunteerId { get; set; }
        [Required]
        [Remote("IsVolunteerNameValid", "Validation", ErrorMessage = "هذا الاسم مستعمل سابقا!", AdditionalFields = "VolunteerId")]
        [Display(Name ="الاسم")]
        public string Name { get; set; }
        [Display(Name ="العنوان")]
        public string Address { get; set; }
        [Display(Name ="الوظيفة")]
        public string Job { get; set; }
        [Display(Name ="ايام التفرغ")]
        public string FreeTime { get; set; }
        [Display(Name ="الملاحظات")]
        public string Note { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}