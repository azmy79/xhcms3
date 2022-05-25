<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="DTcms.Web.News" %>

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
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script src="script/jquery.pagination.js"></script>
    <script src="script/custom/PageDataHelper.js"></script>
    <script src="script/custom/Common.js"></script>
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
            <div class="cls_al">
                <ul></ul>
                <div class="page">
                    <script>
                        //分页查询
                        $(".page").SetPagination({
                            Container: ".cls_al ul",//容器选择器
                            Url: "Ashx/News.ashx",//请求地址
                            PageSize: 6,//页大小
                            Data: {
                                option: "GetNewsData",
                                cid: $.GetQueryString("cid")
                            },//请求参数
                            htmlJoinFunc: function (htmlArr, i, obj) {
                                htmlArr.push("<li " + (i % 2 ? "" : "style=\"background: rgb(240, 240, 240);\"") + "><dl>");
                                htmlArr.push("    <dt><a href=\"" + GetNews_ViewUrl(obj) + "\">" + obj.Title.ToShortString(35) + "</a><span>[" + obj.AddTime + "]</span></dt>");
                                htmlArr.push("    <dd></dd>");
                                htmlArr.push("    <dd>" + obj.ZhaoYao.ToShortString(280) + "</dd>");
                                htmlArr.push("</dl>");
                                htmlArr.push("<a class=\"but\" href=\"" + GetNews_ViewUrl(obj) + "\">查看<br>");
                                htmlArr.push("    详情</a>");
                                htmlArr.push("<div class=\"clear\"></div></li>");
                            }//HTML拼接回调(待拼接HTML数组,索引,对象)
                        });
                    </script>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
