using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class Bid_Step1 : UI.BasePage
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
        /// 用途
        /// </summary>
        public List<DTcms.Model.Purpose> Purpose { get; set; }

        /// <summary>
        /// 前往国家
        /// </summary>
        public List<DTcms.Model.Country> Country { get; set; }

        /// <summary>
        /// 申办业务翻译价格
        /// </summary>
        public List<DTcms.Model.View_BidBusiness_TRLanguage> BidBusiness_TRLanguage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUserLogin())
                SkipLogin();
            if (!IsPostBack)
            {
                TRLanguage = new DTcms.BLL.TRLanguage().GetModelList("1=1 order by Sort Desc");
                BidBusiness = new DTcms.BLL.BidBusiness().GetModelList("1=1 order by Sort Desc");
                Purpose = new DTcms.BLL.Purpose().GetModelList("1=1 order by Sort Desc");
                Country = new DTcms.BLL.Country().GetModelList("1=1 order by Sort Desc");
                BidBusiness_TRLanguage = new DTcms.BLL.View_BidBusiness_TRLanguage().GetModelList("1=1");
                //修改操作
                if (DTcms.Common.DTRequest.GetString("op") == "edit")
                {
                    var list = new DTcms.BLL.Bid().GetModelList("UserID=" + GetUserInfo().id + " and ID=" + DTcms.Common.DTRequest.GetQueryInt("id", 0));
                    if (list.Count == 0)
                        SkipIndex();//非法操作
                    var bidModel = list[0];
                    var tRLanguage = new DTcms.BLL.TRLanguage().GetModelList("ID in(select TRLanguageID from Bid_TRLanguage where Bid_TRLanguage.BidID=" + bidModel.ID + ") order by Sort Desc");
                    var bid_BidBusiness = new DTcms.BLL.Bid_BidBusiness().GetModelList("BidID=" + bidModel.ID);
                    var document = new DTcms.BLL.View_Document().GetModelList("BidID=" + bidModel.ID + " order by Sort Desc");
                    var bidSourceFile = new DTcms.BLL.BidSourceFile().GetModelList("BidID=" + bidModel.ID);
                    //重写COOKIE
                    DTcms.Common.Utils.WriteCookie("BidData", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new
                    {
                        Address = bidModel.Address,
                        BidBusiness = bid_BidBusiness.Select((p, i) => new
                        {
                            BidBusiness = p.BidBusinessID,
                            CertificateStyle = p.CertificateStyleID
                        }).ToArray(),
                        CnName = bidModel.CnName,
                        CopyCount = bidModel.CopyCount,
                        Country = bidModel.CountryID,
                        EnName = bidModel.EnName,
                        Purpose = bidModel.PurposeID,
                        Sex = bidModel.Sex ? "男" : "女",
                        TRLanguage = tRLanguage.Select((p, i) => p.ID).ToArray(),
                        Tel = bidModel.Tel,
                        Document = document.Select((p, i) => new
                        {
                            DocumentTypeID = p.DocumentTypeID,
                            Path = p.Path
                        }).ToArray(),
                        Birthday = bidModel.Birthday.ToString("yyyy-MM-dd"),
                        CartType = bidModel.CartType,
                        CartNum = bidModel.CartNum,
                        BidSourceFile = bidSourceFile.Count > 0 ? new
                        {
                            Path = bidSourceFile[0].Path,
                            NeedTranslation = bidSourceFile[0].NeedTranslation.ToString()
                        } : null
                    }));
                }
                else
                    //移除COOKIE
                    DTcms.Common.Utils.WriteCookie("BidData", null);
            }
        }
    }
}