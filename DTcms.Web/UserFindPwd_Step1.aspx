<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserFindPwd_Step1.aspx.cs" Inherits="DTcms.Web.UserFindPwd_Step1" %>

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
    <link href="script/easydialog.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        //加载运行
        $(function () {
            var timeNum = 0;
            //发送手机验证码
            $("#btnSendMobileCode").click(function () {
                if (!$("#txtMobile").val()) {
                    $.dialogAlert("请输入手机号码");
                    return;
                } else if (!/^[0-9]{11}$/.test($("#txtMobile").val())) {
                    $.dialogAlert("手机号码格式输入不正确");
                    return;
                }
                if (timeNum) return;//时间间隔未到
                var $a = $(this).find("a");
                //提交
                $.ajax({
                    url: "ashx/UserHelper.ashx?option=SendMobileCode",
                    data: $("#txtMobile").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            timeNum = 120;
                            $a.text("立即发送验证码(" + timeNum + "s)");
                            var sl = setInterval(function () {
                                timeNum--;
                                if (!timeNum) {
                                    clearInterval(sl);
                                    $a.text("立即发送验证码");
                                } else
                                    $a.text("立即发送验证码(" + timeNum + "s)");
                            }, 1000);
                        };
                    }
                });
            });

            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!$("#txtMobile").val()) {
                    $.dialogAlert("请输入手机号码");
                    return false;
                } else if (!/^[0-9]{11}$/.test($("#txtMobile").val())) {
                    $.dialogAlert("手机号码格式输入不正确");
                    return false;
                }
                if (!$("#txtMobileCode").val()) {
                    $.dialogAlert("请输入短信验证码");
                    return false;
                }
                //提交
                $.ajax({
                    url: "ashx/UserHelper.ashx?option=UserFindPwd_Step1",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            location.href = "UserFindPwd_Step2.aspx";
                        }
                    }
                });
            });
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
                <form>
                    <div class="regist_step" style="background: url(images/mm.gif) no-repeat 0px 0px; margin: 0 auto 30px; width: 551px; height: 130px;"></div>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr></tr>
                        <tr>
                            <td align="right">手机号码：</td>
                            <td>
                                <input maxlength="11" id="txtMobile" name="txtMobile" class="style_test" type="text" placeholder='11位手机号'></td>
                        </tr>
                        <tr>
                            <td align="right">验证码：</td>
                            <td>
                                <div class="f_l">
                                    <input id="txtMobileCode" name="txtMobileCode" class="style_test2" type="text" placeholder='2分钟后失效'>
                                </div>
                                <div id="btnSendMobileCode" style="padding: 2px 0px 0px 8px;" class="f_l"><a class="style_but2" href="javascript:;">立即发送验证码</a></div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <input class="style_but2" type="button" id="btnTrue" value="下一步"></td>
                        </tr>
                    </table>
                </form>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
