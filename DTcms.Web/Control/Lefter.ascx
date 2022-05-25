<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lefter.ascx.cs" Inherits="DTcms.Web.Control.Lefter" %>
<div class="left_common">
    <div class="w_l1">
        <div class="t1"></div>
    </div>
    <div class="w_l2">
        <div class="line_left"></div>
        <div class="line_left" id="divLeftNav"></div>
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
            //加载左侧菜单
            (function () {
                //所有菜单项
                var navObj = <%=DTcms.Web.UI.BasePage.GetLeftFullNav(DTcms.Common.DTRequest.GetQueryInt("cid"))%>;
                //当前项ID
                var cid=<%=DTcms.Common.DTRequest.GetQueryInt("cid")%>;
                //大标题
                $(".left_common .t1").html("<span>"+navObj.Nav.EnTitle+"</span>"+navObj.Nav.Title);
                //待输出HTML数组
                var htmlArr = [];
                //3级菜单以内
                if(navObj.FullLevel < 3&&navObj.Nav.SubNav){
                    htmlArr.push("<ul class=\"mune\">");
                    $.each(navObj.Nav.SubNav, function (i, obj) {
                        htmlArr.push("<li><a "+(obj.ID ==cid?"class=\"selected\"":"")+" href=\""+obj.LinkUrl+"\">"+obj.Title+"</a></li>");
                    });
                    htmlArr.push("</ul>");
                    //输出
                    $(htmlArr.join("")).insertBefore($("#divLeftNav"));
                }
            })();
            //加载运行
            $(function () 
            { 
                $('.jfade_image').jfade(); 
            });
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
