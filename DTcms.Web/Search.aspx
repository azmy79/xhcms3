<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="DTcms.Web.Search" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/Lefter.ascx" TagPrefix="uc1" TagName="Lefter" %>
<%@ Register Src="~/Control/SiteMap.ascx" TagPrefix="uc1" TagName="SiteMap" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>

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
            <script>
                $(".bread h2").html("关键字<lable style=\"color:red\">“" + $.GetQueryString("kw") + "”</lable>的搜索结果如下：")
            </script>
            <div class="cls_news_02 li">
                <ul>
                </ul>
                <div class="page">
                    <script>
                        //分页查询
                        $(".page").SetPagination({
                            Container: ".cls_news_02 ul",//容器选择器
                            Url: "Ashx/News.ashx",//请求地址
                            PageSize: 6,//页大小
                            Data: {
                                option: "GetNewsData",
                                cid: $.GetQueryString("cid"),
                                keyWord: $.GetQueryString("kw"),
                                dataType: "search"
                            },//请求参数
                            htmlJoinFunc: function (htmlArr, i, obj) {
                                htmlArr.push("<li><a href=\"" + GetNews_ViewUrl(obj) + "\">" + obj.Title + "</a></li>");
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
