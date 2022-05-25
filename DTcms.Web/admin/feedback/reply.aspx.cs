using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.admin.feedback
{
    public partial class reply : ManagePage
    {
        //protected Button btnSubmit;
        //protected HtmlForm form1;
        private int id = 0;
        protected Model.feedback model = new Model.feedback();
        //protected TextBox txtReContent;

        protected void btnSubmit_Click(object sender, EventArgs e) {
            ChkAdminLevel("plugin_feedback", DTEnums.ActionEnum.Reply.ToString());
            BLL.feedback feedback = new BLL.feedback();
            model = feedback.GetModel(id);
            model.reply_content = Utils.ToHtml(txtReContent.Text);
            model.reply_time = DateTime.Now;
            model.is_lock = rblStatus.SelectedIndex;

            feedback.Update(model);
            AddAdminLog(DTEnums.ActionEnum.Reply.ToString(), "回复留言插件内容：" + model.title);
            JscriptMsg("留言回复成功！", "index.aspx", "Success");
        }

        protected void Page_Load(object sender, EventArgs e) {
            id = DTRequest.GetQueryInt("id");
            if (id == 0) {
                JscriptMsg("传输参数不正确！", "back", "Error");
            } else if (!new BLL.feedback().Exists(id)) {
                JscriptMsg("信息不存在或已被删除！", "back", "Error");
            } else if (!Page.IsPostBack) {
                ChkAdminLevel("plugin_feedback", DTEnums.ActionEnum.View.ToString());
                ShowInfo(id);
            }
        }

        private void ShowInfo(int _id) {
            model = new BLL.feedback().GetModel(_id);
            txtReContent.Text = Utils.ToTxt(model.reply_content);
            rblStatus.SelectedIndex = model.is_lock;
        }
    }
}