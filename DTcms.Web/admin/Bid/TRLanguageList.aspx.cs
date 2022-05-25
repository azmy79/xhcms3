using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Bid
{
    public partial class TRLanguageList : UI.ManagePage
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
            rptList.DataSource = new DTcms.BLL.TRLanguage().GetModelList(PageSize, PageIndex, strWhere, "Sort, ID", out TotalCount);
            rptList.DataBind();
            //页码溢出跳转最后一页
            if (PageIndex != 1 && !(TotalCount > PageSize * (PageIndex - 1)))
                Search(TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1));
            //跳转地址
            string pageUrl = string.Format("TRLanguageList.aspx?pix=__id__");
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


        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                new DTcms.BLL.TRLanguage().UpdateField(id, "Sort=" + sortId.ToString());
            }
            JscriptMsg("保存排序成功", "Success");
            Search();
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            var bll = new BLL.TRLanguage();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                    if (bll.Delete(id)) sucCount++;
                    else errorCount++;
            }
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", "Success");
            BindData();
        }

        /// <summary>
        /// 查询(跳转第一页)
        /// </summary>
        public void Search(int pageIndex = 1)
        {
            var url = string.Format("TRLanguageList.aspx?pix={0}", pageIndex);
            Response.Redirect(url);
        }
    }
}