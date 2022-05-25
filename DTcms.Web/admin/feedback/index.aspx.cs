using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.admin.feedback
{
    public partial class index : ManagePage
    {
        /*        
        protected LinkButton btnDelete;
        protected LinkButton btnSearch;
        protected HtmlForm form1;
        protected LinkButton lbtnUnLock;
        protected HtmlGenericControl PageContent;
        protected Repeater rptList;
        protected TextBox txtKeywords;
        protected TextBox txtPageNum;*/
        protected string keywords = string.Empty;
        protected int pageSize;
        protected int page;
        protected int totalCount;

        protected void btnDelete_Click(object sender, EventArgs e) {
            ChkAdminLevel("plugin_feedback", DTEnums.ActionEnum.Delete.ToString());
            int num = 0;
            int num2 = 0;
            BLL.feedback feedback = new BLL.feedback();
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox box = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (!box.Checked)
                    continue;
                if (feedback.Delete(id)) {
                    num++;
                } else {
                    num2++;
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), string.Concat(new object[] { "删除留言成功", num, "条，失败", num2, "条" }));
            JscriptMsg(string.Concat(new object[] { "删除成功", num, "条，失败", num2, "条！" }), Utils.CombUrlTxt("index.aspx", "keywords={0}", new string[] { keywords }), "Success");
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            Response.Redirect(Utils.CombUrlTxt("index.aspx", "keywords={0}", new[] { txtKeywords.Text }));
        }

        protected string CombSqlTxt(string _keywords) {
            StringBuilder builder = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords)) {
                builder.Append(" and (title like '%" + _keywords + "%' or user_name like '%" + _keywords + "%')");
            }
            return builder.ToString();
        }

        private int GetPageSize(int defaultSize) {
            int num;
            if (int.TryParse(Utils.GetCookie("feedback_page_size"), out num) && (num > 0)) {
                return num;
            }
            return defaultSize;
        }

        protected void lbtnUnLock_Click(object sender, EventArgs e) {
            ChkAdminLevel("plugin_feedback", DTEnums.ActionEnum.Audit.ToString());
            BLL.feedback feedback = new BLL.feedback();
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox box = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (box.Checked) {
                    feedback.UpdateField(id, "is_lock=0");
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Audit.ToString(), "审核留言插件内容");
            JscriptMsg("批量审核成功！", Utils.CombUrlTxt("index.aspx", "keywords={0}", new string[] { keywords }), "Success");
        }

        protected void Page_Load(object sender, EventArgs e) {
            keywords = DTRequest.GetQueryString("keywords");
            pageSize = GetPageSize(10);
            if (!Page.IsPostBack) {
                ChkAdminLevel("plugin_feedback", DTEnums.ActionEnum.View.ToString());
                RptBind("MsgType=0 and id>0" + CombSqlTxt(keywords), "is_lock desc,add_time desc");
            }
        }

        private void RptBind(string strWhere, string _orderby) {
            if (!int.TryParse(Request.QueryString["page"], out page)) {
                page = 1;
            }
            txtKeywords.Text = keywords;
            rptList.DataSource = new BLL.feedback().GetList(pageSize, page, strWhere, _orderby, out totalCount);
            rptList.DataBind();
            txtPageNum.Text = pageSize.ToString();
            string linkUrl = Utils.CombUrlTxt("index.aspx", "keywords={0}&page={1}", new string[] { keywords, "__id__" });
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, linkUrl, 8);
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e) {
            int num;
            if (int.TryParse(txtPageNum.Text.Trim(), out num) && (num > 0)) {
                Utils.WriteCookie("feedback_page_size", num.ToString(), 0x3840);
            }
            Response.Redirect(Utils.CombUrlTxt("index.aspx", "keywords={0}", new string[] { keywords }));
        }
    }

}