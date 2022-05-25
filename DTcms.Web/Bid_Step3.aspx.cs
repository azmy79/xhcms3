using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class Bid_Step3 : UI.BasePage
    {
        /// <summary>
        /// 翻译语言
        /// </summary>
        public List<DTcms.Model.TRLanguage> TRLanguage { get; set; }

        /// <summary>
        /// 申办业务
        /// </summary>
        public List<DTcms.Model.BidBusiness> BidBusiness { get; set; }

        /// <summary>
        /// 申办数据
        /// </summary>
        public DTcms.Model.Bid Bid { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUserLogin())//未登录
                SkipLogin();
            var list = new DTcms.BLL.Bid().GetModelList("UserID=" + GetUserInfo().id + " and ID=" + DTcms.Common.DTRequest.GetQueryInt("id", 0));
            if (list.Count == 0)
                SkipIndex();//非法操作
            Bid = list[0];
            //申办信息
            TRLanguage = new DTcms.BLL.TRLanguage().GetModelList("ID in(select TRLanguageID from Bid_TRLanguage where Bid_TRLanguage.BidID=" + Bid.ID + ") order by Sort Desc");
            BidBusiness = new DTcms.BLL.BidBusiness().GetModelList("ID in(select BidBusinessID from Bid_BidBusiness where Bid_BidBusiness.BidID=" + Bid.ID + ") order by Sort Desc");
        }

        /// <summary>
        /// 支付按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTrue_Click(object sender, EventArgs e)
        {
            var userInfo = GetUserInfo();
            var order = new Model.orders();
            var ret = true;
            //生成订单
            if (ret)
            {
                order = new Model.orders
                {
                    order_no = DTcms.Common.Utils.GetOrderNumber().PadLeft(16, '0'),
                    user_id = userInfo.id,
                    user_name = userInfo.user_name,
                    payment_id = 0,//银联
                    express_id = 0,
                    accept_name = Bid.CnName,
                    post_code = string.Empty,
                    telphone = Bid.Tel,
                    mobile = string.Empty,
                    address = Bid.Address,
                    message = string.Empty,
                    payable_amount = Bid.Price,
                    real_amount = Bid.Price,
                    order_amount = Bid.Price,
                    express_status = 1,
                    express_fee = 0,
                    payment_status = 1,
                    payment_fee = 0,
                    point = 0,
                    add_time = DateTime.Now,
                    order_goods = new List<DTcms.Model.order_goods>(){new DTcms.Model.order_goods(){
                            goods_id = Bid.ID,
                            goods_price =Bid.Price,
                            goods_title = string.Empty,
                            point = 0,
                            quantity = 1,
                            real_price =Bid.Price
                        }}
                };
                var ordersID = new BLL.orders().Add(order);
                ret = ordersID > 0;
            }
            //支付
            if (ret)
            {
                ////把请求参数打包成数组
                //var sParaTemp = new SortedDictionary<string, string>
                //    {
                //        {"payment_type", "1"},
                //        {"show_url", config.weburl},
                //        {"out_trade_no", order.order_no},
                //        {"subject", order.accept_name+"的订单"},
                //        {"body", order.remark},
                //        {"total_fee", order.order_amount.ToString()},//Order.order_amount.ToString()
                //        {"paymethod", "directPay"},
                //        {"defaultbank", ""},
                //        {"anti_phishing_key", ""},
                //        {"exter_invoke_ip", DTcms.Common.DTRequest.GetIP()},
                //        {"buyer_email", ""},
                //        {"royalty_type", ""},
                //        {"royalty_parameters", ""},
                //        {"service", "create_direct_pay_by_user"},
                //        {"partner", DTcms.API.Payment.Alipay.Config.Partner},
                //        {"_input_charset", DTcms.API.Payment.Alipay.Config.Input_charset},
                //        {"seller_email", DTcms.API.Payment.Alipay.Config.Email},
                //        {"return_url", DTcms.API.Payment.Alipay.Config.Return_url},
                //        {"notify_url", DTcms.API.Payment.Alipay.Config.Notify_url}
                //    };
                ////增加基本配置
                ////构造表单提交HTML数据
                //Response.Write(DTcms.API.Payment.Alipay.Submit.BuildFormHtml(sParaTemp, DTcms.API.Payment.Alipay.Config.New_Gateway, "get", "确认"));

                //银联支付
                DTcms.API.Payment.Chinapay.ChinapayAct.PaySubmit(order.order_no, Convert.ToInt32(order.order_amount * 100));
            }

        }
    }
}