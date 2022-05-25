﻿using DTcms.API.Payment.Chinapay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.api.chinapay
{
    public partial class PageRetUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //接受请求参数
            var merid = Request["merid"].Trim();//商户号
            var orderno = Request["orderno"].Trim();//订单号
            var transdate = Request["transdate"].Trim();//交易时间
            var amount = Request["amount"].Trim();//金额
            var currencycode = Request["currencycode"].Trim();//交易币种
            var transtype = Request["transtype"].Trim();//交易类型
            var status = Request["status"].Trim();//订单状态
            var checkvalue = Request["checkvalue"].Trim();//校验值
            var GateId = Request["GateId"].Trim();//支付网关号
            var Priv1 = Request["Priv1"].Trim();//商户私有域
            //支付插件实例
            var netPay = new NetPay();
            //创建公钥
            if (!netPay.buildKey("999999999999999", 0, ChinapayConfig.PgPubk))
            {
                Response.Write("秘钥错误");
                return;
            }
            //签名认证
            if (netPay.verifyTransResponse(merid, orderno, amount, currencycode, transdate, transtype, status, checkvalue))
            {
                if (status == "1001")//交易成功
                {
                    var bll = new BLL.orders();
                    var model = bll.GetModel(orderno);
                    //已付款
                    if (model.payment_status == 2)
                    {
                        //跳转支付成功页
                        Response.Redirect("/PaySuccess.aspx");
                        return;
                    }
                    //金额相符
                    if (Convert.ToInt32(model.order_amount * 100).ToString().PadLeft(12, '0') == amount)
                    {
                        //支付成功
                        new DTcms.BLL.Bid_Custom().BidPaySuccess(orderno);
                        //跳转支付成功页
                        Response.Redirect("/PaySuccess.aspx");
                        return;
                    }
                    else
                    {
                        Response.Write("支付金额与订单金额不相符");
                    }
                }
            }
            else
            {
                Response.Write("签名认证失败");
            }
        }
    }
}