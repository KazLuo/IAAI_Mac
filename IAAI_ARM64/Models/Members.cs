using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static IAAI_ARM64.Models.EnumList;

namespace IAAI_ARM64.Models
{
    public class Members
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [Display(Name="密碼")]
        [Required(ErrorMessage ="請輸入密碼")]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage ="請輸入姓名")]
        public string Name { get; set; }

        [Display(Name = "性別")]
        [Required(ErrorMessage = "請選擇性別")]
        //設定性別為nullable才可以判定是否選擇
        public Gender? Gender { get; set; }

        [Display(Name = "生日")]
        [Required(ErrorMessage = "請輸入生日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "申請類別")]
        [Required(ErrorMessage = "請選擇申請類別")]
        public ApplyType ApplyType { get; set; }
        [Display(Name ="通訊處")]
        [Required(ErrorMessage = "請輸入通訊處")]
        public string Address { get; set; }
        [Display(Name ="E-Mail")]
        [Required(ErrorMessage = "請輸入E-Mail")]
        public string Email { get; set; }
        [Display(Name ="國際會籍")]
        public bool International { get; set; }
        [Display(Name ="現職單位")]
        [Required(ErrorMessage ="現職單位必填")]
        public string CurrentPosition { get; set; }
        [Display(Name ="職稱")]
        [Required(ErrorMessage ="職稱必填")]
        public string Title { get; set; }
        [Display(Name ="最高學歷")]
        [Required(ErrorMessage ="最高學歷必填")]
        public string Education { get; set; }
        [Display(Name = "創建時間")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public ICollection<ServiceHistory> serviceHistories { get; set; }
    }
   
}