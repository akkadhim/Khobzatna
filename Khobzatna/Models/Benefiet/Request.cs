using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class Request
    {
        // the donation process for particular donatioType from donor
        public Int64 RequestId { get; set; }
        [Display(Name = "ملاحظات")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "الحالة")]
        public RequestStatus Status { get; set; }

        public int BeneficiaryId { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }

        public int RequestTypeId { get; set; }
        public virtual RequestType RequestType { get; set; }
    }

    public enum RequestStatus
    {
        Waiting,
        Collected,
        Ignored,
        Uregent
    }
}