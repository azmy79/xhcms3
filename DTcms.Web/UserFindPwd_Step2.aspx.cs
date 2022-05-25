using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class UserFindPwd_Step2 : UI.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserFindPwd"] == null)
                SkipIndex();
        }
    }
}