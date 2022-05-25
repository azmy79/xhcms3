using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.ad
{
    public partial class ad_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e) {
            string _action = DTRequest.GetQueryString("action");
            id = DTRequest.GetQueryInt("id");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString()) {
                action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (id == 0) {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.ad().Exists(id)) {
                    JscriptMsg("类别不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (Page.IsPostBack)
                return;

            ChkAdminLevel("ad_list", DTEnums.ActionEnum.View.ToString()); // 检查权限
            if (action != DTEnums.ActionEnum.Edit.ToString())
                return;

            ShowInfo(id);
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id) {
            Model.ad model = new BLL.ad().GetModel(_id);

            rblAdType.SelectedValue = model.adtype.ToString();
            txtTitle.Text = model.title;
            txtAdWidth.Text = model.width.ToString();
            txtAdHeight.Text = model.height.ToString();
            rblAdTarget.SelectedValue = model.target;
            txtNum.Text = model.num.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtContent.InnerText = model.remarks;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd() {
            try {
                var model = new Model.ad() {
                    adtype = Convert.ToInt32(rblAdType.SelectedValue),
                    title = txtTitle.Text,
                    num = Convert.ToInt32(txtNum.Text),
                    width = Convert.ToInt32(txtAdWidth.Text),
                    height = Convert.ToInt32(txtAdHeight.Text),
                    target = rblAdTarget.SelectedValue,
                    sort_id = Convert.ToInt32(txtSortId.Text),
                    remarks = txtContent.InnerText
                };

                if (new BLL.ad().Add(model)) {
                    AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加广告:" + model.title); //记录日志
                    return true;
                }
            } catch {
                return false;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id) {
            try {
                BLL.ad bll = new BLL.ad();
                Model.ad model = bll.GetModel(_id);

                model.adtype = Convert.ToInt32(rblAdType.SelectedValue);
                model.title = txtTitle.Text;
                model.width = Convert.ToInt32(txtAdWidth.Text);
                model.height = Convert.ToInt32(txtAdHeight.Text);
                model.target = rblAdTarget.SelectedValue;
                model.num = Convert.ToInt32(txtNum.Text);
                model.sort_id = Convert.ToInt32(txtSortId.Text);
                model.remarks = txtContent.InnerText;

                if (bll.Update(model)) {
                    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改广告:" + model.title); //记录日志
                    return true;
                }
            } catch {
                return false;
            }
            return false;
        }
        #endregion

        //保存类别
        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("ad_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(id)) {

                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改类别成功！", "ad_list.aspx", "Success");
            } else //添加
            {
                ChkAdminLevel("ad_list", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd()) {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加类别成功！", "ad_list.aspx", "Success");
            }
        }

    }
}