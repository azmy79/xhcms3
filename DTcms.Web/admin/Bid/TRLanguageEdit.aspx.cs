using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Bid
{
    public partial class TRLanguageEdit : UI.ManagePage
    {
        /// <summary>
        /// 是否是编辑操作
        /// </summary>
        public bool IsEdit
        {
            get { return Convert.ToBoolean(ViewState["IsEdit"]); }
            set { ViewState["IsEdit"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IsEdit = DTcms.Common.DTRequest.GetQueryString("option") == "edit";
                if (IsEdit)
                    BindData();
            }
        }

        //绑定数据
        public void BindData()
        {
            var list = new DTcms.BLL.TRLanguage().GetModelList("ID=" + DTcms.Common.DTRequest.GetQueryInt("id", 0));
            if (list.Count > 0)
            {
                var model = list[0];
                txtName.Text = model.Name;
                txtSort.Text = model.Sort.ToString();
                txtPrice.Text = Convert.ToInt32(model.DefaultPrice).ToString();
            }
        }

        //保存按钮点击事件
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var bll = new DTcms.BLL.TRLanguage();
            var model = new DTcms.Model.TRLanguage();
            if (IsEdit)
                model = bll.GetModel(DTcms.Common.DTRequest.GetQueryInt("id", 0));
            model.Name = txtName.Text.Trim();
            model.DefaultPrice = decimal.Parse(txtPrice.Text);
            model.Sort = int.Parse(txtSort.Text.Trim());
            if (IsEdit)
                if (bll.Update(model))
                    JscriptMsg("修改成功！", "TRLanguageList.aspx", "Success");
                else
                    JscriptMsg("修改失败！", "Error");
            else
                if (bll.Add(model) > 0)
                    JscriptMsg("新增成功！", "TRLanguageList.aspx", "Success");
                else
                    JscriptMsg("新增失败！", "Error");
        }
    }
}