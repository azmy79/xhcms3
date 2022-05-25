using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.ad
{
    public partial class ad_code : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e) {
            string _action = DTRequest.GetQueryString("action");
            id = DTRequest.GetQueryInt("id");
            if (id == 0) {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.ad().Exists(id)) {
                JscriptMsg("广告不存或已被删除！", "back", "Error");
                return;
            }

            if (Page.IsPostBack)
                return;

            ShowInfo(id);
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id) {
            txtCode.Text = ltView.Text = string.Format("<script type=\"text/javascript\" src=\"/tools/ad.ashx?id={0}\"></script>", _id);
        }
        #endregion

    }
}