using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class UserLoginOut : UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //清险Session
            HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] = null;
            //清除Cookies
            Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", -43200);
            Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", -43200);
            Response.Redirect("index.aspx");
            Response.End();
        }
    }
}