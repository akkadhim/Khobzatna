using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class Transaction
    {
        public Int64 TransactionId { get; set; }

        public string TransactionNo { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string UserNote { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Debit { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Credit { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Balance { get; set; } = 0;

        public bool IsDeleted { get; set; } = false;
        public bool IsPosted { get; set; } = true;
        public bool IsArchive { get; set; } = false;

        public Int64 OperationId { get; set; }
        public virtual Operation Operation { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}