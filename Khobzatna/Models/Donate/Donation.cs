using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class Donation
    {
        // the donation process for particular donatioType from donor
        public Int64 DonationId { get; set; }
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "الحالة")]
        public DonationStatus Status { get; set; }

        [Display(Name = "المبلغ ان وجد بالدينار")]
        public double Payment { get; set; } = 0;

        [Display(Name = "اسم المتبرع")]
        public int DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        [Display(Name = "نوع التبرع")]
        public int DonationTypeId { get; set; }
        public virtual DonationType DonationType { get; set; }
    }

    public enum DonationStatus
    {
        Waiting,
        Collected,
        Ignored
    }
}