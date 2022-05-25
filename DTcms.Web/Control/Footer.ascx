<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="DTcms.Web.Control.Footer" %>
<div id="footer">
    <div class="link">
        <div class="w">
            <h4>友情链接：</h4>
            <div class="c">
                <ul id="ticker-2">
                    <%
                        DTcms.Web.UI.BasePage.GetIndexLink().ForEach(p =>
                        {
                            Response.Write("<li><a href=\"" + p.site_url + "\" target=\"_blank\"> <img src=\"" + p.img_url + "\"></a><li>");
                        });
                    %>
                </ul>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div class="nav_b">
        <a target="_blank" href="Single.aspx?cid=36">机构简介</a> |
        <a target="_blank" href="News.aspx?cid=45">办证须知</a> |
        <a target="_blank" href="TableDownView.aspx?cid=49">表格下载</a> |
        <a target="_blank" href="News.aspx?cid=56">收费标准</a> |
        <a target="_blank" href="News.aspx?cid=61">法律法规</a> |
        <a target="_blank" href="#">公证书查询</a> |
        <a target="_blank" href="FeedBack.aspx?cid=59">在线咨询</a> |
    </div>
    <div class="mes">
        <img src="images/logo_b.png"><br>
        <!--底部信息-->
        <%=DTcms.Web.UI.BasePage.config.webaddress %>
        <br>
        <%=DTcms.Web.UI.BasePage.config.webcopyright %> <%=DTcms.Web.UI.BasePage.config.webcrod %>
        <!--底部信息-->
        <br>
        技术支持：<a style="font-weight: bold; color: #ffeeb2" href="http://www.whnewnet.com" target="_blank">武汉新网科技</a>
    </div>
</div>
