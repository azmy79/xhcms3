<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.ascx.cs" Inherits="DTcms.Web.Control.SiteMap" %>
<div class="bread">
    <p>
        <img align="absmiddle" src="images/ico5.gif" />
        <span><a href="index.aspx">首页</a></span> &gt;
    <script>
        //加载面包屑
        (function () {
            //JSON源
            var navObj=<%=DTcms.Web.UI.BasePage.GetFullSiteMap(DTcms.Common.DTRequest.GetQueryInt("cid"))%>;
            //递归输出导航
            var writeNav=function(obj){
                if(obj.SubNav){
                    document.write("<span><a href=\""+obj.LinkUrl+"\">"+obj.Title+"</a></span> &gt; ");
                    //递归装载
                    writeNav(obj.SubNav);
                }else{
                    document.write("<span class=\"blue\">"+obj.Title+"</span></p><h2>"+obj.Title+"</h2>");
                }
            };
            //输出
            writeNav(navObj);
        })();
    </script>
</div>

