using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTcms.Web.Ashx
{
    /// <summary>
    /// UserBid 的摘要说明
    /// </summary>
    public class UserBid : DTcms.Web.UI.BasePage_Ajax
    {

        public DTcms.Model.users userInfo { get; set; }

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //检查用户是否登录
            userInfo = GetUserInfo();
            if (userInfo == null)
            {
                context.Response.Write(ReturnMsg("对不起，用户尚未登录或已超时！", false));
                return;
            }
            //操作类型
            switch (DTcms.Common.DTRequest.GetString("option"))
            {
                //获取申办订单信息
                case "GetBidOrder":
                    var pageSize = int.Parse(context.Request.QueryString["PageSize"]);//页大小
                    var pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);//页索引
                    //查询条件
                    string strWhere = "PaymentStatus=2 and OrderStatus=2 and UserID=" + userInfo.id;
                    var bll = new DTcms.BLL.View_BidOrder();
                    //总数
                    var totalCount = 0;
                    //结果集
                    var retList = bll.GetModelList(pageSize, pageIndex + 1, strWhere, "AddTime desc", out totalCount);
                    //输出结果集
                    context.Response.Write(jsSerializer.Serialize(new
                    {
                        totalCount = totalCount,
                        list = retList
                    }));
                    break;
                //获取申办信息
                case "GetBid":
                    GetBid(context);
                    break;
                //获取证件样式
                case "GetCertificateStyle":
                    var bbsid = DTcms.Common.DTRequest.GetInt("bbsid", 0);
                    context.Response.Write(jsSerializer.Serialize(new DTcms.BLL.CertificateStyle().GetModelList("BidBusinessID=" + bbsid)));
                    break;
                //取消申办
                case "CancelBid":
                    ResponseWrite(p =>
                    {
                        p.status = false;//默认操作失败
                        var id = DTcms.Common.DTRequest.GetInt("id", 0);
                        var bidBLL = new DTcms.BLL.Bid();
                        var bidModel = bidBLL.GetModel(id);
                        if (bidModel == null || bidModel.UserID != userInfo.id)
                        {
                            p.msg = "数据异常，请重试";
                            return;
                        }
                        p.status = bidBLL.UpdateField(id, "Status=3");
                        if (!p.status) p.msg = "操作失败";
                        else
                        {
                            var bidModelInfo = new DTcms.BLL.View_Bid().GetModelList("ID=" + bidModel.ID)[0];
                            var smsMsg = string.Empty;
                            var msgBLL = new DTcms.BLL.tx_message();
                            //用户申办提醒
                            var userSMS = new BLL.sms_template().GetModel("UserCancelBid"); //取得短信内容
                            //msgBLL.Send(bidModelInfo.Tel, userSMS.content
                            //    .Replace("{Number}", bidModelInfo.Number)
                            //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                            //    , 1, out smsMsg);
                            var msgParam = string.Format("\"{0}\",\"{1}\"",bidModelInfo.Number, DateTime.Now.ToString("yyyy-MM-dd"));
                            msgBLL.Send(bidModelInfo.Tel, userSMS.content, 1, msgParam, out smsMsg);

                            //公证员申办提醒
                            var JusticeConfigModel = DTcms.Common.SerializationHelper.Load<DTcms.Model.JusticeConfig>(DTcms.Common.DTKeys.BIDCONFIG_JUSTICE_PATH);
                            var manageSMS = new BLL.sms_template().GetModel("ManageCancelBid"); //取得短信内容
                            //msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content
                            //    .Replace("{CnName}", bidModelInfo.CnName)
                            //    .Replace("{BidBusiness}", bidModelInfo.BidBusiness)
                            //    .Replace("{Number}", bidModelInfo.Number)
                            //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                            //    , 1, out smsMsg);
                            msgParam = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                    bidModelInfo.CnName, bidModelInfo.BidBusiness, bidModelInfo.Number, DateTime.Now.ToString("yyyy-MM-dd"));
                            msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content, 1, msgParam, out smsMsg);
                        }
                    });
                    break;
            }
        }

        /// <summary>
        /// 获取申办信息
        /// </summary>
        /// <param name="context"></param>
        public void GetBid(HttpContext context)
        {
            var pageSize = int.Parse(context.Request.QueryString["PageSize"]);//页大小
            var pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);//页索引
            //查询条件
            //未支付
            string strWhere = "Status<>2 and UserID=" + userInfo.id;
            var bll = new DTcms.BLL.View_Bid();
            //总数
            var totalCount = 0;
            //结果集
            var retList = bll.GetModelList(pageSize, pageIndex + 1, strWhere, "AddTime desc", out totalCount);
            //输出结果集
            context.Response.Write(jsSerializer.Serialize(new
            {
                totalCount = totalCount,
                list = retList
            }));
        }
    }
}