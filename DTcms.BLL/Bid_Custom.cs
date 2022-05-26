using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.BLL
{
    public class Bid_Custom
    {
        /// <summary>
        /// 绑定申办信息-翻译语言
        /// </summary>
        /// <param name="BidID">申办信息ID</param>
        /// <param name="TRLanguageIDs">翻译语言ID数组</param>
        /// <returns></returns>
        public bool BindBid_TRLanguage(int BidID, string[] TRLanguageIDs)
        {
            return new DTcms.DAL.Bid_Custom().BindBid_TRLanguage(BidID, TRLanguageIDs);
        }

        /// <summary>
        /// 绑定申办信息-申办业务
        /// </summary>
        /// <param name="BidID">申办信息ID</param>
        /// <param name="Bid_BidBusiness">申办信息-申办业务集合</param>
        /// <returns></returns>
        public bool BindBid_BidBusiness(int BidID, List<DTcms.Model.Bid_BidBusiness> Bid_BidBusiness)
        {
            return new DTcms.DAL.Bid_Custom().BindBid_BidBusiness(BidID, Bid_BidBusiness);
        }


        /// <summary>
        /// 绑定申办信息-证件信息
        /// </summary>
        /// <param name="BidID">申办信息ID</param>
        /// <param name="DocumentList">证件信息集合</param>
        /// <returns></returns>
        public bool BindBid_Document(int BidID, List<DTcms.Model.Document> DocumentList)
        {
            return new DTcms.DAL.Bid_Custom().BindBid_Document(BidID, DocumentList);
        }

        /// <summary>
        /// 申办支付成功
        /// </summary>
        /// <param name="orderNo">订单号</param>
        public void BidPaySuccess(string orderNo)
        {
            #region 修改订单状态
            var bll = new BLL.orders();
            //修改订单状态
            bll.UpdateField(orderNo, "trade_no='" + orderNo + "',status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
            var model = bll.GetModel(orderNo);
            var bidid = model.order_goods[0].goods_id;
            //修改申办信息状态
            new DTcms.BLL.Bid().UpdateField(bidid, "status=2");
            #endregion

            #region 发送短信
            var bidModel = new DTcms.BLL.View_Bid().GetModelList("ID=" + bidid)[0];
            var smsMsg = string.Empty;
            var msgBLL = new DTcms.BLL.ali_message();
            //用户付款提醒
            var userSMS = new BLL.sms_template().GetModel("UserPay"); //取得短信内容
            var msgParam = "{" + string.Format("\"OrderNo\":\"{0}\",\"SendTime\":\"{1}\"",orderNo, DateTime.Now.ToString("yyyy-MM-dd")) + "}";

            //msgBLL.Send(bidModel.Tel, userSMS.content
            //    .Replace("{OrderNo}", orderNo)
            //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
            //    , 1, out smsMsg);
            msgBLL.Send(bidModel.Tel, userSMS.content, 1, msgParam, out smsMsg);

            //	公证员付款提醒
            var JusticeConfigModel = DTcms.Common.SerializationHelper.Load<DTcms.Model.JusticeConfig>(DTcms.Common.DTKeys.BIDCONFIG_JUSTICE_PATH);
            var manageSMS = new BLL.sms_template().GetModel("ManagePay"); //取得短信内容
            //msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content
            //    .Replace("{CnName}", bidModel.CnName)
            //    .Replace("{BidBusiness}", bidModel.BidBusiness)
            //    .Replace("{OrderNo}", orderNo)
            //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
            //    , 1, out smsMsg);
            msgParam = "{" + string.Format("\"CnName\":\"{0}\",\"BidBusiness\":\"{1}\",\"OrderNo\":\"{2}\",\"SendTime\":\"{3}\"",
                bidModel.CnName, bidModel.BidBusiness, orderNo, DateTime.Now.ToString("yyyy-MM-dd")) + "}";
            msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content, 1, msgParam, out smsMsg);

            #endregion
        }
    }
}
