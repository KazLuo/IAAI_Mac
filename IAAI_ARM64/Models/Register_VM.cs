using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_ARM64.Models
{
    public class Register_VM
    {

        public Members Members { get; set; }
        public ServiceHistory ServiceHistory { get; set; }
        [NotMapped]
        [Display(Name = "驗證碼：")]
        //[Required(ErrorMessage = "驗證碼必填")]
        public string Captcha { get; set; }
    }
}