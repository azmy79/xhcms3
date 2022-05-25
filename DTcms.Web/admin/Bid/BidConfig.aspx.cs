using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Bid
{
    public partial class BidConfig : Web.UI.ManagePage
    {
        /// <summary>
        /// 值班公证员配置信息
        /// </summary>
        public DTcms.Model.JusticeConfig JusticeConfigModel { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JusticeConfigModel = DTcms.Common.SerializationHelper.Load<DTcms.Model.JusticeConfig>(DTcms.Common.DTKeys.BIDCONFIG_JUSTICE_PATH);
                txtJusticeName.Text = JusticeConfigModel.Name;
                txtJusticeTel.Text = JusticeConfigModel.Tel;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DTcms.Common.SerializationHelper.Save(new DTcms.Model.JusticeConfig
                {
                    Name = txtJusticeName.Text.Trim(),
                    Tel = txtJusticeTel.Text.Trim()
                }, DTcms.Common.DTKeys.BIDCONFIG_JUSTICE_PATH);
                JscriptMsg("修改配置成功！", "BidConfig.aspx", "Success");
            }
            catch (Exception)
            {
                JscriptMsg("文件写入失败，请检查是否有权限！", "", "Error");
            }
        }
    }
}