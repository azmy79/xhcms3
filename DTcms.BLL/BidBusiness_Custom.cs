using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.BLL
{
    public class BidBusiness_Custom
    {
        /// <summary>
        /// 绑定申办业务翻译价格
        /// </summary>
        /// <param name="bidBusinessID">申办业务ID</param>
        /// <param name="trLanguageIDs">翻译语言ID数组</param>
        /// <param name="trLanguagePrices">翻译价格数组</param>
        /// <returns></returns>
        public bool BindTRLanguagePrice(int bidBusinessID, string[] trLanguageIDs, string[] trLanguagePrices)
        {
            return new DTcms.DAL.BidBusiness_Custom().BindTRLanguagePrice(bidBusinessID, trLanguageIDs, trLanguagePrices);
        }

        /// <summary>
        /// 绑定证件类型
        /// </summary>
        /// <param name="bidBusinessID">申办业务ID</param>
        /// <param name="documentTypeIDs">证件类型ID数组</param>
        /// <returns></returns>
        public bool BindDocumentType(int bidBusinessID, string[] documentTypeIDs)
        {
            return new DTcms.DAL.BidBusiness_Custom().BindDocumentType(bidBusinessID, documentTypeIDs);
        }
    }
}
