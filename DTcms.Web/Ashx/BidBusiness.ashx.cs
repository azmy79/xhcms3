using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTcms.Web.Ashx
{
    /// <summary>
    /// BidBusiness 的摘要说明
    /// </summary>
    public class BidBusiness : DTcms.Web.UI.BasePage_Ajax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //检查管理员是否登录
            if (!new DTcms.Web.UI.ManagePage().IsAdminLogin())
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"尚未登录或已超时，请登录后操作！\"}");
                return;
            }
            var option = DTcms.Common.DTRequest.GetString("option");
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            switch (option)
            {
                case "GetTRLanguage":
                    var id = DTcms.Common.DTRequest.GetInt("ID", 0);
                    context.Response.Write(js.Serialize(new DTcms.BLL.View_BidBusiness_TRLanguage().GetModelList("ID=" + id + " order by Sort")));
                    break;
                case "BindTRLanguage":
                    id = DTcms.Common.DTRequest.GetInt("ID", 0);
                    var ids = string.IsNullOrEmpty(DTcms.Common.DTRequest.GetString("hidID")) ? new string[] { } : DTcms.Common.DTRequest.GetString("hidID").Split(',');
                    var trPrices = string.IsNullOrEmpty(DTcms.Common.DTRequest.GetString("TRPrice")) ? new string[] { } : DTcms.Common.DTRequest.GetString("TRPrice").Split(',');
                    context.Response.Write(ReturnMsg("操作失败", new DTcms.BLL.BidBusiness_Custom().BindTRLanguagePrice(id, ids, trPrices)));
                    break;
                case "GetDocumentType":
                    id = DTcms.Common.DTRequest.GetInt("ID", 0);
                    context.Response.Write(js.Serialize(new
                    {
                        List = new DTcms.BLL.DocumentType().GetModelList("1=1 order by Sort"),
                        BindList = new DTcms.BLL.BidBusiness_DocumentType().GetModelList("BidBusinessID=" + id)
                    }));
                    break;
                case "BindDocumentType":
                    id = DTcms.Common.DTRequest.GetInt("ID", 0);
                    ids = string.IsNullOrEmpty(DTcms.Common.DTRequest.GetString("cbkDocumentType")) ? new string[] { } : DTcms.Common.DTRequest.GetString("cbkDocumentType").Split(',');
                    context.Response.Write(ReturnMsg("操作失败", new DTcms.BLL.BidBusiness_Custom().BindDocumentType(id, ids)));
                    break;
            }
        }
    }
}