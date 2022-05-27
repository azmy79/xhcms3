using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Bid
{
    public partial class BidList : UI.ManagePage
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
            //未支付
            var strWhere = "1=1 and Status<>2 ";
            if (!string.IsNullOrEmpty(txtKeywords.Text.Trim()))
                strWhere += "and (Number='" + txtKeywords.Text.Trim() + "' or CnName='" + txtKeywords.Text.Trim() + "' or Tel='" + txtKeywords.Text.Trim() + "')";
            rptList.DataSource = new DTcms.BLL.View_Bid().GetModelList(PageSize, PageIndex, strWhere, "Status,AddTime Desc, ID", out TotalCount);
            rptList.DataBind();
            //页码溢出跳转最后一页
            if (PageIndex != 1 && !(TotalCount > PageSize * (PageIndex - 1)))
                Search(TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1));
            //跳转地址
            string pageUrl = string.Format("BidList.aspx?pix=__id__&kw={0}", txtKeywords.Text.Trim());
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
            var url = string.Format("BidList.aspx?pix={0}&kw={1}", pageIndex, txtKeywords.Text.Trim());
            Response.Redirect(url);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //取消申办
            if (e.CommandName == "CancelBid")
            {
                if (new DTcms.BLL.Bid().UpdateField(Convert.ToInt32(e.CommandArgument), "Status=3"))
                {
                    var bidModel = new DTcms.BLL.View_Bid().GetModelList("ID=" + Convert.ToInt32(e.CommandArgument))[0];
                    var smsMsg = string.Empty;
                    var msgBLL = new DTcms.BLL.tx_message();
                    //用户申办提醒
                    var userSMS = new BLL.sms_template().GetModel("UserCancelBid"); //取得短信内容
                    //msgBLL.Send(bidModel.Tel, userSMS.content
                    //    .Replace("{Number}", bidModel.Number)
                    //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                    //    , 1, out smsMsg);
                    var msgParam = string.Format("\"{0}\",\"{1}\"",bidModel.Number, DateTime.Now.ToString("yyyy-MM-dd"));
                    msgBLL.Send(bidModel.Tel, userSMS.content, 1, msgParam, out smsMsg);


                    //公证员申办提醒
                    var JusticeConfigModel = DTcms.Common.SerializationHelper.Load<DTcms.Model.JusticeConfig>(DTcms.Common.DTKeys.BIDCONFIG_JUSTICE_PATH);
                    var manageSMS = new BLL.sms_template().GetModel("ManageCancelBid"); //取得短信内容
                    //msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content
                    //    .Replace("{CnName}", bidModel.CnName)
                    //    .Replace("{BidBusiness}", bidModel.BidBusiness)
                    //    .Replace("{Number}", bidModel.Number)
                    //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                    //    , 1, out smsMsg);
                    msgParam = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                        bidModel.CnName, bidModel.BidBusiness, bidModel.Number, DateTime.Now.ToString("yyyy-MM-dd"));
                    msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content, 1, msgParam, out smsMsg);
                    JscriptMsg("取消申办成功！", "Success");
                }
                else
                    JscriptMsg("取消申办失败！", "Error");
                BindData();
            }
        }
    }
}