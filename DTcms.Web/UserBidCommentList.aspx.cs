using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class UserBidCommentList : UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检查登录
            if (!IsUserLogin()) SkipLogin();
        }
    }
}