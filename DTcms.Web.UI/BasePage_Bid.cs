using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.Web.UI
{
    /// <summary>
    /// 申办拓展
    /// </summary>
    public partial class BasePage
    {
        /// <summary>
        /// 输出申办信息表格
        /// </summary>
        /// <param name="Country">前往国家</param>
        /// <param name="CopyCount">副本数量</param>
        /// <param name="TRLanguage">翻译语言</param>
        /// <param name="BidBusiness">申办业务</param>
        /// <param name="totalPrice">输出总价格</param>
        /// <param name="CertificateStyle">证书样式(可选)</param>
        /// <returns></returns>
        public static string WriteBidTable(DTcms.Model.Country Country, int CopyCount, List<DTcms.Model.TRLanguage> TRLanguage, List<DTcms.Model.BidBusiness> BidBusiness, out decimal totalPrice, List<DTcms.Model.CertificateStyle> CertificateStyle = null)
        {
            var BidBusiness_TRLanguage = new DTcms.BLL.View_BidBusiness_TRLanguage().GetModelList("1=1");
            totalPrice = 0;//总价格
            //最终输出HTML字符串
            var htmlStr = new StringBuilder();
            for (int i = 0; i < BidBusiness.Count; i++)
            {
                //当前公证事项
                var item = BidBusiness[i];
                htmlStr.Append("<tr>");
                htmlStr.Append("    <td style=\"background: #f9f9f9\">");
                htmlStr.Append("        <label>" + item.Name + "</label></td>");
                htmlStr.Append("    <td>￥" + Convert.ToInt32(item.NotaryPrice) * TRLanguage.Count + "<br/>详细：￥" + Convert.ToInt32(item.NotaryPrice) + "*" + TRLanguage.Count + "</td>");
                decimal trPrice = 0;//翻译价格
                var trPriceStr = "<br/>详细：";
                for (int j = 0; j < TRLanguage.Count; j++)
                {
                    var price = BidBusiness_TRLanguage.Find(p => p.ID == item.ID && p.TRLanguageID == TRLanguage[j].ID).TRPrice;
                    trPrice += price;
                    trPriceStr += TRLanguage[j].Name + "￥" + Convert.ToInt32(price) + "+";
                }
                trPriceStr = trPriceStr.TrimEnd(new char[] { '+' });
                htmlStr.Append("    <td>￥" + Convert.ToInt32(trPrice) + trPriceStr + "</td>");
                htmlStr.Append("    <td>￥" + Convert.ToInt32(item.CopyPrice * (CopyCount - 1)) + "</td>");
                if (CertificateStyle != null)
                {
                    var styleModel = CertificateStyle.Find(p => p.BidBusinessID == item.ID);
                    htmlStr.Append("    <td>" + (styleModel != null ? styleModel.Title + "<strong style=\"cursor: pointer;\" tagsrc=\"" + styleModel.ImgUrl + "\" class=\"tc\">【预览】</strong>" : string.Empty) + "</td>");
                }
                htmlStr.Append("</tr>");
                totalPrice += item.NotaryPrice * TRLanguage.Count + trPrice + item.CopyPrice * (CopyCount - 1);
                //译源相符项
                if (Country.IsTS)
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("    <td style=\"background: #f9f9f9\">");
                    htmlStr.Append("        <label>译源相符</label></td>");
                    htmlStr.Append("    <td>￥" + 80 * TRLanguage.Count + "<br/>详细：￥80*" + TRLanguage.Count + "</td>");
                    htmlStr.Append("    <td>￥" + Convert.ToInt32(trPrice) + trPriceStr + "</td>");
                    htmlStr.Append("    <td>￥" + Convert.ToInt32(item.CopyPrice * (CopyCount - 1)) + "</td>");
                    if (CertificateStyle != null)
                        htmlStr.Append("    <td></td>");
                    htmlStr.Append("</tr>");
                    totalPrice += 80 * TRLanguage.Count + trPrice + item.CopyPrice * (CopyCount - 1);
                }
            }
            return htmlStr.ToString();
        }

        /// <summary>
        /// 输出申办信息表格
        /// </summary>
        /// <param name="Country">前往国家</param>
        /// <param name="CopyCount">副本数量</param>
        /// <param name="TRLanguage">翻译语言</param>
        /// <param name="BidBusiness">申办业务</param>
        /// <returns></returns>
        public static string WriteBidTable(DTcms.Model.Country Country, int CopyCount, List<DTcms.Model.TRLanguage> TRLanguage, List<DTcms.Model.BidBusiness> BidBusiness)
        {
            decimal totalPrice = 0;//总价格
            return WriteBidTable(Country, CopyCount, TRLanguage, BidBusiness, out totalPrice);
        }
    }
}
