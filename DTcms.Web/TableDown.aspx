<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableDown.aspx.cs" Inherits="DTcms.Web.TableDown" %>

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
        <div class="right_common">
            <uc1:SiteMap runat="server" ID="SiteMap" />
            <div class="w_bg">
                <div class="list_dwon">
                    <%
                        GetNews(DTcms.Common.DTRequest.GetQueryInt("cid")).ForEach(p =>
                        {
                            var model = new DTcms.BLL.article().GetModel(p.id);
                            Response.Write("<a href=\"" + (model.attach != null && model.attach.Count > 0 ? model.attach[0].file_path : string.Empty) + "\">" + p.title + "</a>");
                        });
                    %>
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
