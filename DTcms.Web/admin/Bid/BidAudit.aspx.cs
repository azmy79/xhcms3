using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Bid
{
    public partial class BidAudit : Web.UI.ManagePage
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 申办信息对象
        /// </summary>
        public DTcms.Model.View_Bid BidModel { get; set; }

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
            ID = DTcms.Common.DTRequest.GetQueryInt("id");
            if (ID == 0)
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            //申办信息
            BidModel = new DTcms.BLL.View_Bid().GetModelList("ID=" + ID)[0];
            TRLanguage = new DTcms.BLL.TRLanguage().GetModelList("ID in(select TRLanguageID from Bid_TRLanguage where Bid_TRLanguage.BidID=" + BidModel.ID + ") order by Sort Desc");
            BidBusiness = new DTcms.BLL.BidBusiness().GetModelList("ID in(select BidBusinessID from Bid_BidBusiness where Bid_BidBusiness.BidID=" + BidModel.ID + ") order by Sort Desc");
            Document = new DTcms.BLL.View_Document().GetModelList("BidID=" + BidModel.ID + " order by Sort Desc");
            BidSourceFile = new DTcms.BLL.BidSourceFile().GetModelList("BidID=" + BidModel.ID);
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            txtPrice.Text = Convert.ToInt32(BidModel.Price).ToString();
            rblStatus.SelectedValue = BidModel.Status.ToString();
            if (BidSourceFile.Count > 0)
                txtTranslationPrice.Text = Convert.ToInt32(BidSourceFile[0].TranslationPrice).ToString();
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var ret = true;
            //公证文本翻译价格
            if (BidSourceFile.Count > 0)
            {
                BidSourceFile[0].TranslationPrice = decimal.Parse(txtTranslationPrice.Text);
                ret = new DTcms.BLL.BidSourceFile().Update(BidSourceFile[0]);
            }
            if (ret && new DTcms.BLL.Bid().UpdateField(ID, "Status=" + rblStatus.SelectedValue + ",Price=" + txtPrice.Text.Trim()))
            {
                var smsMsg = string.Empty;
                var msgBLL = new DTcms.BLL.ali_message();
                //审核不通过
                if (rblStatus.SelectedValue == "0")
                {
                    //用户审核通过提醒
                    var userSMS = new BLL.sms_template().GetModel("UserAuditFalse"); //取得短信内容
                   // msgBLL.Send(BidModel.Tel, userSMS.content
                   //.Replace("{Number}", BidModel.Number)
                   //.Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                   //, 1, out smsMsg);
                   var msgParam = "{" + string.Format("\"Number\":\"{0}\",\"SendTime\":\"{1}\"",
                        BidModel.Number, DateTime.Now.ToString("yyyy-MM-dd")) + "}";
                    msgBLL.Send(BidModel.Tel, userSMS.content, 1, msgParam, out smsMsg);
                }
                else
                {
                    //用户审核不通过提醒
                    var userSMS = new BLL.sms_template().GetModel("UserAuditTrue"); //取得短信内容
                   // msgBLL.Send(BidModel.Tel, userSMS.content
                   //.Replace("{Number}", BidModel.Number)
                   //.Replace("{SendTime}", DateTime.Now.ToString("yyyy-MM-dd"))
                   //, 1, out smsMsg);
                   var msgParam = "{" + string.Format("\"Number\":\"{0}\",\"SendTime\":\"{1}\"",
                        BidModel.Number, DateTime.Now.ToString("yyyy-MM-dd")) + "}";
                    msgBLL.Send(BidModel.Tel, userSMS.content, 1, msgParam, out smsMsg);
                }
                JscriptMsg("保存成功！", "BidList.aspx", "Success");
            }
            else
                JscriptMsg("保存失败！", "Error");
        }
    }
}