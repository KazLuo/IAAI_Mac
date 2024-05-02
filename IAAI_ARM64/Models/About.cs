using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static IAAI_ARM64.Models.EnumList;

namespace IAAI_ARM64.Models
{
    public class About
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name ="職位")]
        [Required]
        public string Position { get; set; }
        [Display(Name="姓名")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "性別")]
        [Required]
        public Gender Gender { get; set; }
        [Display(Name ="經歷")]
        [Required]
        public string Experience { get; set; }

    }
   
}