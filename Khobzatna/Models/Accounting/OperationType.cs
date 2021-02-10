
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class OperationType
    {
        public OperationType()
        {
            OperationActions = new HashSet<OperationAction>();
        }

        public int OperationTypeId { get; set; }
        [Display(Name = "اسم العملية")]
        [StringLength(450)]
        public string Name { get; set; }
        [Display(Name = "هل تظهر للمستخدم")]
        public bool IsInMenu { get; set; } = true;

        public virtual ICollection<OperationAction> OperationActions { get; set; }
    }
}