<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.MWeb.index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <meta name="viewport" content="width=device-width,initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="style/common2.css" media="all">
    <script type="text/javascript" src="script/touchslider_1.js"></script>
    <script type="text/javascript" src="script/jquery-1.4.2.min.js"></script>
</head>
<body>
    <!--#include file ="module/header.shtml" -->
    <div class="sbox">
        <div class="swipe" id="slider">
            <ul class="piclist">
                <%
                    var ad7 = GetAdvertising(7);
                    ad7.ForEach(p =>
                    {
                        Response.Write("<li>");
                        Response.Write("    <a href=\"" + p.link_url + "\" title=\"" + p.title + "\" target=\"_blank\">");
                        Response.Write("        <img src=\"" + p.ad_url + "\" alt=\"" + p.title + "\">");
                        Response.Write("    </a> ");
                        Response.Write("</li>");
                    });
                %>
            </ul>
            <div id="pagenavi2" class="page">
                <%
                    ad7.ForEach(p =>
                    {
                        Response.Write("<a class=\"" + (ad7.IndexOf(p) == 0 ? "active" : string.Empty) + "\"></a>");
                    });
                %>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div>
        <ul class="list_font">
            <%
                var nav36 = GetCategory(36);
                var nav41 = GetCategory(41);
                var nav45 = GetCategory(45);
                var nav56 = GetCategory(56);
                var nav59 = GetCategory(59);
                var nav62 = GetCategory(62);
            %>
            <li><a href="<%=nav36.link_url %>">
                <div>
                    <span class="">
                        <img src="images/1.png"></span>
                </div>
                <div>
                    <p><%=nav36.title %></p>
                </div>
            </a></li>
            <li><a href="<%=nav41.link_url %>">
                <div>
                    <span class="">
                        <img src="images/2.png"></span>
                </div>
                <div>
                    <p><%=nav41.title %></p>
                </div>
            </a></li>
            <li><a href="<%=nav45.link_url %>">
                <div>
                    <span class="">
                        <img src="images/3.png"></span>
                </div>
                <div>
                    <p><%=nav45.title %></p>
                </div>
            </a></li>
            <li><a href="<%=nav56.link_url %>">
                <div>
                    <span class="">
                        <img src="images/4.png"></span>
                </div>
                <div>
                    <p><%=nav56.title %></p>
                </div>
            </a></li>
            <li><a href="<%=nav59.link_url %>">
                <div>
                    <span class="">
                        <img src="images/5.png"></span>
                </div>
                <div>
                    <p><%=nav59.title %></p>
                </div>
            </a></li>
            <li><a href="<%=nav62.link_url %>">
                <div>
                    <span class="">
                        <img src="images/6.png"></span>
                </div>
                <div>
                    <p><%=nav62.title %></p>
                </div>
            </a></li>
        </ul>
        <div class="clear"></div>
    </div>
    <!--#include file ="module/foot.shtml" -->
    <script type="text/javascript" src="script/index.js"></script>
</body>
</html>
