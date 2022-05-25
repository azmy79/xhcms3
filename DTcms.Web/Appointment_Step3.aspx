<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appointment_Step3.aspx.cs" Inherits="DTcms.Web.Appointment_Step3" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>
<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/doc.css" type="text/css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        $(function () {
            var timeCount = 5;
            setInterval(function () {
                timeCount--;
                if (!timeCount) location.href = "UserAppointment.aspx";
                else $(".red").text(timeCount);
            }, 1000)
        });
    </script>
</head>
<body>
    <uc1:Header runat="server" ID="Header" />
    <uc1:DocBanner runat="server" ID="DocBanner" />
    <!--顶部banner-->
    <div class="w">
        <div class="style_but3">网上预约</div>
        <div class="step_yy" style="background-position: 0px -138px"></div>
        <div class="w_yy">
            <div style="padding: 10px; text-align: center">
                <h5 style="font-size: 20px; padding: 10px 0px;">恭喜您预约成功，请按照预约时间，凭短信通知和预约号码到我处办理公证。</h5>
                <p>还有5秒将会自动跳转到“我的预约”页面，还剩<span class="red"> 5 </span>秒</p>
                <p><a class="blue" href="UserAppointment.aspx">如果未自动跳转，请手点击此链接</a></p>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
