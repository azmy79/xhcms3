﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.article
{
    public partial class category_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        private int channel_id;
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.id = DTRequest.GetQueryInt("id");

            if (this.channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }
            this.channel_name = new BLL.channel().GetChannelName(this.channel_id); //取得频道名称
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.article_category().Exists(this.id))
                {
                    JscriptMsg("类别不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(this.channel_id); //绑定类别
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    if (this.id > 0)
                    {
                        this.ddlParentId.SelectedValue = this.id.ToString();
                    }
                }
                selPageType.Items.AddRange(new ListItem[]{
                    new ListItem("超链接","Link"),
                    new ListItem("单页","Single"),
                    new ListItem("新闻列表","News"),
                    new ListItem("表格下载频道","TableDownView"),
                    new ListItem("表格下载列表","TableDown"),
                });
            }
        }

        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("无父级分类", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.article_category bll = new BLL.article_category();
            Model.article_category model = bll.GetModel(_id);

            ddlParentId.SelectedValue = model.parent_id.ToString();
            txtCallIndex.Text = model.call_index;
            txtTitle.Text = model.title;
            txtSortId.Text = model.sort_id.ToString();
            txtSeoTitle.Text = model.seo_title;
            txtSeoKeywords.Text = model.seo_keywords;
            txtSeoDescription.Text = model.seo_description;
            txtLinkUrl.Text = model.link_url;
            txtImgUrl.Text = model.img_url;
            txtContent.Value = model.content;
        }
        #endregion

        #region 增加操作=================================
        private int DoAdd()
        {
            var retID = 0;
            try
            {
                Model.article_category model = new Model.article_category();
                BLL.article_category bll = new BLL.article_category();
                model.channel_id = this.channel_id;
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                model.parent_id = int.Parse(ddlParentId.SelectedValue);
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.seo_title = txtSeoTitle.Text;
                model.seo_keywords = txtSeoKeywords.Text;
                model.seo_description = txtSeoDescription.Text;
                model.link_url = txtLinkUrl.Text.Trim();
                model.img_url = txtImgUrl.Text.Trim();
                model.content = txtContent.Value;
                retID = bll.Add(model);
                if (retID > 0)
                {
                    if (selPageType.Value != "Link")
                    {
                        bll.UpdateField(retID, "link_url='" + selPageType.Value + ".aspx?cid=" + retID + "'");
                    }
                    bll.UpdateField(retID, "");
                    AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道栏目分类:" + model.title); //记录日志
                }
            }
            catch
            {
                return retID;
            }
            return retID;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            try
            {
                BLL.article_category bll = new BLL.article_category();
                Model.article_category model = bll.GetModel(_id);

                int parentId = int.Parse(ddlParentId.SelectedValue);
                model.channel_id = this.channel_id;
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                //如果选择的父ID不是自己,则更改
                if (parentId != model.id)
                {
                    model.parent_id = parentId;
                }
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.seo_title = txtSeoTitle.Text;
                model.seo_keywords = txtSeoKeywords.Text;
                model.seo_description = txtSeoDescription.Text;
                model.link_url = txtLinkUrl.Text.Trim();
                if (selPageType.Value != "Link")
                {
                    model.link_url = selPageType.Value + ".aspx?cid=" + model.id;
                }
                model.img_url = txtImgUrl.Text.Trim();
                model.content = txtContent.Value;
                if (bll.Update(model))
                {
                    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道栏目分类:" + model.title); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        //保存类别
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {

                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改类别成功！", "category_list.aspx?channel_id=" + channel_id, "Success");
            }
            else //添加
            {
                ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!(DoAdd() > 0))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加类别成功！", "category_list.aspx?channel_id=" + channel_id, "Success");
            }
        }

    }
}