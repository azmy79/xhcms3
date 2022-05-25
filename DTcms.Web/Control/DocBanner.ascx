<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocBanner.ascx.cs" Inherits="DTcms.Web.Control.SiteMap" %>
<div class="banner_doc">
    <%
        var cModel = new DTcms.BLL.article_category().GetModel(DTcms.Common.DTRequest.GetQueryInt("cid"));
        if (cModel != null && !string.IsNullOrEmpty(cModel.img_url))
        {
    %>
    <img src="<%=cModel.img_url %>">
    <%
        }%>
</div>
