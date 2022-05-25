using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.admin.link
{
    public partial class index : ManagePage
    {

        protected string keywords;
        protected int page;
        protected int pageSize;
        protected int totalCount;

        protected void btnDelete_Click(object sender, EventArgs e) {
            ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Delete.ToString());
            int num = 0;
            int num2 = 0;
            BLL.link link = new BLL.link();
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox box = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (box.Checked) {
                    if (link.Delete(id)) {
                        num++;
                    } else {
                        num2++;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), string.Concat(new object[] { "删除友情链接成功", num, "条，失败", num2, "条" }));
            JscriptMsg(string.Concat(new object[] { "删除成功", num, "条，失败", num2, "条！" }), Utils.CombUrlTxt("index.aspx", "keywords={0}", new string[] { keywords }), "Success");
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Edit.ToString());
            BLL.link link = new BLL.link();
            for (int i = 0; i < rptList.Items.Count; i++) {
                int num3;
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out num3)) {
                    num3 = 0x63;
                }
                link.UpdateField(id, "sort_id=" + num3.ToString());
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改友情链接插件排序:");
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("index.aspx", "keywords={0}", new[] { keywords }), "Success");
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            Response.Redirect(Utils.CombUrlTxt("index.aspx", "keywords={0}", new[] { txtKeywords.Text }));
        }

        protected string CombSqlTxt(string _keywords) {
            StringBuilder builder = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords)) {
                builder.Append(" and title like '%" + _keywords + "%'");
            }
            return builder.ToString();
        }

        private int GetPageSize(int _default_size) {
            int num;
            if (int.TryParse(Utils.GetCookie("link_page_size"), out num) && (num > 0)) {
                return num;
            }
            return _default_size;
        }

        protected void lbtnUnLock_Click(object sender, EventArgs e) {
            ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Audit.ToString());
            BLL.link link = new BLL.link();
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox box = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (box.Checked) {
                    link.UpdateField(id, "is_lock=0");
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Audit.ToString(), "审核友情链接");
            JscriptMsg("批量审核成功！", Utils.CombUrlTxt("index.aspx", "keywords={0}", new[] { keywords }), "Success");
        }

        protected void Page_Load(object sender, EventArgs e) {
            keywords = DTRequest.GetQueryString("keywords");
            pageSize = GetPageSize(10);
            if (!Page.IsPostBack) {
                ChkAdminLevel("plugin_link", DTEnums.ActionEnum.View.ToString());
                RptBind("id>0" + CombSqlTxt(keywords), "sort_id asc,add_time desc");
            }
        }

        private void RptBind(string _strWhere, string _orderby) {
            page = DTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = keywords;
            rptList.DataSource = new BLL.link().GetList(pageSize, page, _strWhere, _orderby, out totalCount);
            rptList.DataBind();
            txtPageNum.Text = pageSize.ToString();
            string linkUrl = Utils.CombUrlTxt("index.aspx", "keywords={0}&page={1}", new string[] { keywords, "__id__" });
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, linkUrl, 8);
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e) {
            ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Edit.ToString());
            int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
            BLL.link link = new BLL.link();
            Model.link model = link.GetModel(id);
            string str = e.CommandName.ToLower();
            if ((str != null) && (str == "lbtnisred")) {
                if (model.is_red == 1) {
                    link.UpdateField(id, "is_red=0");
                } else {
                    link.UpdateField(id, "is_red=1");
                }
            }
            RptBind("id>0" + CombSqlTxt(keywords), "sort_id asc,add_time desc");
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e) {
            int num;
            if (int.TryParse(txtPageNum.Text.Trim(), out num) && (num > 0)) {
                Utils.WriteCookie("link_page_size", num.ToString(), 0x3840);
            }
            Response.Redirect(Utils.CombUrlTxt("index.aspx", "keywords={0}", new string[] { keywords }));
        }
    }
}