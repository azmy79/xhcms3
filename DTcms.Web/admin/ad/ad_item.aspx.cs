using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.ad
{
    public partial class ad_list : Web.UI.ManagePage
    {
        protected int ad_id;
        protected Model.ad Ad; //频道名称

        protected void Page_Load(object sender, EventArgs e) {
            ad_id = DTRequest.GetQueryInt("ad_id");
            Ad = new BLL.ad().GetModel(ad_id); //取得频道名称
            if (Ad == null) {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }
            if (Page.IsPostBack)
                return;

            ChkAdminLevel("ad_list", DTEnums.ActionEnum.View.ToString()); //检查权限
            RptBind();
        }

        //数据绑定
        private void RptBind() {
            DataTable dt = new BLL.ad_item().GetList("ad_id = " + ad_id).Tables[0];
            rptList.DataSource = dt;
            rptList.DataBind();
        }

        ////美化列表
        //protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e) {
        //    if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        //        return;

        //    Literal litFirst = (Literal)e.Item.FindControl("LitFirst");
        //    HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
        //    const string LIT_STYLE = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
        //    const string LIT_IMG1 = "<span class=\"folder-open\"></span>";
        //    const string LIT_IMG2 = "<span class=\"folder-line\"></span>";

        //    int classLayer = Convert.ToInt32(hidLayer.Value);
        //    litFirst.Text = classLayer == 1 ? LIT_IMG1 : string.Format(LIT_STYLE, (classLayer - 2) * 24, LIT_IMG2, LIT_IMG1);
        //}

        protected string GetState(string strLock, string strTime) {
            if (strLock == "1")
                return "<font color=\"#FF0000\">已停止</font>";

            return DateTime.Compare(DateTime.Parse(strTime), DateTime.Today) == -1 ? "<font color=\"#FF0000\">已过期</font>" : "<font color=\"#009900\">正常</font>";
        }

        ////保存排序
        //protected void btnSave_Click(object sender, EventArgs e) {
        //    ChkAdminLevel("ad_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
        //    BLL.ad_item bll = new BLL.ad_item();
        //    for (int i = 0; i < rptList.Items.Count; i++) {
        //        int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
        //        int sortId;
        //        if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId)) {
        //            sortId = 99;
        //        }
        //        bll.UpdateField(id, "sort_id=" + sortId.ToString());
        //    }
        //    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存广告排序"); // 记录日志
        //    JscriptMsg("保存排序成功！", Utils.CombUrlTxt("ad_item.aspx", "ad_id={0}", ad_id.ToString()), "Success");
        //}

        // 删除类别
        protected void btnDelete_Click(object sender, EventArgs e) {
            ChkAdminLevel("ad_list", DTEnums.ActionEnum.Delete.ToString()); // 检查权限
            BLL.ad_item bll = new BLL.ad_item();
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked) {
                    bll.Delete(id);
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除广告数据"); // 记录日志
            JscriptMsg("删除数据成功！", Utils.CombUrlTxt("ad_item.aspx", "ad_id={0}", ad_id.ToString()), "Success");
        }
    }
}