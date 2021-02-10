using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Display(Name = "رقم الحساب")]
        public string AccountNo { get; set; }
        [Display(Name = "اسم الحساب")]
        [StringLength(450)]
        public string Name { get; set; }
        [Display(Name = " اسم الحساب بالانكليزي")]
        public string EnglishName { get; set; }
        [Display(Name = "اسم الحساب المبسط")]
        public string FriendlyName { get; set; }

        [Display(Name = "تابع الى حساب")]
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Account Parent { get; set; }

        [Display(Name = "المستوى")]
        public int Level { get; set; }
        [Display(Name = "اخفاء الحساب")]
        public bool Disabled { get; set; } = false;

        [DataType(DataType.Date)]
        [Display(Name = "تاريخ انشائها")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "تاريخ اخر مطابقة")]
        public DateTime LastMatching { get; set; } = DateTime.Now;

        public bool IsCredit { get; set; } = false;
        [Display(Name = "له")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Credit { get; set; } = 0;
        [Display(Name = "عليه")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Debit { get; set; } = 0;
        [Display(Name = "الاجمالي")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Balance { get; set; } = 0;
        [Display(Name = "العملة")]
        public Currency Currency { get; set; } = Currency.Dinnar;

        [ForeignKey("ParentId")]
        public virtual ICollection<Account> Accounts { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public enum Currency
    {
        [Display(Name = "دينار عراقي")]
        Dinnar,
        [Display(Name = "دولار امريكي")]
        Dollar
    }
}