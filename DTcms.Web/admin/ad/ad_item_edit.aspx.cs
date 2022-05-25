using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.ad
{
    public partial class ad_item_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        //protected string channel_name = string.Empty; //频道名称
        protected int ad_id;
        protected Model.ad Ad;
        private int id;

        //页面加载事件
        protected void Page_Load(object sender, EventArgs e) {
            ad_id = DTRequest.GetQueryInt("ad_id");
            string _action = DTRequest.GetQueryString("action");
            if (ad_id == 0) {
                JscriptMsg("广告参数不正确！", "back", "Error");
                return;
            }
            Ad = new BLL.ad().GetModel(ad_id); //取得频道名称
            if (ad_id == 0) {
                JscriptMsg("广告不存在或已被删除！", "back", "Error");
                return;
            }

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString()) {
                action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                id = DTRequest.GetQueryInt("id");
                if (id == 0) {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.ad_item().Exists(id)) {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (Page.IsPostBack)
                return;

            ChkAdminLevel("ad_list", DTEnums.ActionEnum.View.ToString()); // 检查权限
            if (action == DTEnums.ActionEnum.Edit.ToString()) {
                ShowInfo(id);
            } else {
                txtStart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                txtEnd.Text = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd 23:59:59");
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id) {
            var model = new BLL.ad_item().GetModel(_id);
            if (model == null)
                return;

            txtTitle.Text = model.title;
            txtTag.Text = model.tag;
            txtStart.Text = model.start_time.ToString("yyyy-MM-dd HH:mm:ss");
            txtEnd.Text = model.end_time.ToString("yyyy-MM-dd HH:mm:ss");
            txtImg.Text = model.ad_url;
            txtLink.Text = model.link_url;
            txtRemarks.Text = model.remarks;
            rblStatus.SelectedIndex = model.is_lock.GetValueOrDefault();
            txtAdd.Text = model.add_time.ToString("yyyy-MM-dd HH:mm:ss");
            txtSortId.Text = model.sort_id.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd() {
            bool result = false;
            var model = new Model.ad_item {
                ad_id = ad_id,
                title = txtTitle.Text.Trim(),
                tag = txtTag.Text.Trim(),
                start_time = Utils.StrToDateTime(txtStart.Text.Trim()),
                end_time = Utils.StrToDateTime(txtEnd.Text.Trim()),
                ad_url = txtImg.Text,
                link_url = txtLink.Text,
                remarks = txtRemarks.Text,
                is_lock = rblStatus.SelectedIndex,
                add_time = Utils.StrToDateTime(txtAdd.Text.Trim()),
                sort_id = Convert.ToInt32(txtSortId.Text)
            };

            if (new BLL.ad_item().Add(model)) {
                //开始生成缩略图咯
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加广告内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id) {
            bool result = false;
            var model = new Model.ad_item {
                id = _id,
                ad_id = ad_id,
                title = txtTitle.Text.Trim(),
                tag = txtTag.Text.Trim(),
                start_time = Utils.StrToDateTime(txtStart.Text.Trim()),
                end_time = Utils.StrToDateTime(txtEnd.Text.Trim()),
                ad_url = txtImg.Text,
                link_url = txtLink.Text,
                remarks = txtRemarks.Text,
                is_lock = rblStatus.SelectedIndex,
                add_time = Utils.StrToDateTime(txtAdd.Text.Trim()),
                sort_id = Convert.ToInt32(txtSortId.Text)
            };

            if (new BLL.ad_item().Update(model)) {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改广告内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        // 保存
        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (action == DTEnums.ActionEnum.Edit.ToString()) { // 修改
                ChkAdminLevel("ad_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(id)) {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "ad_item.aspx?ad_id=" + ad_id, "Success");
            } else { // 添加
                ChkAdminLevel("ad_list", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd()) {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "ad_item.aspx?ad_id=" + ad_id, "Success");
            }
        }
    }
}