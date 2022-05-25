using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class Bid_Step2 : UI.BasePage
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
        /// 证书样式
        /// </summary>
        public List<DTcms.Model.CertificateStyle> CertificateStyle { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public List<DTcms.Model.Purpose> Purpose { get; set; }

        /// <summary>
        /// 前往国家
        /// </summary>
        public List<DTcms.Model.Country> Country { get; set; }

        /// <summary>
        /// 申办数据
        /// </summary>
        public Dictionary<string, object> BidData { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public DTcms.Model.users UserInfo { get; set; }

        /// <summary>
        /// 用户拓展信息
        /// </summary>
        public DTcms.Model.UserExt UserExt { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUserLogin())//未登录
                SkipLogin();
            var BidDataJSON = DTcms.Common.Utils.GetCookie("BidData");
            if (string.IsNullOrEmpty(BidDataJSON))//无数据
                Response.Redirect("Bid_Step1.aspx?cid=58");
            if (!IsPostBack)
            {
                UserInfo = GetUserInfo();
                UserExt = new BLL.UserExt().GetModel(UserInfo.id);
                BidData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(BidDataJSON);
                TRLanguage = new DTcms.BLL.TRLanguage().GetModelList("1=1 order by Sort Desc");
                BidBusiness = new DTcms.BLL.BidBusiness().GetModelList("1=1 order by Sort Desc");
                CertificateStyle = new DTcms.BLL.CertificateStyle().GetModelList("1=1 order by Sort Desc");
                Purpose = new DTcms.BLL.Purpose().GetModelList("1=1 order by Sort Desc");
                Country = new DTcms.BLL.Country().GetModelList("1=1 order by Sort Desc");
            }
        }
    }
}