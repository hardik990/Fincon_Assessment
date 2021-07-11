using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fincon_Assessment.Common
{
    public static class Functions
    {
        public static readonly string StrCookieName = "FinconCookies";
        public static void UpdateCookies(string strUserName, string strUserId)
        {
            HttpContext.Current.Response.Cookies["FinconCookies"].Expires = DateTime.Now.AddDays(-1);
            HttpCookie hcUser = new HttpCookie("FinconCookies");
            hcUser.HttpOnly = true;
            hcUser.Values["username"] = strUserName;
            hcUser.Values["userid"] = strUserId;
            hcUser.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(hcUser);
        }
        public static void UpdateActiveID(string ActiveID)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies[StrCookieName];
            if (cookie != null)
            {
                cookie.Values["activeid"] = ActiveID;
            }
        }
    }
}