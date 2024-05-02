using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI_ARM64.Models
{
    public class EnumList
    {
        public enum ApplyType
        {
            正式會員 = 1,
            準會員 = 2,
            個人贊助會員 = 3,
            學生會員 = 4
        }

        public enum Gender
        {
            男,
            女,
            未選擇
        }
    }
}