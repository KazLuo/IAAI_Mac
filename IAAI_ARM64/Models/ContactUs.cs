using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static IAAI_ARM64.Models.EnumList;

namespace IAAI_ARM64.Models
{
    public class ContactUs
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [Display(Name = "姓　名：")]
        [Required(ErrorMessage ="姓名必填")]
        public string Name { get; set; }
        [Display(Name = "性　別：")]
        //使用來自About.cs的Gender
        [Required(ErrorMessage = "性別必填")]
        public Gender? Gender { get; set; }

        [Display(Name = "E-mail：")]
        [Required(ErrorMessage = "Email必填")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "聯絡電話：")]
        [Required(ErrorMessage = "連絡電話必填")]
        public string Phone { get; set; }

        [Display(Name = "詢問內容：")]
        [Required(ErrorMessage = "詢問內容必填")]
        public string Message { get; set; }

        [Display(Name = "建立時間")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [NotMapped]
        [Display(Name = "驗證碼：")]
        //[Required(ErrorMessage = "驗證碼必填")]
        public string Captcha { get; set; }


    }


}