using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class UserCenter : UI.BasePage
    {

        /// <summary>
        /// 基本用户信息
        /// </summary>
        public DTcms.Model.users UserInfo { get; set; }

        /// <summary>
        /// 用户拓展信息
        /// </summary>
        public DTcms.Model.UserExt UserExt { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //检查登录
            if (!IsUserLogin()) SkipLogin();
            //非响应回发
            if (!IsPostBack)
            {
                UserInfo = GetUserInfo();//获取用户信息
                UserExt = new DTcms.BLL.UserExt().GetModel(UserInfo.id) ?? new DTcms.Model.UserExt();
            }
        }
    }
}