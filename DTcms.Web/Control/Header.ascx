<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="DTcms.Web.Control.Header" %>
<div id="header">
    <div class="top">
        <div class="w">
            <div class="f_l">欢迎光临武汉长江公证处</div>
            <div class="f_r">
                <%
                    var userInfo = new DTcms.Web.UI.BasePage().GetUserInfo();
                    if (userInfo == null) Response.Write("<div class=\"off\"><span><a href=\"UserLogin.aspx\">[登录]</a></span><span><a href=\"UserRegister.aspx\">[注册]</a></span></div>");
                    else Response.Write("<div class=\"on\"><span>尊敬的<a href=\"UserCenter.aspx\"><em>" + userInfo.user_name + "</em></a>欢迎登录！</span><span><a href=\"UserCenter.aspx\">[当事人中心]</a></span><span><a href=\"UserLoginOut.aspx\">[退出]</a></span></div>");
                %>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <div class="w">
        <h1 class="logo">
            <img src="images/logo.jpg"></h1>
        <div class="nav">
            <ul>
                <li><a class="a" href="index.aspx">首页</a> </li>
            </ul>
        </div>
        <div class="line">|</div>
        <div class="search">
            <input class="txt" type="text" id="txtKw" onfocus="if (value =='请输入关键字'){value =''}" onblur="if (value ==''){value='请输入关键字'}" value="<%=string.IsNullOrEmpty(Request.QueryString["kw"])?"请输入关键字":Request.QueryString["kw"] %>">
            <input class="but" type="button" onclick="ch()">
        </div>
        <div class="by">
        </div>
    </div>
</div>
<script>
    //加载顶部菜单
    (function(){
        //所有菜单
        var navList=<%=DTcms.Web.UI.BasePage.GetTopFullNav(1,11)%>;
        //当前页
        <% var cid = DTcms.Common.DTRequest.GetQueryInt("cid");%>
        var cid=<%=cid%>;
        //根节点
        var baseID=<%=cid==0?cid:DTcms.Web.UI.BasePage.GetBaseNavID(cid).id%>;
        //菜单字符串
        var navHtmlArr=[];
        //按钮菜单字符串
        var byhtmlArr=[];
        //按钮菜单颜色数组
        var byColorArr=["#608d00","#434343","#00407c"];
        //循环拼接
        $.each(navList.NavList,function(i,obj){
            if(i<8){
                navHtmlArr.push("<li><a class=\"a "+(obj.ID==baseID?"selected":"")+"\" href=\""+obj.LinkUrl+"\">"+obj.Title+"</a>");
                if(obj.SubNav){
                    navHtmlArr.push("    <div class=\"nav-down\">");
                    //子菜单字符串
                    var subNavHtmlArr=[];
                    $.each(obj.SubNav,function(i,o){
                        subNavHtmlArr.push("        <a href=\""+o.LinkUrl+"\">"+o.Title+"</a>");
                    });
                    navHtmlArr.push(subNavHtmlArr.join(""));
                    navHtmlArr.push("    </div>");
                }
                navHtmlArr.push("</li>");
            }else{
                byhtmlArr.push("<a style=\"background: "+byColorArr[3-(11-i-1)-1]+"\" href=\""+obj.LinkUrl+"\">"+obj.Title+"</a>");
            }
        });
        //输出头部菜单
        $(".nav ul").append(navHtmlArr.join(""));
        //输出按钮菜单
        $("#header .by").append(byhtmlArr.join(""));
    })();

    //回车搜索
    $("#txtKw").keypress(function (e) {
        if (e.keyCode == "13")
            ch();
    });

    //搜索
    var ch = function () {
        if (!$("#txtKw").val()||$("#txtKw").val()=="请输入关键字") return;
        var kw = $("#txtKw").val().replace(/[~\!\@\#\$\%\^\&\*\(\)\_\+\|\{\}\:\"\<\>\?\`\-\=\\\[\]\;\'\,\.\/]/g, "");
        location.href = "Search.aspx?cid=63&kw=" + kw;
    };
</script>
