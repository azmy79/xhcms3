<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserFindPwd_Step3.aspx.cs" Inherits="DTcms.Web.UserFindPwd_Step3" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/login.css" type="text/css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        $(function () {
            var timeCount = 5;
            setInterval(function () {
                timeCount--;
                if (!timeCount) location.href = "UserLogin.aspx";
                else $(".red").text(timeCount);
            }, 1000)
        });
    </script>
</head>
<body style="background: url(images/bg_user.jpg) no-repeat center 80px; background-attachment: fixed">
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <!--综合头部结束-->

    <!--顶部banner-->
    <div class="w">
        <div class="w_login">
            <h2>找回密码</h2>
            <div class="c">
                <div class="regist_step" style="background: url(images/mm.gif) no-repeat 0px -260px; margin: 0 auto 30px; width: 551px; height: 130px;"></div>
                <div style="padding: 20px; text-align: center">
                    <h5 style="font-size: 20px; padding: 10px 0px;">恭喜您密码修改成功</h5>
                    <p>还有5秒将会自动跳转到登录页面，还剩<span class="red"> 5 </span>秒</p>
                    <p><a class="blue" href="UserLogin.aspx">如果未自动跳转，请手点击此链接</a></p>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
