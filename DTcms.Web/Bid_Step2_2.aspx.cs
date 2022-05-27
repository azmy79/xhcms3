using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class Bid_Step2_2 : UI.BasePage
    {
        /// <summary>
        /// 证件类型
        /// </summary>
        public List<DTcms.Model.DocumentType> DocumentType { get; set; }

        /// <summary>
        /// 申办数据
        /// </summary>
        public Dictionary<string, object> BidData { get; set; }

        /// <summary>
        /// 翻译语言
        /// </summary>
        public List<DTcms.Model.TRLanguage> TRLanguage { get; set; }

        /// <summary>
        /// 申办业务
        /// </summary>
        public List<DTcms.Model.BidBusiness> BidBusiness { get; set; }

        /// <summary>
        /// 申办业务翻译价格
        /// </summary>
        public List<DTcms.Model.View_BidBusiness_TRLanguage> BidBusiness_TRLanguage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUserLogin())//未登录
                SkipLogin();
            var BidDataJSON = DTcms.Common.Utils.GetCookie("BidData");
            if (string.IsNullOrEmpty(BidDataJSON))//无数据
                Response.Redirect("Bid_Step1.aspx?cid=58");
            var BidData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(BidDataJSON);
            var bs = BidData["BidBusiness"] as System.Collections.ArrayList;
            var bsids = string.Empty;
            for (int i = 0; i < bs.Count; i++)
            {
                bsids += ((Dictionary<string, object>)bs[i])["BidBusiness"].ToString() + ",";
            }
            bsids = bsids.Length > 0 ? bsids.TrimEnd(',') : bsids;
            DocumentType = new DTcms.BLL.DocumentType().GetModelList("ID in(select DocumentTypeID from BidBusiness_DocumentType where BidBusinessID in(" + bsids + ")) order by Sort Desc");
            TRLanguage = new DTcms.BLL.TRLanguage().GetModelList("1=1 order by Sort Desc");
            BidBusiness = new DTcms.BLL.BidBusiness().GetModelList("1=1 order by Sort Desc");
            BidBusiness_TRLanguage = new DTcms.BLL.View_BidBusiness_TRLanguage().GetModelList("1=1");
        }

        /// <summary>
        /// 提交申办信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTrue_Click(object sender, EventArgs e)
        {
            var BidDataJSON = DTcms.Common.Utils.GetCookie("BidData");
            if (string.IsNullOrEmpty(BidDataJSON))//无数据
                Response.Redirect("Bid_Step1.aspx?cid=58");
            BidData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(BidDataJSON);
            var userInfo = GetUserInfo();
            var userExt = new DTcms.BLL.UserExt().GetModel(userInfo.id);
            //操作结果
            var ret = true;
            #region 计算价格
            var copyCount = Convert.ToInt32(BidData["CopyCount"]);//副本数量
            var bs = BidData["BidBusiness"] as System.Collections.ArrayList;//申办业务
            var tr = BidData["TRLanguage"] as System.Collections.ArrayList;//翻译语言
            var country = new DTcms.BLL.Country().GetModel(Convert.ToInt32(BidData["Country"]));//前往国家
            decimal totalPrice = 0;//总价格
            for (int i = 0; i < bs.Count; i++)
            {
                var bsDic = (Dictionary<string, object>)bs[i];
                var id = Convert.ToInt32(bsDic["BidBusiness"]);
                var item = BidBusiness.Find(p => p.ID == id);
                decimal trPrice = 0;//翻译价格
                for (int j = 0; j < tr.Count; j++)
                {
                    trPrice += BidBusiness_TRLanguage.Find(p => p.ID == id && p.TRLanguageID == Convert.ToInt32(tr[j])).TRPrice;
                }
                totalPrice += item.NotaryPrice * tr.Count + trPrice + item.CopyPrice * (copyCount - 1);
                //译源相符项
                if (country.IsTS) totalPrice += 80 * tr.Count + trPrice + item.CopyPrice * (copyCount - 1);
            }
            #endregion
            //申办业务处理实例
            var bidBLL = new DTcms.BLL.Bid();
            //申办对象
            var bidModel = new DTcms.Model.Bid();
            //是否执行修改操作
            var isEdit = false;
            //修改操作
            if (DTcms.Common.DTRequest.GetString("op") == "edit")
            {
                bidModel = bidBLL.GetModel(DTcms.Common.DTRequest.GetInt("id", 0));
                isEdit = true;
            }
            //申办信息
            bidModel.Address = BidData["Address"].ToString();
            bidModel.AddTime = DateTime.Now;
            bidModel.CnName = BidData["CnName"].ToString();
            bidModel.CountryID = Convert.ToInt32(BidData["Country"]);
            bidModel.EnName = BidData["EnName"].ToString();
            bidModel.Number = DTcms.Common.Utils.Number(8);
            bidModel.Price = totalPrice;
            bidModel.PurposeID = Convert.ToInt32(BidData["Purpose"]);
            bidModel.Sex = BidData["Sex"].ToString() == "男" ? true : false;
            bidModel.Tel = BidData["Tel"].ToString();
            bidModel.UserID = userInfo.id;
            bidModel.CopyCount = copyCount;
            bidModel.Status = 0;//默认审核中
            bidModel.Birthday = DateTime.Parse(BidData["Birthday"].ToString());
            bidModel.CartType = Convert.ToInt32(BidData["CartType"]);
            bidModel.CartNum = BidData["CartNum"].ToString();
            //保存申办信息
            if (isEdit)
                ret = bidBLL.Update(bidModel);//修改
            else
                bidModel.ID = new DTcms.BLL.Bid().Add(bidModel);//新增
            ret = bidModel.ID > 0;
            var bid_customBLL = new DTcms.BLL.Bid_Custom();
            //翻译语言
            if (ret)
            {
                ret = bid_customBLL.BindBid_TRLanguage(bidModel.ID, tr.ToArray(typeof(string)) as string[]);
            }
            //申办业务
            if (ret)
            {
                ret = bid_customBLL.BindBid_BidBusiness(bidModel.ID, bs.ToArray().Select((p, i) =>
                {
                    var dic = p as Dictionary<string, object>;
                    return new DTcms.Model.Bid_BidBusiness
                    {
                        BidBusinessID = Convert.ToInt32(dic["BidBusiness"]),
                        BidID = bidModel.ID,
                        CertificateStyleID = Convert.ToInt32(dic["CertificateStyle"])
                    };
                }).ToList());
            }
            //证件
            if (ret)
            {
                var docArr = BidData["Document"] as System.Collections.ArrayList;
                //证件集合
                var documentList = new List<DTcms.Model.Document>();
                foreach (var item in docArr)
                {
                    var dicItem = item as Dictionary<string, object>;
                    documentList.Add(new DTcms.Model.Document
                    {
                        DocumentTypeID = Convert.ToInt32(dicItem["DocumentTypeID"]),
                        Path = dicItem["Path"].ToString()
                    });
                }
                ret = bid_customBLL.BindBid_Document(bidModel.ID, documentList);
            }
            //公证文本
            if (ret)
            {
                if (BidData["BidSourceFile"] != null)
                {
                    var BidSourceFileDic = BidData["BidSourceFile"] as Dictionary<string, object>;
                    var BidSourceFileBLL = new DTcms.BLL.BidSourceFile();
                    var BidSourceFileList = BidSourceFileBLL.GetModelList("BidID=" + bidModel.ID);
                    if (BidSourceFileList.Count == 0)
                    {
                        ret = BidSourceFileBLL.Add(new DTcms.Model.BidSourceFile
                         {
                             BidID = bidModel.ID,
                             AddTime = DateTime.Now,
                             Path = BidSourceFileDic["Path"].ToString(),
                             NeedTranslation = BidSourceFileDic["NeedTranslation"].ToString() == "True"
                         }) > 0;
                    }
                    else
                    {
                        BidSourceFileList[0].Path = BidSourceFileDic["Path"].ToString();
                        BidSourceFileList[0].NeedTranslation = BidSourceFileDic["NeedTranslation"].ToString() == "True";
                        ret = BidSourceFileBLL.Update(BidSourceFileList[0]);
                    }
                }
            }
            //短信
            if (ret)
            {
                var smsMsg = string.Empty;
                var msgBLL = new DTcms.BLL.tx_message();
                //用户申办提醒
                var userSMS = new BLL.sms_template().GetModel("UserBid"); //取得短信内容
                //msgBLL.Send(bidModel.Tel, userSMS.content
                //    .Replace("{Number}", bidModel.Number)
                //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                //    , 1, out smsMsg);
                var msgParam = string.Format("\"{0}\",\"{1}\"", bidModel.Number, DateTime.Now.ToString("yyyy-MM-dd"));
                msgBLL.Send(bidModel.Tel, userSMS.content, 1, msgParam, out smsMsg);

                //公证员申办提醒
                var JusticeConfigModel = DTcms.Common.SerializationHelper.Load<DTcms.Model.JusticeConfig>(DTcms.Common.DTKeys.BIDCONFIG_JUSTICE_PATH);
                var manageSMS = new BLL.sms_template().GetModel("ManageBid"); //取得短信内容
                //msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content
                //    .Replace("{CnName}", bidModel.CnName)
                //    .Replace("{BidBusiness}", new DTcms.BLL.View_Bid().GetModelList("ID=" + bidModel.ID)[0].BidBusiness)
                //    .Replace("{Number}", bidModel.Number)
                //    .Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                //    , 1, out smsMsg);
                msgParam = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                        bidModel.CnName, new DTcms.BLL.View_Bid().GetModelList("ID=" + bidModel.ID)[0].BidBusiness,
                        bidModel.Number, DateTime.Now.ToString("yyyy-MM-dd"));
                msgBLL.Send(JusticeConfigModel.Tel, manageSMS.content, 1, msgParam, out smsMsg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgRet", "location.href=\"Bid_Step2_3.aspx?cid=58\";", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgRet", "alert(\"提交失败，请联系系统管理员\");", true);
        }
    }
}