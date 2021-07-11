using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Fincon_Assessment.Common
{
    public class MySession
    {
        public static MySession Current
        {
            get
            {
                return new MySession();
            }
        }
        public int UserId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["FinconCookies"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["FinconCookies"]["userid"]))
                {
                    return int.Parse(HttpContext.Current.Request.Cookies["FinconCookies"]["userid"].ToString());
                }

                return 0;
            }
        }
        public string UserName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["FinconCookies"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["FinconCookies"]["UserName"]))
                {
                    return HttpContext.Current.Request.Cookies["FinconCookies"]["UserName"].ToString();
                }
                return "";
            }
        }
        public string ActiveID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["FinconCookies"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["FinconCookies"]["activeid"]))
                {
                    return HttpContext.Current.Request.Cookies["FinconCookies"]["activeid"].ToString();
                }
                return "";
            }
        }
        public string UserPhoto
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["FinconCookies"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["FinconCookies"]["userphoto"]))
                {
                    return HttpContext.Current.Request.Cookies["FinconCookies"]["userphoto"].ToString();
                }
                return "~/images/user-photo.png";
            }
        }
        public string LogoutURL
        {
            get
            {
                return ConfigurationManager.AppSettings["Logout"];
            }
        }
    }
}