using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.admin.link
{
    public partial class link_edit : ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString();
        //protected Button btnSubmit;
        //protected CheckBox cbIsRed;
        //protected HtmlForm form1;
        private int id = 0;
        //protected RadioButtonList rblIsLock;
        //protected TextBox txtEmail;
        //protected TextBox txtImgUrl;
        //protected TextBox txtSiteUrl;
        //protected TextBox txtSortId;
        //protected TextBox txtTitle;
        //protected TextBox txtUserName;
        //protected TextBox txtUserTel;

        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (action == DTEnums.ActionEnum.Edit.ToString()) {
                base.ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Edit.ToString());
                if (!DoEdit(id)) {
                    base.JscriptMsg("保存过程中发生错误！", "", "Error");
                } else {
                    base.JscriptMsg("修改链接成功！", "index.aspx", "Success");
                }
            } else {
                base.ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Add.ToString());
                if (!DoAdd()) {
                    base.JscriptMsg("保存过程中发生错误！", "", "Error");
                } else {
                    base.JscriptMsg("添加链接成功！", "index.aspx", "Success");
                }
            }
        }

        private bool DoAdd() {
            bool flag = false;
            Model.link model = new Model.link();
            BLL.link link2 = new BLL.link();
            model.title = txtTitle.Text.Trim();
            model.is_lock = Utils.StrToInt(rblIsLock.SelectedValue, 0);
            if (cbIsRed.Checked) {
                model.is_red = 1;
            } else {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 0x63);
            model.user_name = txtUserName.Text.Trim();
            model.user_tel = txtUserTel.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.site_url = txtSiteUrl.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.is_image = 1;
            if (string.IsNullOrEmpty(model.img_url)) {
                model.is_image = 0;
            }
            if (link2.Add(model) > 0) {
                base.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加友情链接：" + model.title);
                flag = true;
            }
            return flag;
        }

        private bool DoEdit(int _id) {
            bool flag = false;
            BLL.link link = new BLL.link();
            Model.link model = link.GetModel(_id);
            model.title = txtTitle.Text.Trim();
            model.is_lock = Utils.StrToInt(rblIsLock.SelectedValue, 0);
            if (cbIsRed.Checked) {
                model.is_red = 1;
            } else {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 0x63);
            model.user_name = txtUserName.Text.Trim();
            model.user_tel = txtUserTel.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.site_url = txtSiteUrl.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.is_image = 1;
            if (string.IsNullOrEmpty(model.img_url)) {
                model.is_image = 0;
            }
            if (link.Update(model)) {
                base.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改友情链接：" + model.title);
                flag = true;
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e) {
            string queryString = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(queryString) && (queryString == DTEnums.ActionEnum.Edit.ToString())) {
                action = DTEnums.ActionEnum.Edit.ToString();
                id = DTRequest.GetQueryInt("id");
                if (id == 0) {
                    base.JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.link().Exists(id)) {
                    base.JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack) {
                base.ChkAdminLevel("plugin_link", DTEnums.ActionEnum.View.ToString());
                if (action == DTEnums.ActionEnum.Edit.ToString()) {
                    ShowInfo(id);
                }
            }
        }

        private void ShowInfo(int _id) {
            Model.link model = new BLL.link().GetModel(_id);
            txtTitle.Text = model.title;
            if (model.is_red == 1) {
                cbIsRed.Checked = true;
            } else {
                cbIsRed.Checked = false;
            }
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtUserName.Text = model.user_name;
            txtUserTel.Text = model.user_tel;
            txtEmail.Text = model.email;
            txtSiteUrl.Text = model.site_url;
            txtImgUrl.Text = model.img_url;
        }
    }
}