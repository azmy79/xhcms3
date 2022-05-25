using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace DTcms.API.Payment.Chinapay
{
    public class ChinapayConfig
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public static string ActionUrl = ConfigurationManager.AppSettings["Chinapay_Action"];

        /// <summary>
        /// 商户号
        /// </summary>
        public static string MerId = ConfigurationManager.AppSettings["Chinapay_MerId"];

        /// <summary>
        /// 公钥
        /// </summary>
        public static string PgPubk = ConfigurationManager.AppSettings["Chinapay_PgPubk"];

        /// <summary>
        /// 私钥
        /// </summary>
        public static string MerPrK = ConfigurationManager.AppSettings["Chinapay_MerPrK"];

        /// <summary>
        /// 后台交易接收URL地址
        /// </summary>
        public static string BgRetUrl = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + ConfigurationManager.AppSettings["Chinapay_BgRetUrl"];

        /// <summary>
        /// 页面交易接收URL地址
        /// </summary>
        public static string PageRetUrl = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + ConfigurationManager.AppSettings["Chinapay_PageRetUrl"];

    }
}
