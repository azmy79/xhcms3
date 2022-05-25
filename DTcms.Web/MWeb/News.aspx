<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="DTcms.Web.MWeb.News" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <meta name="viewport" content="width=device-width,initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="style/common2.css" media="all">
    <script src="../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="../script/custom/Common.js"></script>
    <script>
        $(function () {
            var pageIndex = 0;//当前页
            var pageSize = 10;//页大小
            var isLoad = true;//是否加载数据

            //加载数据
            var loadData = function () {
                if (!isLoad) return;
                isLoad = false;
                //获取数据源
                $.ajax({
                    type: "Get",//请求方式
                    url: "../Ashx/News.ashx",
                    dataType: "JSON",
                    data: {
                        option: "GetNewsData",
                        cid: $.GetQueryString("cid"),
                        pageSize: pageSize,
                        pageIndex: pageIndex
                    }//参数
                }).done(function (data) {
                    //加载数据
                    var htmlArr = [];
                    $.each(data.list, function (i, obj) {
                        //输出格式
                        htmlArr.push("<li><a href=\"" + GetNews_ViewUrl(obj) + "\">" + obj.Title + " <span>" + obj.AddTime + "</span></a></li>")
                    });
                    //输出
                    $(htmlArr.join("")).appendTo($(".list_news"));
                    isLoad = pageIndex * pageSize + data.list.length < data.totalCount;//是否加载数据
                    pageIndex++;//页码累加
                });
            };

            $(window).scroll(function () {
                //当内容滚动到底部时加载新的内容  
                if ($(this).scrollTop() + $(window).height() + 20 >= $(document).height() && $(this).scrollTop() > 20) {
                    //加载数据  
                    loadData();
                }
            });

            loadData();
        });
    </script>
</head>
<body>
    <!--#include file ="module/header.shtml" -->
    <!--#include file ="module/sitemap.shtml" -->
    <!--#include file ="module/nav.shtml" -->
    <ul class="list_news">
    </ul>
    <!--#include file ="module/foot.shtml" -->
</body>
</html>
