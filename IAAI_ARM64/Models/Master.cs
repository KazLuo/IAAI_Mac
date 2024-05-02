using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI_ARM64.Models
{
    public class Master
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required]
        public string Name { get; set; }

        [Display(Name ="現職")]
        [Required]
        public string Position { get; set; }

        [Display(Name ="相片")]
        public string Photo { get; set; }
        [Display(Name ="經歷")]
        [Required]
        public string Experience { get; set; }

        [Display(Name = "學歷")]
        public string Education { get; set; }
        [Display(Name = "基本介紹")]
        public string Introduction { get; set; }
        [Display(Name = "詳細資訊")]
        public string Detail { get; set; }

    }
}