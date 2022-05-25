<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.index" %>

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
    <link href="style/index.css" type="text/css" rel="stylesheet" />
    <link href="css/Common.css" rel="stylesheet" />
    <script type="text/javascript" src="script/jquery-1.4.4.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script src="script/custom/Common.js"></script>
    <script type="text/javascript">
        //默认检查移动设备并跳转移动端
        if (!$.GetQueryString("formMWeb") && browser.versions.mobile) location.href = "MWeb/index.aspx";
    </script>
</head>
<body>
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <script>
        //导航选择
        $(".nav ul a[href=index.aspx]").addClass("selected");
    </script>
    <!--综合头部结束-->
    <div class="focus" id="focus">
        <div id="focus_m" class="focus_m">
            <ul>
                <%
                    var isFirst = true;
                    GetAdvertising(1).ForEach(p =>
                    {
                        Response.Write("<li style=\"display: " + (isFirst ? "block" : "none") + "; background: url('" + p.ad_url + "') center top no-repeat;\" onclick=\"javascript:window.open('" + p.link_url + "');\"></li>");
                        isFirst = false;
                    });
                %>
            </ul>
        </div>
        <a href="javascript:;" class="focus_l" id="focus_l" title="上一张"></a><a href="javascript:;" class="focus_r" id="focus_r" title="下一张"></a>
        <script type="text/javascript" src="script/flashBanner.js"></script>
    </div>
    <div class="w">
        <div class="left_common">
            <%var model36 = new DTcms.BLL.article_category().GetModel(36);%>
            <div class="w_l1">
                <div class="t1">
                    <span><%=model36.call_index %></span>
                    武汉市长江公证处
                </div>
            </div>
            <div class="w_l2">
                <div class="line_left"></div>
                <div class="intro">
                    <a href="<%=model36.link_url %>">
                        <img src="/ls/aboutus.jpg" width="217"></a>
                    <p><%=model36.seo_description.Length>170?model36.seo_description.Substring(0,167)+"...":model36.seo_description %></p>
                </div>
                <div class="line_left"></div>
                <div class="tit_01" style="padding-left: 15px;">
                    <span style="font-size: 12px;">服务热线：
                    </span>
                    <%=DTcms.Web.UI.BasePage.config.webtel %>
                </div>
                <div class="ico_left">
                    <a class="jfade_image" href="http://wpa.qq.com/msgrd?v=3&uin=273663437&site=qq&menu=yes">
                        <img src="images/p_002.jpg"></a> <a class="jfade_image" href="http://weibo.com/u/3047522025?from=hissimilar_home">
                            <img src="images/p_003.jpg"></a>
                    <a class="ewm jfade_image" href="http://wpa.qq.com/msgrd?v=3&uin=273663437&site=qq&menu=yes">
                        <img src="images/p_004.jpg">
                        <div class="drop">
                            <img src="images/wx.jpg">
                        </div>
                    </a>
                </div>
                <script type="text/javascript" src="script/jfade.js"></script>
                <script type="text/javascript">
                    $(function () { $('.jfade_image').jfade(); });
                </script>
            </div>
            <div class="w_l3">
                <%var model62 = new DTcms.BLL.article_category().GetModel(62);%>
                <div class="tit_01"><span><%=model62.call_index %></span><%=model62.title %> </div>
                <div class="mes">
                    <%=model62.content %>
                </div>
                <div class="cave">
                    <a class="jfade_image" href="Single.aspx?cid=60">
                        <img src="images/p_005.jpg">
                    </a><a class="jfade_image" href="Appointment_Step1.aspx?cid=57">
                        <img src="images/p_006.jpg"></a>
                    <a class="jfade_image" href="#">
                        <img src="images/p_007.jpg"></a>
                    <a class="jfade_image" href="Bid_Step1.aspx?cid=58">
                        <img src="images/p_008.jpg"></a>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div class="right_common">
            <div class="w_gg">
                <%var model44 = GetCategory(44); %>
                <h4><%=model44.title %></h4>
                <div class="list">
                    <ul class="cls_dt" id="ticker-1">
                        <%
                            GetIndexNews(44, 8).ForEach(p =>
                            {
                                Response.Write("<li><a href=\"" + GetNews_ViewUrl(p) + "\">" + p.title + "</a><span>[" + p.add_time.ToString("MM-dd") + "]</span></li>");
                            });  
                        %>
                    </ul>
                </div>

            </div>
            <div class="w_news">
                <div class="flash_02">
                    <div class="comiis_wrapad" id="slideContainer">
                        <div id="frameHlicAe" class="frame cl">
                            <div class="temp"></div>
                            <div class="block">
                                <div class="cl">
                                    <ul class="slideshow" id="slidesImgs">
                                        <%
                                            GetAdvertising(2).ForEach(p =>
                                            {
                                                Response.Write("<li><a href=\"" + p.link_url + "\" target=\"_blank\"><img src=\"" + p.ad_url + "\" height=\"310\" width=\"490\" alt=\"\" /></a><span class=\"title\">" + p.title + "</span></li>");
                                            });
                                        %>
                                    </ul>
                                </div>
                                <div class="slidebar" id="slideBar">
                                    <ul>
                                        <script>
                                            $.each($("#slidesImgs li"), function (i, obj) {
                                                document.write("<li" + (!i ? " class=\"on\"" : "") + ">" + (i + 1) + "</li>")
                                            });
                                        </script>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <script src="script/slideshow.js" type="text/javascript"></script>
                    <script type="text/javascript">
                        SlideShow(5000);
                    </script>
                </div>
            </div>
            <div class="list_news">
                <div class="tit_02">
                    <%var model41 = GetCategory(41); %>
                    <h2><%=model41.title %><span><%=model41.call_index %></span></h2>
                    <a href="<%=model41.link_url %>">MORE <span class="blue">+</span></a>
                </div>
                <div class="pad10">
                    <ul class="cls_news_01">
                        <%
                            GetIndexNews(41, 8).ForEach(p =>
                            {
                                Response.Write("<li><span>" + p.add_time.ToString("MM-dd") + "</span><a href=\"" + GetNews_ViewUrl(p) + "\"><p class=\"text-overflow\">" + p.title + "</p></a></li>");
                            });
                        %>
                    </ul>
                </div>
            </div>
            <div class="clear"></div>
            <div class="air"></div>
            <div class="air"></div>
            <div class="w_news_c">
                <div class="tit_03">
                    <%var model46 = GetCategory(46); %>
                    <h2><%=model46.title %></h2>
                    <a href="<%=model46.link_url %>">MORE <span class="blue">+</span></a>
                </div>
                <ul class="cls_news_02">
                    <%
                        GetIndexNews(46, 8).ForEach(p =>
                        {
                            Response.Write("<li><a href=\"" + GetNews_ViewUrl(p) + "\"><p class=\"text-overflow\">" + p.title + "</p></a></li>");
                        });
                    %>
                </ul>
            </div>
            <div class="w_news_c">
                <div class="tit_03">
                    <%var model47 = GetCategory(47); %>
                    <h2><%=model47.title %></h2>
                    <a href="<%=model47.link_url %>">MORE <span class="blue">+</span></a>
                </div>
                <ul class="cls_news_02">
                    <%
                        GetIndexNews(47, 8).ForEach(p =>
                        {
                            Response.Write("<li><a href=\"" + GetNews_ViewUrl(p) + "\"><p class=\"text-overflow\">" + p.title + "</p></a></li>");
                        });
                    %>
                </ul>
            </div>
            <div class="w_news_c">
                <div class="tit_03">
                    <%var model48 = GetCategory(48); %>
                    <h2><%=model48.title %></h2>
                    <a href="<%=model48.link_url %>">MORE <span class="blue">+</span></a>
                </div>
                <ul class="cls_news_02">
                    <%
                        GetIndexNews(48, 8).ForEach(p =>
                        {
                            Response.Write("<li><a href=\"" + GetNews_ViewUrl(p) + "\"><p class=\"text-overflow\">" + p.title + "</p></a></li>");
                        });
                    %>
                </ul>
            </div>
            <div class="w_news_c">
                <div class="tit_03">
                    <%var model61 = GetCategory(61); %>
                    <h2><%=model61.title %></h2>
                    <a href="<%=model61.link_url %>">MORE <span class="blue">+</span></a>
                </div>
                <ul class="cls_news_02">
                    <%
                        GetIndexNews(61, 8).ForEach(p =>
                        {
                            Response.Write("<li><a href=\"" + GetNews_ViewUrl(p) + "\"><p class=\"text-overflow\">" + p.title + "</p></a></li>");
                        });
                    %>
                </ul>
            </div>
            <div class="clear"></div>
            <div class="air"></div>
            <div>
                <%
                    GetAdvertising(5).ForEach(p =>
                    {
                        Response.Write("<a href=\"" + p.link_url + "\"><img src=\"" + p.ad_url + "\" width=\"935\" height=\"111\"></a>");
                    });
                %>
            </div>
            <div class="air"></div>
            <div class="w_down">
                <div class="c">
                    <%
                        GetIndexNews(49).ForEach(p =>
                        {
                            var model = new DTcms.BLL.article().GetModel(p.id);
                            Response.Write("<a href=\"" + (model.attach != null && model.attach.Count > 0 ? model.attach[0].file_path : string.Empty) + "\">" + p.title + " </a>");
                        });
                    %>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
