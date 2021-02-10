using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class ToDoTask
    {
        public ToDoTask()
        {
            Jobs = new HashSet<Job>();
        }

        public Int64 ToDoTaskId { get; set; }
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }
        [Display(Name = "الحالة")]
        public ToDoTaskStatus Status { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }

    public enum ToDoTaskStatus
    {
        Waiting,
        Done,
        Pending
    }
}