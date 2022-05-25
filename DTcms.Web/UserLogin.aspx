<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="DTcms.Web.UserLogin" %>

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
    <link href="script/easydialog.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        //加载运行
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!$("#txtUserName").val()) {
                    $.dialogAlert("请输入用户名");
                    return false;
                }
                if (!$("#txtPassword").val()) {
                    $.dialogAlert("请输入密码");
                    return false;
                }
                if (!$("#txtCode").val()) {
                    $.dialogAlert("请输入验证码");
                    return false;
                }
                //提交
                $.ajax({
                    url: "tools/submit_ajax.ashx?action=user_login",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            location.href = "UserCenter.aspx";
                        }
                    }
                });
            });
        });
    </script>
</head>
<body style="background: url(images/bg_user.jpg) no-repeat center 80px;">
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <!--综合头部结束-->

    <!--顶部banner-->
    <div class="w">
        <div class="w_login">
            <h2>用户登录</h2>
            <div class="c">
                <form>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right">用户名：</td>
                            <td>
                                <input id="txtUserName" name="txtUserName" class="style_test" type="text" placeholder='请输入用户名或手机'></td>
                        </tr>
                        <tr>
                            <td align="right">密码：</td>
                            <td>
                                <input id="txtPassword" name="txtPassword" class="style_test" type="password" placeholder='请输入密码'></td>
                        </tr>
                        <tr>
                            <td align="right">验证码：</td>
                            <td>
                                <input name="txtCode" id="txtCode" class="style_test2" type="text" placeholder='请输入验证码'>
                                <img style="cursor: pointer" align="absmiddle" src="tools/verify_code.ashx" height="42" onclick="this.src='tools/verify_code.ashx?'+Math.random()">
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="left"><span class="f_l">
                                <input style="vertical-align: middle;" id="chkRemember" name="chkRemember" type="checkbox" value="true">
                                下次自动登陆</span><span class="f_r"><a href="UserFindPwd_Step1.aspx">[忘记密码]</a></span></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <input class="style_but2" type="button" value="登 录" id="btnTrue"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="right"><a href="UserRegister.aspx">没有账号，立即注册</a></td>
                        </tr>
                    </table>
                </form>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
