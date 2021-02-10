using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class Operation
    {
        public Operation()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Int64 OperationId { get; set; }
        public DateTime Date { get; set; }

        public int OperationTypeId { get; set; }
        public virtual OperationType OperationType { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}