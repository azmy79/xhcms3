using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DTcms.API.Payment.Chinapay
{
    public class ChinapayAct
    {

        /// <summary>
        /// 提交支付表单
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <param name="transAmt">订单金额/分</param>
        /// <param name="priv1">商户私有域，长度不要超过60 个字节</param>
        /// <param name="gateId">支付网关号</param>
        public static void PaySubmit(string orderID, int transAmt, string priv1 = "", string gateId = "")
        {
            //支付插件实例
            var netPay = new NetPay();
            if (!netPay.buildKey(ChinapayConfig.MerId, 0, ChinapayConfig.MerPrK)) return;//创建私钥
            #region 支付参数
            //这里action 的内容为提交交易数据的URL 地址
            var action = ChinapayConfig.ActionUrl;
            //MerId 为ChinaPay 统一分配给商户的商户号，15 位长度，必填
            var MerId = ChinapayConfig.MerId;
            //商户提交给ChinaPay 的交易订单号，16 位长度，必填
            var OrdId = orderID;
            //订单交易金额，12 位长度，左补0，必填,单位为分
            var TransAmt = transAmt.ToString().PadLeft(12, '0');
            //订单交易币种，3 位长度，固定为人民币156，必填
            var CuryId = "156";
            //订单交易日期，8 位长度，必填
            var TransDate = DateTime.Now.ToString("yyyyMMdd");
            //交易类型，4 位长度，必填
            var TransType = "0001";
            //支付接入版本号，必填
            var Version = "20141120";
            //后台交易接收URL，长度不要超过80 个字节，必填
            var BgRetUrl = ChinapayConfig.BgRetUrl;
            //页面交易接收URL，长度不要超过80 个字节，必填
            var PageRetUrl = ChinapayConfig.PageRetUrl;
            //支付网关号，可选
            var GateId = gateId;
            //商户私有域，长度不要超过60 个字节
            var Priv1 = priv1;
            //256 字节长的ASCII 码,为此次交易提交关键数据的数字签名，必填
            var ChkValue = netPay.Sign(MerId + OrdId + TransAmt + CuryId + TransDate + TransType + Version + BgRetUrl + PageRetUrl + GateId + Priv1);
            #endregion
            //输出表单并提交
            HttpContext.Current.Response.Write("<form name=\"chinapayForm\" id=\"chinapayForm\" action=\"" + action + "\" METHOD=POST>");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"MerId\" value=\"" + MerId + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"OrdId\" value=\"" + OrdId + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"TransAmt\" value=\"" + TransAmt + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"CuryId\" value=\"" + CuryId + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"TransDate\" value=\"" + TransDate + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"TransType\" value=\"" + TransType + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"Version\" value=\"" + Version + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"BgRetUrl\" value=\"" + BgRetUrl + "\"/> ");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"PageRetUrl\" value=\"" + PageRetUrl + "\">");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"GateId\" value=\"" + GateId + "\">");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"Priv1\" value=\"" + Priv1 + "\">");
            HttpContext.Current.Response.Write("    <input type=\"hidden\" name=\"ChkValue\" value=\"" + ChkValue + "\">");
            HttpContext.Current.Response.Write("</form> ");
            HttpContext.Current.Response.Write("<script>document.forms['chinapayForm'].submit();</script>");
        }
    }
}
