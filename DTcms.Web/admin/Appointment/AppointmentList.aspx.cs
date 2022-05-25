using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Appointment
{
    public partial class AppointmentList : UI.ManagePage
    {
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //非响应回发
            if (!IsPostBack)
            {
                //搜索框
                txtKeywords.Text = DTcms.Common.DTRequest.GetQueryString("kw");
                //页大小输入框赋值
                txtPageNum.Text = DTcms.Common.Utils.GetPageSize(10).ToString();
                //绑定列表
                BindData();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindData()
        {
            //页大小
            var PageIndex = DTcms.Common.DTRequest.GetInt("pix", 1);
            //页大小
            var PageSize = DTcms.Common.Utils.GetPageSize(10);
            //总条数
            var TotalCount = 0;
            //查询字符串
            var strWhere = "1=1";
            var admin = GetAdminInfo();
            //权限
            if (admin.role_id == 3)
                strWhere += " and ManagerID=" + admin.id + " ";
            //关键字
            if (!string.IsNullOrEmpty(txtKeywords.Text.Trim()))
                strWhere += " and (Name like'%" + txtKeywords.Text.Trim() + "%' or Number='" + txtKeywords.Text.Trim() + "')";
            rptList.DataSource = new DTcms.BLL.Appointment().GetModelList(PageSize, PageIndex, strWhere, "Date Desc,AddTime Desc", out TotalCount);
            rptList.DataBind();
            //页码溢出跳转最后一页
            if (PageIndex != 1 && !(TotalCount > PageSize * (PageIndex - 1)))
                Search(TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1));
            //跳转地址
            string pageUrl = string.Format("AppointmentList.aspx?pix=__id__&kw={0}", txtKeywords.Text.Trim());
            //分页HTML
            PageContent.InnerHtml = DTcms.Common.Utils.OutPageList(PageSize, PageIndex, TotalCount, pageUrl, 8);
        }


        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            //设置页大小
            DTcms.Common.Utils.SetPageSize(txtPageNum.Text.Trim());
            Search();
        }

        /// <summary>
        /// 查询(跳转第一页)
        /// </summary>
        public void Search(int pageIndex = 1)
        {
            var url = string.Format("AppointmentList.aspx?pix={0}&kw={1}", pageIndex, txtKeywords.Text.Trim());
            Response.Redirect(url);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}