using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.DAL
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
            var ret = false;
            try
            {
                var sqlStr = "delete BidBusiness_TRLanguage where BidBusinessID=" + bidBusinessID;
                for (int i = 0; i < trLanguageIDs.Length; i++)
                {
                    sqlStr += " insert into  BidBusiness_TRLanguage(BidBusinessID,TRLanguageID,Price) values(" + bidBusinessID + "," + trLanguageIDs[i] + "," + trLanguagePrices[i] + ") ";
                }
                DTcms.DBUtility.DbHelperSQL.ExecuteSql(sqlStr);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }


        /// <summary>
        /// 绑定证件类型
        /// </summary>
        /// <param name="bidBusinessID">申办业务ID</param>
        /// <param name="documentTypeIDs">证件类型ID数组</param>
        /// <returns></returns>
        public bool BindDocumentType(int bidBusinessID, string[] documentTypeIDs)
        {
            var ret = false;
            try
            {

                var sqlStr = "delete BidBusiness_DocumentType where BidBusinessID=" + bidBusinessID;
                for (int i = 0; i < documentTypeIDs.Length; i++)
                {
                    sqlStr += " insert into  BidBusiness_DocumentType(BidBusinessID,DocumentTypeID) values(" + bidBusinessID + "," + documentTypeIDs[i] + ") ";
                }
                DTcms.DBUtility.DbHelperSQL.ExecuteSql(sqlStr);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
    }
}
