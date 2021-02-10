using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class Job
    {
        public Int64 JobId { get; set; }
        [Display(Name ="الحالة")]
        public JobStatus Status { get; set; }
        [Display(Name ="التاريخ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name ="الوصف")]
        public string Note { get; set; }

        public Int64 ToDoTaskId { get; set; }
        public virtual ToDoTask ToDoTask { get; set; }

        public int? DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        public int? VolunteerId { get; set; }
        public virtual Volunteer Volunteer { get; set; }

        public int? BeneficiaryId { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }

        public Int64? DonationId { get; set; }
        public virtual Donation Donation { get; set; }

        public Int64? RequestId { get; set; }
        public virtual Request Request { get; set; }

        public Int64? OperationId { get; set; }
        public virtual Operation Operation { get; set; }
    }

    public enum JobStatus
    {
        Started,
        InProgress,
        Done,
    }
}