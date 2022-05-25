using DTcms.Common;
using DTcms.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Bid
{
    public partial class BidCommentReply : ManagePage
    {
        //protected Button btnSubmit;
        //protected HtmlForm form1;
        private int id = 0;
        protected Model.feedback model = new Model.feedback();
        //protected TextBox txtReContent;

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BLL.feedback feedback = new BLL.feedback();
            model = feedback.GetModel(id);
            model.reply_content = Utils.ToHtml(txtReContent.Text);
            model.reply_time = DateTime.Now;
            model.is_lock = rblStatus.SelectedIndex;

            feedback.Update(model);
            JscriptMsg("评论回复成功！", "BidComment.aspx", "Success");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            id = DTRequest.GetQueryInt("id");
            if (id == 0)
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
            }
            else if (!new BLL.feedback().Exists(id))
            {
                JscriptMsg("信息不存在或已被删除！", "back", "Error");
            }
            else if (!Page.IsPostBack)
            {
                ShowInfo(id);
            }
        }

        private void ShowInfo(int _id)
        {
            model = new BLL.feedback().GetModel(_id);
            txtReContent.Text = Utils.ToTxt(model.reply_content);
            rblStatus.SelectedIndex = model.is_lock;
        }
    }
}