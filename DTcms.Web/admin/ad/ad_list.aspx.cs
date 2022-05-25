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
    public partial class ad_item_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string channel_name = string.Empty;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = string.Empty;

        protected void Page_Load(object sender, EventArgs e) {
            keywords = DTRequest.GetQueryString("keywords");
            property = DTRequest.GetQueryString("property");

            //if (channel_id == 0) {
            //    JscriptMsg("频道参数不正确！", "back", "Error");
            //    return;
            //}
            //channel_name = new BLL.channel().GetChannelName(channel_id); //取得频道名称
            pageSize = GetPageSize(10); //每页数量
            prolistview = Utils.GetCookie("ad_list_view"); //显示方式
            if (Page.IsPostBack)
                return;

            ChkAdminLevel("ad_list", DTEnums.ActionEnum.View.ToString()); //检查权限
            //TreeBind(channel_id); //绑定类别
            RptBind("id>0" + CombSqlTxt(keywords, property), "sort_id asc,add_time desc,id desc");
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby) {
            page = DTRequest.GetQueryInt("page", 1);

            txtKeywords.Text = keywords;
            //图表或列表显示
            BLL.ad bll = new BLL.ad();
            //switch (prolistview) {
            //    case "Txt":
            //        rptList2.Visible = false;
            rptList1.DataSource = bll.GetList(pageSize, page, _strWhere, _orderby, out totalCount);
            rptList1.DataBind();
            //        break;
            //    default:
            //        rptList1.Visible = false;
            //        rptList2.DataSource = bll.GetList(pageSize, page, _strWhere, _orderby, out totalCount);
            //        rptList2.DataBind();
            //        break;
            //}
            //绑定页码
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("ad_list.aspx", "keywords={0}&page={1}",
                 keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, string _property) {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords)) {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size) {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("article_page_size"), out _pagesize)) {
                if (_pagesize > 0) {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e) {
            Response.Redirect(Utils.CombUrlTxt("ad_list.aspx", "keywords={0}", txtKeywords.Text));
        }

        //设置文字列表显示
        protected void lbtnViewTxt_Click(object sender, EventArgs e) {
            Utils.WriteCookie("ad_list_view", "Txt", 14400);
            Response.Redirect(Utils.CombUrlTxt("ad_list.aspx", "keywords={0}&page={1}",
                keywords, page.ToString()));
        }

        //设置图文列表显示
        protected void lbtnViewImg_Click(object sender, EventArgs e) {
            Utils.WriteCookie("ad_list_view", "Img", 14400);
            Response.Redirect(Utils.CombUrlTxt("ad_list.aspx", "keywords={0}&page={1}",
                keywords, page.ToString()));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e) {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize)) {
                if (_pagesize > 0) {
                    Utils.WriteCookie("article_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("ad_list.aspx", "keywords={0}",
                keywords));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e) {
            ChkAdminLevel("ad_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.ad bll = new BLL.ad();
            //switch (prolistview) {
            //    case "Txt":
            Repeater rptList = rptList1;
            //        break;
            //    default:
            //        rptList = rptList2;
            //        break;
            //}
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId)) {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存" + channel_name + "频道内容排序"); //记录日志
            JscriptMsg("保存排序成功啦！", Utils.CombUrlTxt("ad_list.aspx", "keywords={0}", keywords), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e) {
            ChkAdminLevel("ad_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.ad bll = new BLL.ad();
            //switch (prolistview) {
            //    case "Txt":
            Repeater rptList = rptList1;
            //        break;
            //    default:
            //        rptList = rptList2;
            //        break;
            //}
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked) {
                    if (bll.Delete(id)) {
                        sucCount++;
                    } else {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除" + channel_name + "频道内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("ad_list.aspx", "keywords={0}",
                keywords), "Success");
        }
    }
}