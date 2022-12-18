using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace company1.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl)
        {
            if (!urlHelper.IsLocalUrl(localUrl))
            {
                return urlHelper.Page("/Index");
            }

            return localUrl;
        }

        public static string GetSingleDisplayName(this Enum flag)
        {
            try
            {
                return flag.GetType()
                    .GetMember(flag.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    ?.Name;
            }
            catch
            {
                return flag.ToString();
            }
        }

    }
}

