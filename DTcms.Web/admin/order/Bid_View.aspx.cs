using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.order
{
    public partial class Bid_View : Web.UI.ManagePage
    {
        /// <summary>
        /// 申办信息对象
        /// </summary>
        public DTcms.Model.View_Bid BidModel { get; set; }

        /// <summary>
        /// 订单对象
        /// </summary>
        public DTcms.Model.orders OrderModel { get; set; }

        /// <summary>
        /// 翻译语言
        /// </summary>
        public List<DTcms.Model.TRLanguage> TRLanguage { get; set; }

        /// <summary>
        /// 申办业务
        /// </summary>
        public List<DTcms.Model.BidBusiness> BidBusiness { get; set; }

        /// <summary>
        /// 证件
        /// </summary>
        public List<DTcms.Model.View_Document> Document { get; set; }

        /// <summary>
        /// 所需公证文本
        /// </summary>
        public List<DTcms.Model.BidSourceFile> BidSourceFile { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var ID = DTcms.Common.DTRequest.GetQueryInt("id");
            if (ID == 0)
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            //订单信息
            OrderModel = new DTcms.BLL.orders().GetModel(ID);
            //申办信息
            BidModel = new DTcms.BLL.View_Bid().GetModelList("ID=" + OrderModel.order_goods[0].goods_id)[0];
            TRLanguage = new DTcms.BLL.TRLanguage().GetModelList("ID in(select TRLanguageID from Bid_TRLanguage where Bid_TRLanguage.BidID=" + BidModel.ID + ") order by Sort Desc");
            BidBusiness = new DTcms.BLL.BidBusiness().GetModelList("ID in(select BidBusinessID from Bid_BidBusiness where Bid_BidBusiness.BidID=" + BidModel.ID + ") order by Sort Desc");
            Document = new DTcms.BLL.View_Document().GetModelList("BidID=" + BidModel.ID + " order by Sort Desc");
            BidSourceFile = new DTcms.BLL.BidSourceFile().GetModelList("BidID=" + BidModel.ID);
        }
    }
}