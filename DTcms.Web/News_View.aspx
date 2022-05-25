<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_View.aspx.cs" Inherits="DTcms.Web.News_View" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/Lefter.ascx" TagPrefix="uc1" TagName="Lefter" %>
<%@ Register Src="~/Control/SiteMap.ascx" TagPrefix="uc1" TagName="SiteMap" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>


<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/doc.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="script/jquery-1.4.4.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
</head>
<body>
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <!--综合头部结束-->
    <uc1:DocBanner runat="server" ID="DocBanner" />
    <!--顶部banner-->
    <div class="w">
        <uc1:Lefter runat="server" ID="Lefter" />
        <%
            var newsModel = ReadArticle(DTcms.Common.DTRequest.GetQueryInt("id"));
            if (newsModel == null) SkipIndex();
        %>
        <div class="right_common">
            <uc1:SiteMap runat="server" ID="SiteMap" />
            <div class="doc">
                <h2><%=newsModel.title %></h2>
                <div class="air"></div>
                <div class="air"></div>
                <div class="bv"><span>作者：<font><%=newsModel.user_name %></font></span><span>发布时间：<font><%=newsModel.add_time.ToString("yyyy-MM-dd") %></font></span><span>浏览量：<font><%=newsModel.click %></font></span></div>
                <div class="air"></div>
                <div class="line"></div>
                <div class="showtxt">
                    <%=newsModel.content %>
                </div>
                <div class="xg">
                    <h4>相关阅读</h4>
                    <ul>
                        <%
                            string _where = "status=0  and category_id=" + newsModel.category_id;
                            var bll = new DTcms.BLL.article();
                            string _order = "is_top Desc,sort_id desc,add_time desc";
                            var ds = bll.GetList(9, _where, _order);
                            var list = DTcms.Common.DataConvertHelper.DataTableToList<DTcms.Model.article>(ds.Tables[0]);
                            list.Remove(list.Find(p => p.id == newsModel.id));
                            if (list.Count > 8) list.RemoveAt(8);
                            list.ForEach(p =>
                           {
                               Response.Write("<li><a href=\"" + GetNews_ViewUrl(p) + "\">" + GetShortString(p.title, 60) + "</a></li>");
                           });
                        %>
                    </ul>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
