<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Single.aspx.cs" Inherits="DTcms.Web.MWeb.Single" %>

<html>
<head>
    <meta charset="utf-8" />
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <meta name="viewport" content="width=device-width,initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="style/common2.css" media="all">
    <script type="text/javascript" src="script/jquery-1.4.2.min.js"></script>
</head>
<body>
    <!--#include file ="module/header.shtml" -->
    <!--#include file ="module/sitemap.shtml" -->
    <%
        var model = GetCategory(DTcms.Common.DTRequest.GetQueryInt("cid"));
        if (model == null) Response.Redirect("index.aspx");
    %>
    <div class="article">
        <h2><%=model.title %></h2>
        <div class="art_co sau">
            <%=model.content %>
        </div>
        <div class="share">
            <!-- JiaThis Button BEGIN -->
            <div id="ckepop">
                <span class="span">分享到</span>
                <div class="bdsharebuttonbox"><a title="分享到QQ空间" href="#" class="bds_qzone" data-cmd="qzone"></a><a title="分享到新浪微博" href="#" class="bds_tsina" data-cmd="tsina"></a><a title="分享到腾讯微博" href="#" class="bds_tqq" data-cmd="tqq"></a><a title="分享到人人网" href="#" class="bds_renren" data-cmd="renren"></a><a title="分享到微信" href="#" class="bds_weixin" data-cmd="weixin"></a></div>
                <script>window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "", "bdMini": "2", "bdMiniList": false, "bdPic": "", "bdStyle": "0", "bdSize": "16" }, "share": {} }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];
                </script>
            </div>
            <!-- JiaThis Button END -->
        </div>
    </div>
    <!--#include file ="module/foot.shtml" -->
</body>
</html>
