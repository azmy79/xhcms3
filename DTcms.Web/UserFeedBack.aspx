<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserFeedBack.aspx.cs" Inherits="DTcms.Web.UserFeedBack" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/user.css" type="text/css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script src="script/jquery.pagination.js"></script>
    <script src="script/custom/PageDataHelper.js"></script>
    <script src="script/custom/Common.js"></script>
</head>
<body>
    <uc1:Header runat="server" ID="Header" />
    <div class="w_user">
        <!--#include file ="module/UserMenu.shtml" -->
        <div class="right_user">
            <div class="pad15">
                <div class="list_message2">
                    <div class="cls_news">
                        <ul>
                        </ul>
                        <div class="clear"></div>
                        <div class="page">
                            <script>
                                //分页查询
                                $(".page").SetPagination({
                                    Container: ".cls_news ul",//容器选择器
                                    Url: "Ashx/FeedBack.ashx",//请求地址
                                    PageSize: 4,//页大小
                                    Data: {
                                        option: "GetFeedBackData",
                                        FromUser: true
                                    },//请求参数
                                    htmlJoinFunc: function (htmlArr, i, obj) {
                                        htmlArr.push("<li>");
                                        htmlArr.push("    <h2><span>时间：" + obj.AddTime + "</span>  " + obj.Content + "</h2>");
                                        htmlArr.push("    <p class=\"down\"><span class=\"red\">官方回复：</span>" + obj.ReContent + " </p>");
                                        htmlArr.push("</li>");
                                    }//HTML拼接回调(待拼接HTML数组,索引,对象)
                                });
                            </script>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
