using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class OperationAction
    {
        public Int64 OperationActionId { get; set; }

        [Display(Name = "اسم العملية")]
        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }

        [Display(Name = "ارتباط بالحساب")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Display(Name = "اتجاه العملية")]
        public Direction Direction { get; set; }

        [Display(Name = "نوع العملية")]
        public ActionType ActionType { get; set; }

        [Display(Name = "هل هي اخيرة")]
        public bool IsFinalNode { get; set; }
        [Display(Name = "اظهارها في المعاملة")]
        public bool IsVisible { get; set; }

        public bool HasVehicle { get; set; }
        public bool HasContainer { get; set; }
    }

    public enum ActionType
    {
        [Display(Name = "دائن")]
        Credit,
        [Display(Name = "مدين")]
        Debit,
    }

    public enum Direction
    {
        [Display(Name = "قبض")]
        Input,
        [Display(Name = "صرف")]
        Output,
    }
}