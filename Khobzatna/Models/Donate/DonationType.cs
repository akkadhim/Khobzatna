using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class DonationType
    {
        public DonationType()
        {
            Donations = new HashSet<Donation>();
        }
        // Donation is anything could be donate o other like food 
        public int DonationTypeId { get; set; }
        [Display(Name = "نوع التبرع")]
        public string Name { get; set; }
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }
        [Display(Name = "هل هي اموال")]
        public bool IsMoney { get; set; }

        public virtual ICollection<Donation> Donations { get; set; }
    }
}