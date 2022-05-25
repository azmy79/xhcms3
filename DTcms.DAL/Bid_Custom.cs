using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.DAL
{
    public class Bid_Custom
    {

        /// <summary>
        /// 绑定申办信息-翻译语言
        /// </summary>
        /// <param name="BidID">申办信息ID</param>
        /// <param name="TRLanguageIDs">翻译语言ID数组</param>
        /// <returns></returns>
        public bool BindBid_TRLanguage(int BidID, string[] TRLanguageIDs)
        {
            var ret = false;
            try
            {

                var sqlStr = "delete Bid_TRLanguage where BidID=" + BidID;
                for (int i = 0; i < TRLanguageIDs.Length; i++)
                {
                    sqlStr += " insert into  Bid_TRLanguage(BidID,TRLanguageID) values(" + BidID + "," + TRLanguageIDs[i] + ") ";
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
        /// 绑定申办信息-申办业务
        /// </summary>
        /// <param name="BidID">申办信息ID</param>
        /// <param name="Bid_BidBusiness">申办信息-申办业务集合</param>
        /// <returns></returns>
        public bool BindBid_BidBusiness(int BidID, List<DTcms.Model.Bid_BidBusiness> Bid_BidBusiness)
        {
            var ret = false;
            try
            {

                var sqlStr = "delete Bid_BidBusiness where BidID=" + BidID;
                for (int i = 0; i < Bid_BidBusiness.Count; i++)
                {
                    sqlStr += " insert into  Bid_BidBusiness(BidID,BidBusinessID,CertificateStyleID) values(" + Bid_BidBusiness[i].BidID + "," + Bid_BidBusiness[i].BidBusinessID + "," + Bid_BidBusiness[i].CertificateStyleID + ") ";
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
        /// 绑定申办信息-证件信息
        /// </summary>
        /// <param name="BidID">申办信息ID</param>
        /// <param name="DocumentList">证件信息集合</param>
        /// <returns></returns>
        public bool BindBid_Document(int BidID, List<DTcms.Model.Document> DocumentList)
        {
            var ret = false;
            try
            {

                var sqlStr = "delete Document where BidID=" + BidID;
                DocumentList.ForEach(p =>
                {
                    sqlStr += " insert into  Document(BidID,DocumentTypeID,Path,AddTime) values(" + BidID + "," + p.DocumentTypeID + ",'" + p.Path + "',getdate()) ";
                });
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
