<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterSuccess.aspx.cs" Inherits="DTcms.Web.RegisterSuccess" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/login.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="script/jquery-1.4.4.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        $(function () {
            var timeCount = 5;
            setInterval(function () {
                timeCount--;
                if (!timeCount) location.href = "UserCenter.aspx";
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
            <h2>用户注册</h2>
            <div class="c">
                <div style="padding: 20px; text-align: center">
                    <h5 style="font-size: 20px; padding: 10px 0px;">恭喜您注册成功</h5>
                    <p>还有5秒将会自动跳转到会员中心，还剩<span class="red"> 5 </span>秒</p>
                    <p><a class="blue" href="UserCenter.aspx">如果未自动跳转，请手点击此链接</a></p>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
