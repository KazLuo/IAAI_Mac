﻿using System.Web;
using System.Web.Mvc;

namespace IAAI_ARM64
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
