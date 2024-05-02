using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI_ARM64.Models
{
    public class ServiceHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int Id { get; set; }
        public int MemberId { get; set; }
        [Display(Name = "服務單位")]
        public string ServiceUnit { get; set; }
        [Display(Name ="職稱")]
        public string Title { get; set; }

        [Display(Name = "開始服務年份")]
        public DateTime StartYear { get; set; }
        [Display(Name ="開始服務月份")]
        public DateTime StartMonth { get; set; }
        [Display(Name = "結束服務年份")]
        public DateTime EndYear { get; set; }
        [Display(Name = "結束服務月份")]
        public DateTime EndMonth { get; set; }
        [Display(Name = "服務單位")]
        public string ServiceUnit2 { get; set; }
        [Display(Name = "職稱")]
        public string Title2 { get; set; }

        [Display(Name = "開始服務年份")]
        public DateTime StartYear2 { get; set; }
        [Display(Name = "開始服務月份")]
        public DateTime StartMonth2 { get; set; }
        [Display(Name = "結束服務年份")]
        public DateTime EndYear2 { get; set; }
        [Display(Name = "結束服務月份")]
        public DateTime EndMonth2 { get; set; }
        [Display(Name ="創建時間")]
        public DateTime CreateTime { get; set; }=DateTime.Now;

        public virtual Members Members { get; set; }








    }



}